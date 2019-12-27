﻿using MailService.Contracts.Commands.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailService.Contracts.Commands
{
    public class SendMailCmd : CommandBase
    {
        public Guid MailId { get; }
       
        [JsonConstructor]
        public SendMailCmd(Guid mailId)
        {
            MailId = mailId;
        }
    }
}