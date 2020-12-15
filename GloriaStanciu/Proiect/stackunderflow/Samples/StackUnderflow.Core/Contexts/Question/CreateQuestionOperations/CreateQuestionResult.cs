using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOperations
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult { }
        
        public class QuestionCreated : ICreateQuestionResult
        {
            public int QuestionId { get; private set; }
            public string Title { get; private set; }
            public string Description { get; private set; }
            public string Tags { get; private set; }

            public QuestionCreated(int QuestionId, string Title, string Description, string Tags)
            {
                this.QuestionId = QuestionId;
                this.Title = Title;
                this.Description = Description;
                this.Tags = Tags;
            }
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Message{ get; }
            public QuestionNotCreated(string message)
            {
                Message = message;
            }
        }
    }
}
