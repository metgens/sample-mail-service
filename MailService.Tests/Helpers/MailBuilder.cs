using MailService.Contracts.Enums;
using MailService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Tests.Helpers
{
    public class MailBuilder
    {
        private Guid _id;
        private string _from, _subject, _body;
        private List<string> _to = new List<string>();
        private CustomMailPriority _priority;
        private bool _isHtml;
        private List<MailAttachment> _attcahments = new List<MailAttachment>();
        
        public MailBuilder()
        {
            _id = Guid.NewGuid();
        }

        public MailBuilder SetId(Guid id)
        {
            _id = id;
            return this;
        }

        public MailBuilder SetFrom(string from = "from@mail.com")
        {
            _from = from;
            return this;
        }

        public MailBuilder SetTo(string to = "to@mail.com")
        {
            _to.Add(to);
            return this;
        }

        public MailBuilder SetSubject(string subject = "Test subject")
        {
            _subject = subject;
            return this;
        }

        public MailBuilder SetBody(string body = "Test body")
        {
            _body = body;
            return this;
        }

        public MailBuilder SetIsHtml(bool isHtml = true)
        {
            _isHtml = isHtml;
            return this;
        }

        public MailBuilder SetPriority(CustomMailPriority priority)
        {
            _priority = priority;
            return this;
        }

        public MailBuilder SetDefaults()
        {
            return SetFrom().SetTo().SetSubject().SetBody();
        }

        public MailBuilder AddTextAttachment(string name = "Test name", string content = "Test content", Guid? id = null)
        {
            _attcahments.Add(new MailAttachment(id ?? Guid.NewGuid(), name, content, "utf-8", "text/plain"));

            return this;
        }

        public Mail Build()
        {
            var mail = new Mail(_id, _from, _to, _subject, _body, _isHtml, _priority.ToString());
            foreach (var attachment in _attcahments)
                mail.AddAttachment(attachment.Name, attachment.Content, attachment.Encoding, attachment.MediaType);

            return mail;
        }
    }
}
