using Access.Primitives.IO;
using GrainInterfaces;
using Orleans;
using System;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Question.SendQuestionOwnerAcknoledgementCmd.SendQuestionOwnerAcknowledgementResult;

namespace StackUnderflow.Domain.Core.Contexts.Question.SendQuestionOwnerAcknoledgementOperations
{
    class SendQuestionOwnerAcknowledgementAdaptor : Adapter<SendQuestionOwnerAcknowledgementCmd, ISendQuestionOwnerAcknowledgementResult, QuestionWriteContext, QuestionDependencies>
    {
        private readonly IClusterClient clusterClient;
        public SendQuestionOwnerAcknowledgementAdaptor(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }
        public override Task PostConditions(SendQuestionOwnerAcknowledgementCmd cmd, ISendQuestionOwnerAcknowledgementResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ISendQuestionOwnerAcknowledgementResult> Work(SendQuestionOwnerAcknowledgementCmd cmd, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var asyncHelloGrain = this.clusterClient.GetGrain<IAsyncHello>("User1");
            await asyncHelloGrain.StartAsync();

            var stream = clusterClient.GetStreamProvider("SMSProvider").GetStream<string>(Guid.Empty, "chat");
            await stream.OnNextAsync("email@email.com");

            return new AcknowledgementSent(1, 2);
        }
    }
}
