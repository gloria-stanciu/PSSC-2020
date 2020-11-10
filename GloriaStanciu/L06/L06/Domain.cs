using Access.Primitives.IO;
using static PortExt;
using L06.Inputs;
using L06.Outputs;
using LanguageExt;

namespace L06
{
    public static class Domain
    {
        public static Port<CreateReplyResult.ICreateReplyResult> ValidateReply(int questionId, int userId, string answer)
            => NewPort<CreateReplyCmd, CreateReplyResult.ICreateReplyResult>(new CreateReplyCmd(userId, questionId, answer));

        public static Port<CheckLanguageResult.ICheckLanguageResult> CheckLanguage(string text)
            => NewPort<CheckLanguageCmd, CheckLanguageResult.ICheckLanguageResult>(new CheckLanguageCmd(text));

        public static Port<SendAckToQuestionOwnerResult.ISendAckToQuestionOwnerResult> SendAckToQuestionOwner(int replyid, int questionid, string answer)
       => NewPort<SendAckToQuestionOwnerCmd, SendAckToQuestionOwnerResult.ISendAckToQuestionOwnerResult>(new SendAckToQuestionOwnerCmd(replyid, questionid, answer));

        public static Port<SendAckToReplyAuthorResult.ISendAskToReplyAuthorResult> SendAckToReplyAuthor(int replyid, int questionid, string answer)
        => NewPort<SendAckToReplyAuthorCmd, SendAckToReplyAuthorResult.ISendAskToReplyAuthorResult>(new SendAckToReplyAuthorCmd(replyid, questionid, answer));

        public static Port<Unit> SendAckToOwner(object safeReply) => NewPort<Unit, Unit>(Unit.Default);

        public static Port<Unit> SendAckToAuthor(object problematicReply) => NewPort<Unit, Unit>(Unit.Default);
    }
}
