using System;
using System.Collections.Generic;

namespace MailService.Contracts.DTOs
{
    public class MailDto
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? SentDate { get; set; }
        public List<MailAttachmentWithoutContentDto> Attachments { get; set; }
    }
}