using MailService.Contracts.Commands.Base;
using MailService.Contracts.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailService.Contracts.Commands
{
    public class EditMailCmd : CommandBase
    {
        public Guid Id { get; }
        public string From { get; }
        public List<string> To { get; }
        public string Subject { get; }
        public string Body { get; }
        public bool IsBodyHtml { get; }
        public CustomMailPriority? Priority { get; }

        [JsonConstructor]
        public EditMailCmd(Guid id, string from, List<string> to, string subject, string body, bool isBodyHtml,
            CustomMailPriority? priority = null)
        {
            Id = id;
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            IsBodyHtml = isBodyHtml;
            Priority = priority;
        }
    }
}