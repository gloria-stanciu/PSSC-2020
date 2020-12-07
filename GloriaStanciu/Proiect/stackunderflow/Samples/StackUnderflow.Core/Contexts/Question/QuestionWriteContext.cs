using StackUnderflow.DatabaseModel.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionWriteContext
    {
        public ICollection<QuestionModel> Questions { get; }
        public QuestionWriteContext(ICollection<QuestionModel> questions)
        {
            Questions = questions ?? new List<QuestionModel>();
        }
    }
}
