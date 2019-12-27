using MailService.Contracts.Commands.Base;
using Newtonsoft.Json;
using System;

namespace MailService.Contracts.Commands
{
    public class AddMailAttachmentCmd : CommandBase
    {
        public Guid MailId { get; }
        public string Name { get; }
        public string Content { get; }
        public string ContentType { get; }

        [JsonConstructor]
        public AddMailAttachmentCmd(Guid mailId, string name, string content, string contentType)
        {
            MailId = mailId;
            Name = name;
            Content = content;
            ContentType = contentType;
        }

    }
}