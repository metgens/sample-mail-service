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

        public string Priority { get; }

        public List<AddMailAttachmentSimplifiedCmd> Attachments { get; }

        [JsonConstructor]
        public CreateMailCmd(string from, List<string> to, string subject, string body, bool isHtml,
            string priority = null, List<AddMailAttachmentSimplifiedCmd> attachments = null)
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
            public string Encoding { get; }
            public string MediaType { get; }
            
            [JsonConstructor]
            public AddMailAttachmentSimplifiedCmd(string name, string content, string encoding, string mediaType)
            {
                Name = name;
                Content = content;
                Encoding = encoding;
                MediaType = mediaType;
            }

        }
    }
}