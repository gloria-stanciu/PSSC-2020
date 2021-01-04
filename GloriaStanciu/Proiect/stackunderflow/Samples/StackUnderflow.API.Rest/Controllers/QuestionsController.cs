using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOperations;
using StackUnderflow.EF.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackUnderflow.Domain.Core.Contexts.Question;
using LanguageExt;
using StackUnderflow.Domain.Core.Contexts.Questions;
using StackUnderflow.Domain.Schema.Backoffice.CreateTenantOp;
using StackUnderflow.Domain.Schema.Backoffice.InviteTenantAdminOp;
using System.Linq;
using StackUnderflow.DatabaseModel.Models;
using System;
using StackUnderflow.Domain.Core.Contexts.Question.SendQuestionOwnerAcknoledgementOperations;
using Access.Primitives.EFCore;
using StackUnderflow.Domain.Core.Contexts.Question.CheckLanguageOperations;
using Orleans;

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly DatabaseContext _dbContext;
        private readonly IClusterClient clusterClient;

        public QuestionsController(IInterpreterAsync interpreter, IClusterClient clusterClient)
        {
            _interpreter = interpreter;
            //_dbContext = dbContext;
            this.clusterClient = clusterClient;
        }
        [HttpPost("createQuestionAgain")]
        public async Task<IActionResult>CreateQuestion()
        {
            var stream = clusterClient.GetStreamProvider("SMSProvider").GetStream<Post>(Guid.Empty, "questions");
            var post = new Post
            {
                PostId = 2,
                PostText = "My question2"
            };

            await stream.OnNextAsync(post);
            return Ok();
        }
        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCmd cmd)
        {
            var dep = new QuestionDependencies();

            var questions = await _dbContext.QuestionModel.ToListAsync();

            // var ctx = new QuestionWriteContext(questions);

            _dbContext.QuestionModel.AttachRange(questions);
            var ctx = new QuestionWriteContext(new EFList<QuestionModel>(_dbContext.QuestionModel));

            var expr = from CreateQuestionResult in QuestionContext.CreateQuestion(cmd)
                       //let checkLanguageCmd = new CheckLanguageCmd()
                       //select CreateQuestionResult;
                       from checkLanguageResult in QuestionContext.CheckLanguage(new CheckLanguageCmd(cmd.Description))
                       from sendAckToQuestionOwnerCmd in QuestionContext.SendQuestionOwnerAcknowledgment(new SendQuestionOwnerAcknowledgementCmd(1, 2))
                       select CreateQuestionResult;

            var r = await _interpreter.Interpret(expr, ctx, dep);


            _dbContext.QuestionModel.Add(new DatabaseModel.Models.QuestionModel {QuestionId = Guid.NewGuid(), Title = cmd.Title, Description= cmd.Description, Tags = cmd.Tags});
            //var reply = await _dbContext.QuestionModel.Where(r => r.Title == "Intrebarea1").SingleOrDefaultAsync();
            //_dbContext.QuestionModel.Update(reply);
            await _dbContext.SaveChangesAsync();

            return r.Match(
                succ => (IActionResult)Ok("Successfully"),
                fail => BadRequest("Reply could not be added")
                );
        }
    }
}
