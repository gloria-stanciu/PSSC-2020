using Access.Primitives.IO;
using static PortExt;
using Tema6.Inputs;
using L06.Outputs;
using L06.Inputs;
using Tema6.Outputs;

namespace L06
{
    public static class Domain
    {
        public static Port<CreateReplyResult.ICreateReplyResult> ValidateReply(string questionId, string userId, string answer)
            => NewPort<CreateReplyCmd, CreateReplyResult.ICreateReplyResult>(new CreateReplyCmd(userId, questionId, answer));

        public static Port<CheckLanguageResult.ICheckLanguageResult> CheckLanguage(string text)
            => NewPort<CheckLanguageCmd, CheckLanguageResult.ICheckLanguageResult>(new CheckLanguageCmd(text));

        public static Port<SendAckToQuestionOwnerResult.ISendAckToQuestionOwnerResult> SendAckToQuestionOwner(string replyid, string questionid, string answer)
       => NewPort<SendAckToQuestionOwnerCmd, SendAckToQuestionOwnerResult.ISendAckToQuestionOwnerResult>(new SendAckToQuestionOwnerCmd(replyid, questionid, answer));

        public static Port<SendAckToReplyAuthorResult.ISendAskToReplyAuthorResult> SendAckToReplyAuthor(string replyid, string questionid, string answer)
        => NewPort<SendAckToReplyAuthorCmd, SendAckToReplyAuthorResult.ISendAskToReplyAuthorResult>(new SendAckToReplyAuthorCmd(replyid, questionid, answer));


    }
}
