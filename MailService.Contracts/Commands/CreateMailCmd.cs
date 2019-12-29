using FluentValidation;
using MailService.Contracts.Commands.Base;
using MailService.Contracts.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MailService.Contracts.Commands
{
    public partial class CreateMailCmd : CommandBase
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
    }

    public class CreateMailCmdValidator : AbstractValidator<CreateMailCmd>
    {
        public CreateMailCmdValidator()
        {
            RuleFor(x => x.From).EmailAddress();
            RuleForEach(x => x.To).EmailAddress();
            RuleFor(x => x.Priority).IsEnumName(typeof(CustomMailPriority), caseSensitive: false);
            RuleForEach(x => x.Attachments).SetValidator(new AddMailAttachmentSimplifiedCmdValidator());
        }
    }
}