using GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class EmailSenderGrain : Orleans.Grain, IEmailSender
    {
        public Task<string> SendEmailAsync(string message)
        {
            return Task.FromResult(message);
        }
    }
}
