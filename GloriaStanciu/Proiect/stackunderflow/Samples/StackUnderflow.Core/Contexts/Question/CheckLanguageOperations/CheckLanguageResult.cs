using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CheckLanguageOperations
{
    [AsChoice]
    public static partial class CheckLanguageResult
    {
        public interface ICheckLanguageResult { }
        public class ValidationSucceeded : ICheckLanguageResult
        {
            public string Text { get; }
            public ValidationSucceeded(string text)
            {
                Text = text;
            }
        }
        public class ValidationFailed : ICheckLanguageResult
        {
            public string Message { get; }
            public ValidationFailed(string message)
            {
                Message = message;
            }
        }
        public class ManualReviewRequired : ICheckLanguageResult
        {
            public string Text { get; }
            public ManualReviewRequired(string text)
            {
                Text = text;
            }
        }
    }
}
