using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Tema6.Inputs
{
    public class SendAckToReplyAuthorCmd
    {
        [Required]
        public string ReplyId { get; }

        [Required]
        public string QuestionId { get; }

        [Required]
        public string Answer { get; }

        public SendAckToReplyAuthorCmd(string ReplyId, string QuestionId, string Answer)
        {
            ReplyId = this.ReplyId;
            QuestionId = this.QuestionId;
            Answer = this.Answer;
        }
    }
}