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
using System;

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

            var r = await _interpreter.Interpret(expr, ctx, dep);

            await _dbContext.SaveChangesAsync();

            await _dbContext.QuestionModel.Add(new DatabaseModel.Models.QuestionModel {Title = cmd.Title, Description= cmd.Description, Tags = cmd.Tags}).GetDatabaseValuesAsync();
            //var reply = await _dbContext.QuestionModel.Where(r => r.Title == "Intrebarea1").SingleOrDefaultAsync();
            //_dbContext.QuestionModel.Update(reply);

            return r.Match(
                succ => (IActionResult)Ok("Succeeded"),
                fail => BadRequest("Reply could not be added")
                );
        }
    }
}
