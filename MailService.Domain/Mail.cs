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
        public MailStatus Status { get; private set; }
        public DateTimeOffset? SentDate { get; private set; }
        public List<MailAttachment> Attachments { get; private set; } = new List<MailAttachment>();
        private bool _isADraft => string.IsNullOrEmpty(From) || To == null || To.Count == 0;

        public Mail() { }

        public Mail(string from, List<string> to, string subject, string body, bool isHtml, string priority = null)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;

            InvalidateStatus();

            if (!(priority is null))
                ChangePriority(priority);
        }

        public void Edit(string from, List<string> to, string subject, string body, bool isHtml, string priority = null)
        {
            CheckRule(new OnlyNotSentMailsCanBeEdited(this));

            From = from;
            To = to;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;

            InvalidateStatus();
                      
            if (!(priority is null))
                ChangePriority(priority);

            SetUpdatedDate();
        }

        public void MarkAsSent()
        {
            Status = MailStatus.Sent;
            SentDate = SystemTimeOffset.UtcNow;
            SetUpdatedDate();
        }

        public void MarkAsRejected()
        {
            Status = MailStatus.Rejected;
            SetUpdatedDate();
        }
               
        public void AddAttachment(string name, string content, string encoding, string mediaType)
        {
            CheckRule(new OnlyNotSentMailsCanBeEdited(this));

            Attachments.Add(new MailAttachment(name, content, encoding, mediaType));
            SetUpdatedDate();
        }

        public void RemoveAttachment(Guid attachmentId)
        {
            CheckRule(new OnlyNotSentMailsCanBeEdited(this));

            var attachmentToRemove = Attachments.FirstOrDefault(x => x.Id == attachmentId);

            if (attachmentToRemove is null)
                throw AppException.NotExisting(typeof(MailAttachment).Name, Id, attachmentId);

            Attachments.Remove(attachmentToRemove);
            SetUpdatedDate();
        }

        private void InvalidateStatus()
        {
            if (_isADraft)
                Status = MailStatus.Draft;
            else
                Status = MailStatus.Pending;
        }

        private void ChangePriority(string priority)
        {
            if (!Enum.TryParse(priority, out CustomMailPriority parsedPriority))
                throw new AppException("Incorrect 'priority' value", $"Incorrect priority value '{priority}'. Change failed for mail with id: {Id}", ErrorCode.BadRequest);

            Priority = parsedPriority;
        }
    }
}
