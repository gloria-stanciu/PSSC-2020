using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace L06.Inputs
{
    public class SendAckToQuestionOwnerCmd
    {
        [Required]
        public string ReplyId { get; }

        [Required]
        public string QuestionId { get; }

        [Required]
        public string Answer { get; }

        public SendAckToQuestionOwnerCmd(string ReplyId, string QuestionId, string Answer)
        {
            ReplyId = this.ReplyId;
            QuestionId = this.QuestionId;
            Answer = this.Answer;
        }
    }
}