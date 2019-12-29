using AutoMapper;
using FluentAssertions;
using MailService.Common.Exceptions;
using MailService.Contracts.Enums;
using MailService.Domain;
using MailService.Domain.Handlers;
using MailService.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailService.Tests.Domain
{
    [TestFixture]
    public class MailCreateTests
    {
        private Guid _id;
        private string _from, _body, _subject, _priority;
        private List<string> _to;
        private bool _isHtml;

        [SetUp]
        public void Setup()
        {
            _id = Guid.NewGuid();
            _from = "from-after-edit@mail.com";
            _to = new List<string>() { "to-after-edit-1@mail.com", "to-after-edit-2@mail.com" };
            _subject = "Test subject after edit";
            _body = "Test body after edit";
            _isHtml = true;
            _priority = CustomMailPriority.High.ToString();
        }

        [Test]
        public void For_Creating_Should_ReturnObjWithAllProperties()
        {
            //arrange

            //act
            var mail = new Mail(_id, _from, _to, _subject, _body, _isHtml, _priority);

            //assert
            mail.From.Should().Be(_from);
            mail.To.Should().BeEquivalentTo(_to);
            mail.Subject.Should().Be(_subject);
            mail.Body.Should().Be(_body);
            mail.IsHtml.Should().Be(_isHtml);
            mail.Priority.ToString().Should().Be(_priority);
        }

        [Test]
        public void For_CreatingWithFromAndTo_Should_HavePendingStatus()
        {
            //arrange

            //act
            var mail = new Mail(_id, _from, _to, _subject, _body, _isHtml, _priority);

            //assert
            mail.Status.Should().Be(MailStatus.Pending);
        }

        [Test]
        public void For_CreatingWithEmptyFrom_Should_HaveDrafftStatus()
        {
            //arrange
            _from = null;

            //act
            var mail = new Mail(_id, _from, _to, _subject, _body, _isHtml, _priority);

            //assert
            mail.Status.Should().Be(MailStatus.Draft);
        }

        [Test]
        public void For_CreatingWithEmptyTo_Should_HaveDrafftStatus()
        {
            //arrange
            _to = new List<string>();

            //act
            var mail = new Mail(_id, _from, _to, _subject, _body, _isHtml, _priority);

            //assert
            mail.Status.Should().Be(MailStatus.Draft);
        }
    }
}
