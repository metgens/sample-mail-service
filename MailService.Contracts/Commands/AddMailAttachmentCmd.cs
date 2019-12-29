using FluentValidation;
using MailService.Common.Helpers;
using MailService.Contracts.Commands.Base;
using Newtonsoft.Json;
using System;
using System.Text;

namespace MailService.Contracts.Commands
{
    public class AddMailAttachmentCmd : CommandBase
    {
        public Guid MailId { get; private set; }
        /// <summary>
        /// Attachment file name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Content of the attachment
        /// </summary>
        public string Content { get; }
        /// <summary>
        /// Encoding of mail attachment
        /// </summary>
        /// <seealso cref="https://docs.microsoft.com/pl-pl/dotnet/api/system.text.encoding?view=netframework-4.8"/>
        public string Encoding { get; }
        /// <summary>
        /// Media type names
        /// </summary>
        /// <seealso cref="https://docs.microsoft.com/pl-pl/dotnet/api/system.net.mime.mediatypenames?view=netframework-4.8"/>
        public string MediaType { get; }

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

    public class AddMailAttachmentCmdValidator : AbstractValidator<AddMailAttachmentCmd>
    {
        public AddMailAttachmentCmdValidator()
        {
            RuleFor(x => x.MailId).NotEmpty();
            RuleFor(x => x.Content).NotEmpty().MaximumLength(8000);
            RuleFor(x => x.Encoding).Must(ValidationHelpers.BeValidEncoding).WithMessage($"Unknown encoding.");
            RuleFor(x => x.MediaType).Must(ValidationHelpers.BeValidMediaType).WithMessage($"Unknown media type. " +
                $"Provide one from {string.Join(", ", ValidationHelpers.ValidMediaTypes)}");
        }
    }
}