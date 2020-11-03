using System;
using System.Collections.Generic;
using System.Text;
using CSharp.Choices;

namespace L06.Outputs
{
    [AsChoice]
    public static partial class CheckLanguageResult
    {
        public interface ICheckLanguageResult { }

        public class SafeText : ICheckLanguageResult
        {
            public int Certainty { get; private set; }

            public void TextChecked(int certainty)
            {
                Certainty = certainty;
            }
        }

        public class NotSafeText
        {
            public string ErrorMessage { get; private set; }

            public void CheckFailed(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
        }
    }
}
