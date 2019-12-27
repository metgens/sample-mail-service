using MailService.Contracts.Commands.Base;
using Newtonsoft.Json;
using System;

namespace MailService.Contracts.Commands
{
    public class RemoveMailAttachmentCmd : CommandBase
    {
        public Guid MailId { get; }
        public Guid AttachmentId { get; }

        [JsonConstructor]
        public RemoveMailAttachmentCmd(Guid mailId, Guid attachmentId)
        {
            MailId = mailId;
            AttachmentId = attachmentId;
        }
    }
}