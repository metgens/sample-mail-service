using MailService.Contracts.Commands.Base;
using Newtonsoft.Json;
using System;

namespace MailService.Contracts.Commands
{
    public class AddMailAttachmentCmd : CommandBase
    {
        public Guid MailId { get; private set; }
        public string Name { get; }
        public string Content { get; }
        public string MediaType { get; }
        public string Encoding { get; }

        [JsonConstructor]
        public AddMailAttachmentCmd(Guid mailId, string name, string content, string encoding, string mediaType)
        {
            MailId = mailId;
            Name = name;
            Content = content;
            Encoding = encoding;
            MediaType = mediaType;
        }

        public void SetMailId(Guid mailId)
        {
            MailId = mailId;
        }
    }
}