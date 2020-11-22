using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.SendQuestionOwnerAcknoledgementOperations
{
    public class SendQuestionOwnerAcknowledgementCmd
    {
        [Required]
        public int QuestionId { get; }
        [Required]
        public int QuestionOwnerId { get; }
        public SendQuestionOwnerAcknowledgementCmd(int questionId, int questionOwnerId)
        {
            QuestionId = questionId;
            QuestionOwnerId = questionOwnerId;
        }
    }
}
