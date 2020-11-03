using System;
using System.Collections.Generic;
using System.Text;
using L06.Inputs;
using L06.Models;
using Tema6.Inputs;

namespace L06.Outputs
{

    public static partial class CreateReplyResult
    {
        public interface ICreateReplyResult { }

        public class ReplyValid : ICreateReplyResult
        {
            public Reply Reply { get; }

            public ReplyValid(Reply reply)
            {
                Reply = reply;
            }
        }

        public class ReplyInvalid : ICreateReplyResult
        {
            public string Reasons { get; }

            public ReplyInvalid(string reasons)
            {
                Reasons = reasons;
            }
        }

        public interface IValidateReplyResult
        {
        }
        public class InvalidRequest : IValidateReplyResult
        {
            public string ValidationErrors { get; }
            public CreateReplyCmd Cmd { get; }

            public InvalidRequest(string validationErrors, CreateReplyCmd cmd)
            {
                ValidationErrors = validationErrors;
                Cmd = cmd;
            }
        }

    }

}
