using MailService.Contracts.Enums;
using MailService.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Tests.Helpers
{
    public class MailBuilder
    {

        private string _from, _subject, _body;
        private List<string> _to = new List<string>();
        private CustomMailPriority _priority;
        private bool _isHtml;

        public MailBuilder()
        {
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

        public Mail Build()
        {
            return new Mail(_from, _to, _subject, _body, _isHtml, _priority.ToString());
        }

        public Mail BuildDefault()
        {
            return SetFrom().SetTo().SetSubject().SetBody().Build();
        }
    }
}
