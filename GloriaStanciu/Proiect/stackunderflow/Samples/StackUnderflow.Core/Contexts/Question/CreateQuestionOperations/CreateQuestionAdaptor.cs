using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Question;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOperations.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOperations
{
    class CreateQuestionAdaptor : Adapter<CreateQuestionCmd, ICreateQuestionResult, QuestionWriteContext, QuestionDependencies>
    {
        public override Task PostConditions(CreateQuestionCmd cmd, ICreateQuestionResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ICreateQuestionResult> Work(CreateQuestionCmd cmd, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = AddQuestion(state, CreateQuestionFromCmd(cmd))
                           select t;
            var result = await workflow.Match(
                Succ: r => r,
                Fail: er => new QuestionNotCreated(er.Message)
                );

            return result;
        }

        private ICreateQuestionResult AddQuestion(QuestionWriteContext state, object v)
        {
            return new QuestionCreated(1, "Titlu1", "Descriere pentru prima intrebare", "Tag-uri");
        }

        private object CreateQuestionFromCmd(CreateQuestionCmd cmd)
        {
            return new { };
        }
    }
}
