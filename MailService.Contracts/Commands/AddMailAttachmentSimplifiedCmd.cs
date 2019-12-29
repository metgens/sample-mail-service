using FluentValidation;
using MailService.Common.Helpers;
using Newtonsoft.Json;

namespace MailService.Contracts.Commands
{
    public class AddMailAttachmentSimplifiedCmd
    {
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
        public AddMailAttachmentSimplifiedCmd(string name, string content, string encoding, string mediaType)
        {
            Name = name;
            Content = content;
            Encoding = encoding;
            MediaType = mediaType;
        }

    }

    public class AddMailAttachmentSimplifiedCmdValidator : AbstractValidator<AddMailAttachmentSimplifiedCmd>
    {
        public AddMailAttachmentSimplifiedCmdValidator()
        {
            RuleFor(x => x.Content).NotEmpty().MaximumLength(8000);
            RuleFor(x => x.Encoding).Must(ValidationHelpers.BeValidEncoding).WithMessage($"Unknown encoding.");
            RuleFor(x => x.MediaType).Must(ValidationHelpers.BeValidMediaType).WithMessage($"Unknown media type. " +
                $"Provide one from {string.Join(", ", ValidationHelpers.ValidMediaTypes)}");
        }
    }
}