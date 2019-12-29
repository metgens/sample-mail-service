using FluentValidation;
using MailService.Common.Helpers;
using Newtonsoft.Json;

namespace MailService.Contracts.Commands
{
    public class AddMailAttachmentSimplifiedCmd
    {
        public string Name { get; }
        public string Content { get; }
        public string Encoding { get; }
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