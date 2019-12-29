using FluentValidation;
using MailService.Contracts.Commands.Base;
using MailService.Contracts.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MailService.Contracts.Commands
{
    public partial class CreateMailCmd : CommandBase
    {
        /// <summary>
        /// Id of mail, set automatically
        /// </summary>
        public Guid MailId { get; }
        /// <summary>
        /// Sender mail address
        /// </summary>
        public string From { get; }
        /// <summary>
        /// Collection of recipients' mail addresses
        /// </summary>
        public List<string> To { get; }
        /// <summary>
        /// Mail subject
        /// </summary>
        public string Subject { get; }
        /// <summary>
        /// Body of the mail. It can be plain text or html
        /// </summary>
        public string Body { get; }
        /// <summary>
        /// When body is html it should be set true
        /// </summary>
        public bool IsHtml { get; }
        /// <summary>
        /// Mail priority, possible values: 'Normal', 'Low', 'High'. Defualt is 'Normal'
        /// </summary>
        public string Priority { get; }
        /// <summary>
        /// Collection of mail attachments
        /// </summary>
        public List<AddMailAttachmentSimplifiedCmd> Attachments { get; }

        [JsonConstructor]
        public CreateMailCmd(string from, List<string> to, string subject, string body, bool isHtml,
            string priority = null, List<AddMailAttachmentSimplifiedCmd> attachments = null)
        {
            MailId = Guid.NewGuid();
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;

            Priority = priority;
            Attachments = attachments ?? new List<AddMailAttachmentSimplifiedCmd>();
        }
    }

    public class CreateMailCmdValidator : AbstractValidator<CreateMailCmd>
    {
        public CreateMailCmdValidator()
        {
            RuleFor(x => x.From).EmailAddress();
            RuleForEach(x => x.To).EmailAddress();
            RuleFor(x => x).Must(x => x.From != null || x.To != null || x.Body != null || x.Subject != null).WithMessage("Cannot save empty object.");
            RuleFor(x => x.Priority).IsEnumName(typeof(CustomMailPriority), caseSensitive: false);
            RuleForEach(x => x.Attachments).SetValidator(new AddMailAttachmentSimplifiedCmdValidator());
        }
    }
}