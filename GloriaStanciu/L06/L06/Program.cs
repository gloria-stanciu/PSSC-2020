using L06.Outputs;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using Access.Primitives.IO;
using Microsoft.Extensions.DependencyInjection;
using L06.Adapters;

namespace L06
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var wf = from createReplyResult in Domain.ValidateReply(100, 1, "123hndbfjshdfjhsgdfhjsdgfhj")
                     let validReply = (CreateReplyResult.ReplyValid)createReplyResult
                     from checkLanguageResult in Domain.CheckLanguage(validReply.Reply.Answer)
                     from ownerAck in Domain.SendAckToOwner(checkLanguageResult)
                     from authorAck in Domain.SendAckToAuthor(checkLanguageResult)
                     select (validReply, checkLanguageResult, ownerAck, authorAck);

            var serviceProvider = new ServiceCollection()
                .AddOperations(typeof(ValidateReplyAdapter).Assembly)
                .AddTransient<IInterpreterAsync>(sp => new LiveInterpreterAsync(sp))
                .BuildServiceProvider();

            var interpreter = serviceProvider.GetService<IInterpreterAsync>();

            var writeContext = new QuestionWriteContext(new List<int>() { 1, 2, 3 }, new List<int>() { 100, 101, 102 });
            var result = await interpreter.Interpret(wf, writeContext);

            Console.WriteLine("Hello World!");
        }

    }
}