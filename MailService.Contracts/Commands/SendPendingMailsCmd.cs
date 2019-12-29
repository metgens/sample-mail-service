using MailService.Contracts.Commands.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailService.Contracts.Commands
{
    public class SendPendingMailsCmd : CommandBase
    {
        public int MaxNumberToSend { get; }

        [JsonConstructor]
        public SendPendingMailsCmd(int maxNumberToSend = 500)
        {
            MaxNumberToSend = maxNumberToSend;
        }
    }
}