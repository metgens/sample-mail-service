using MailService.Common.Dates;
using MailService.Common.Exceptions;
using MailService.Contracts.Enums;
using MailService.Domain.Base;
using MailService.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailService.Domain
{
    public class Mail : EntityBase, IEntityWithGuidId
    {
        public Guid Id { get; private set; }
        public string From { get; private set; }
        public List<string> To { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public bool IsHtml { get; private set; }
        public CustomMailPriority Priority { get; private set; } = CustomMailPriority.Normal;
        public MailStatus Status { get; private set; } = MailStatus.Pending;
        public DateTimeOffset? SentDate { get; private set; }
        public List<MailAttachment> Attachments { get; private set; } = new List<MailAttachment>();
        
        public Mail() { }

        public Mail(string from, List<string> to, string subject, string body, bool isHtml, string priority = null)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;

            if (!(priority is null))
                ChangePriority(priority);
        }

        public void Edit(string from, List<string> to, string subject, string body, bool isHtml, string priority = null)
        {
            CheckRule(new OnlyPendingMailCanBeEdited(this));

            From = from;
            To = to;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;

            SetUpdatedDate();

            if (!(priority is null))
                ChangePriority(priority);
        }

        public void MarkAsSent(DateTimeOffset? date = null)
        {
            Status = MailStatus.Sent;
            SentDate = date ?? SystemTimeOffset.UtcNow;
            SetUpdatedDate();
        }

        public void MarkAsRejected(DateTimeOffset? date = null)
        {
            Status = MailStatus.Rejected;
            SetUpdatedDate();
        }

        public void MarkAsToRetry(DateTimeOffset? date = null)
        {
            Status = MailStatus.Pending;
            SetUpdatedDate();
        }

        public void AddAttachment(string name, string content, string encoding, string mediaType)
        {
            CheckRule(new OnlyPendingMailCanBeEdited(this));

            Attachments.Add(new MailAttachment(name, content, encoding, mediaType));
            SetUpdatedDate();
        }

        public void RemoveAttachment(Guid attachmentId)
        {
            CheckRule(new OnlyPendingMailCanBeEdited(this));

            var attachmentToRemove = Attachments.FirstOrDefault(x => x.Id == attachmentId);

            if (attachmentToRemove is null)
                throw AppException.NotExisting(typeof(MailAttachment).Name, Id, attachmentId);

            Attachments.Remove(attachmentToRemove);
            SetUpdatedDate();
        }

        public void ChangePriority(string priority)
        {
            CheckRule(new OnlyPendingMailCanBeEdited(this));

            if (!Enum.TryParse(priority, out CustomMailPriority parsedPriority))
                throw new AppException("Incorrect 'priority' value", $"Incorrect priority value '{priority}'. Change failed for mail with id: {Id}", ErrorCode.BadRequest);

            Priority = parsedPriority;
            SetUpdatedDate();
        }
    }
}
