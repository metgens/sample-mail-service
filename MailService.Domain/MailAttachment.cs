using MailService.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Domain
{
    public class MailAttachment : IEntityWithGuidId
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Content { get; private set; }
        public string ContentType { get; private set; }

        public Mail Mail { get; private set; }

        public MailAttachment (string name, string content, string contentType)
        {
            Name = name;
            Content = content;
            ContentType = contentType;
        }
    }
}
