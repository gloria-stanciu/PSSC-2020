using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Question.CheckLanguageOperations;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOperations;
using StackUnderflow.Domain.Core.Contexts.Question.SendQuestionOwnerAcknoledgementOperations;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
using static StackUnderflow.Domain.Core.Contexts.Question.CheckLanguageOperations.CheckLanguageResult;
using static StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOperations.CreateQuestionResult;
using static StackUnderflow.Domain.Core.Contexts.Question.SendQuestionOwnerAcknoledgementCmd.SendQuestionOwnerAcknowledgementResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public static class QuestionContext
    {
        public static Port<ICreateQuestionResult> CreateQuestion(CreateQuestionCmd createQuestionCmd) =>
            NewPort<CreateQuestionCmd, ICreateQuestionResult>(createQuestionCmd);
        public static Port<ICheckLanguageResult> CheckLanguage(CheckLanguageCmd checkLanguageCmd) =>
            NewPort<CheckLanguageCmd, ICheckLanguageResult>(checkLanguageCmd);
        public static Port<ISendQuestionOwnerAcknowledgementResult> SendQuestionOwnerAcknowledgment(SendQuestionOwnerAcknowledgementCmd SendQuestionOwnerAcknowledgmentCmd) =>
            NewPort<SendQuestionOwnerAcknowledgementCmd, ISendQuestionOwnerAcknowledgementResult>(SendQuestionOwnerAcknowledgmentCmd);
    }
}