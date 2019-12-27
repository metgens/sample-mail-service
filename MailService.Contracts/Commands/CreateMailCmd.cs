using MailService.Contracts.Commands.Base;
using MailService.Contracts.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MailService.Contracts.Commands
{
    public class CreateMailCmd : CommandBase
    {
        public string From { get; }
        public List<string> To { get; }
        public string Subject { get; }
        public string Body { get; }
        public bool IsHtml { get; }
        public CustomMailPriority? Priority { get; }

        public List<AddMailAttachmentSimplifiedCmd> Attachments { get; }

        [JsonConstructor]
        public CreateMailCmd(string from, List<string> to, string subject, string body, bool isHtml,
            CustomMailPriority? priority = null, List<AddMailAttachmentSimplifiedCmd> attachments = null)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;

            Priority = priority;
            Attachments = attachments ?? new List<AddMailAttachmentSimplifiedCmd>();
        }

        public class AddMailAttachmentSimplifiedCmd
        {
            public string Name { get; }
            public string Content { get; }
            public string ContentType { get; }


            [JsonConstructor]
            public AddMailAttachmentSimplifiedCmd(string name, string content, string contentType)
            {
                Name = name;
                Content = content;
                ContentType = contentType;
            }

        }
    }
}