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
    public class MailAttachmentTests
    {

        private string _name, _content, _encoding, _mediaType;

        [SetUp]
        public void Setup()
        {
            _name = "Test name";
            _content = "Test content";
            _encoding = "utf-8";
            _mediaType = "text/plain";
        }

        [Test]
        public void For_AddingTwice_Should_HaveTwoAttachments()
        {
            //arrange
            var mail = new MailBuilder().SetDefaults().Build();

            //act
            mail.AddAttachment(_name + " 1", _content + " 1", _encoding, _mediaType);
            mail.AddAttachment(_name + " 2", _content + " 2", _encoding, _mediaType);

            //assert
            mail.Attachments.Count.Should().Be(2);
            mail.Attachments[0].Name.Should().Be("Test name 1");
            mail.Attachments[0].Content.Should().Be("Test content 1");
            mail.Attachments[0].Encoding.Should().Be("utf-8");
            mail.Attachments[0].MediaType.Should().Be("text/plain");
            mail.Attachments[1].Name.Should().Be("Test name 2");
            mail.Attachments[1].Content.Should().Be("Test content 2");
            mail.Attachments[1].Encoding.Should().Be("utf-8");
            mail.Attachments[1].MediaType.Should().Be("text/plain");
        }

        [Test]
        public void For_AddingToSentMail_Should_ThrowEx()
        {
            //arrange
            var mail = new MailBuilder().SetDefaults().Build();
            mail.MarkAsSent();

            //act
            Action act = () => mail.AddAttachment(_name + " 1", _content + " 1", _encoding, _mediaType);
            
            //assert
            act.Should().Throw<AppException>();
        }

        [Test]
        public void For_Removing_Should_LeftZeroAttachments()
        {
            //arrange
            var mail = new MailBuilder().SetDefaults()
                .AddTextAttachment()
                .Build();

            var attchamentId = mail.Attachments[0].Id;

            //act
            mail.RemoveAttachment(attchamentId);

            //assert
            mail.Attachments.Count.Should().Be(0);
        }

        [Test]
        public void For_RemovingFromSentMail_Should_ThrowEx()
        {
            //arrange
            var mail = new MailBuilder().SetDefaults()
                .AddTextAttachment()
                .Build();
            mail.MarkAsSent();

            var attchamentId = mail.Attachments[0].Id;

            //act
            Action act = () => mail.RemoveAttachment(attchamentId);

            //assert
            act.Should().Throw<AppException>();
        }
    }
}
