using L06.Outputs;
using System;

namespace L06
{
    class Program
    {
        public static void Main(string[] args)
        {
            var wf = from createReplyResult in Domain.ValidateReply("1", "1", "test")
                     let validReply = (CreateReplyResult.ReplyValid)createReplyResult
                     from checkLanguageResult in Domain.CheckLanguage(validReply.Reply.Answer)
                     from ownerAck in Domain.SendAckToQuestionOwner("1", "1", "test")
                     from authorAck in Domain.SendAckToReplyAuthor("1", "1", "test")
                     select (validReply, checkLanguageResult, ownerAck, authorAck);


            Console.WriteLine("Hello World!");
        }

    }

    internal interface IReplyPosted
    {
    }
}