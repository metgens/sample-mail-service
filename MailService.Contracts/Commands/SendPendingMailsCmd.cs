using MailService.Contracts.Commands.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailService.Contracts.Commands
{
    public class SendPendingMailsCmd : CommandBase
    {
        /// <summary>
        /// Maximum number of pending mails to send when executing this command
        /// </summary>
        public int MaxNumberToSend { get; }

        [JsonConstructor]
        public SendPendingMailsCmd(int maxNumberToSend = 500)
        {
            MaxNumberToSend = maxNumberToSend;
        }
    }
}