using MailService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Domain
{
    public class MailAttachment : IEntityWithGuidId
    {
        public Guid Id { get; private set; }
        public Guid MailId { get; private set; }
        public string Name { get; private set; }
        public string Content { get; private set; }
        public string Encoding { get; private set; }
        public string MediaType { get; private set; }

        public MailAttachment (string name, string content, string encoding, string mediaType)
        {
            Name = name;
            Content = content;
            MediaType = mediaType;
            Encoding = encoding;
        }
    }
}
