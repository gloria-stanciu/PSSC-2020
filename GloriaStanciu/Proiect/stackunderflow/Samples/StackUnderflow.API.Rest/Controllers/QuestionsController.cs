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

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly DatabaseContext _dbContext;

        public QuestionsController(IInterpreterAsync interpreter, DatabaseContext dbContext)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;

        }
        [HttpPost("createQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCmd cmd)
        {
            var dep = new QuestionDependencies();

            var questions = await _dbContext.QuestionModel.ToListAsync();

            var ctx = new QuestionWriteContext(questions);

            var expr = from CreateQuestionResult in QuestionContext.CreateQuestion(cmd)
                       select CreateQuestionResult;
                       //from checkLanguageResult in QuestionContext.CheckLanguage(new CheckLanguageCmd(cmd.Body))
                       //from sendAckToQuestionOwnerCmd in QuestionContext.SendAckToQuestionOwner(new SendAckToQuestionOwnerCmd(1, 2))
                       //select createQuestionResult;

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
