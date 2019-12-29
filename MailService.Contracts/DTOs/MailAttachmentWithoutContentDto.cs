using System;

namespace MailService.Contracts.DTOs
{
    public class MailAttachmentWithoutContentDto
    {
        /// <summary>
        /// Attachment Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Attachment file name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Attachment media type
        /// </summary>
        public string MediaType { get; set; }
        /// <summary>
        /// Mail content encoding
        /// </summary>
        public string Encoding { get; set; }
    }
}