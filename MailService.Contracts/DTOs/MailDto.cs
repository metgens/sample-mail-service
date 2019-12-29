using System;
using System.Collections.Generic;

namespace MailService.Contracts.DTOs
{
    public class MailDto
    {
        /// <summary>
        /// Mail Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Sender mail address
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Collection of recipients' mail addresses
        /// </summary>
        public List<string> To { get; set; }
        /// <summary>
        /// Mail subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Body of the mail. It can be plain text or html
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// When body is html it should be set true
        /// </summary>
        public bool IsHtml { get; set; }
        /// <summary>
        /// Mail priority, possible values: 'Normal', 'Low', 'High'. Defualt is 'Normal'
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// Mail status, possible values: 'Pending' - ready to send, 'Draft' - incomplete, 'Sent', 'Rejected' - delivery failed.
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Date when mail was created
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }
        /// <summary>
        /// Delivery date
        /// </summary>
        public DateTimeOffset? SentDate { get; set; }
        /// <summary>
        /// Attachments
        /// </summary>
        public List<MailAttachmentWithoutContentDto> Attachments { get; set; }
    }
}