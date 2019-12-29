using FluentAssertions;
using MailService.Common.Bus.Event;
using MailService.Contracts.Commands;
using MailService.Contracts.Enums;
using MailService.Contracts.Events;
using MailService.Domain;
using MailService.Domain.Handlers;
using MailService.Domain.MailSenders;
using MailService.Domain.Repositories;
using MailService.Domain.RetryPolicy;
using MailService.Tests.Helpers;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Tests.Handlers
{
    [TestFixture]
    public class MailSenderFacadeTests : AutoMockerTests
    {

        private Mock<IMailSenderFactory> _mailSenderFactoryMock;
        private Mock<IMailSender> _mailSenderMock;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            _mailSenderFactoryMock = Mocker.GetMock<IMailSenderFactory>();
            _mailSenderMock = Mocker.GetMock<IMailSender>();

            var retryPolicyFactory = Mocker.GetMock<IRetryPolicyFactory>();
            retryPolicyFactory.Setup(x => x.GetForSendingMails()).Returns(Policy.NoOpAsync()); //disable polly polices
        }

        [Test]
        public async Task For_ProperData_Should_MarkMailAsSent()
        {
            //arrange
            var mail = new MailBuilder().SetDefaults().Build();
            _mailSenderFactoryMock.Setup(x => x.GetMailSender()).Returns(_mailSenderMock.Object);

            var target = Mocker.CreateInstance<MailSenderFacade>();

            //act
            await target.SendMailAsync(mail);

            //assert
            mail.Status.Should().Be(MailStatus.Sent);
            mail.SentDate.Should().NotBeNull();
        }

        [Test]
        public async Task For_ProperData_Should_ReturnEventWithSentNo1()
        {
            //arrange
            var mail = new MailBuilder().SetDefaults().Build();
            _mailSenderFactoryMock.Setup(x => x.GetMailSender()).Returns(_mailSenderMock.Object);

            var target = Mocker.CreateInstance<MailSenderFacade>();

            //act
            var result = await target.SendMailAsync(mail);

            //assert
            result.MailsSentNo.Should().Be(1);
            result.MailsRejectedNo.Should().Be(0);
        }


        [Test]
        public async Task For_MailSenderError_Should_MarkMailAsRejected()
        {
            //arrange
            var mail = new MailBuilder().SetDefaults().Build();
            _mailSenderMock.Setup(x => x.SendMailAsync(It.IsAny<Mail>())).Throws(new Exception()); //fail
            _mailSenderFactoryMock.Setup(x => x.GetMailSender()).Returns(_mailSenderMock.Object);

            var target = Mocker.CreateInstance<MailSenderFacade>();

            //act
            await target.SendMailAsync(mail);

            //assert
            mail.Status.Should().Be(MailStatus.Rejected);
            mail.SentDate.Should().BeNull();
        }

        [Test]
        public async Task For_MailSenderError_Should_ReturnEventWithRejectedNo1()
        {
            //arrange
            var mail = new MailBuilder().SetDefaults().Build();
            _mailSenderMock.Setup(x => x.SendMailAsync(It.IsAny<Mail>())).Throws(new Exception("Test msg")); //fail
            _mailSenderFactoryMock.Setup(x => x.GetMailSender()).Returns(_mailSenderMock.Object);

            var target = Mocker.CreateInstance<MailSenderFacade>();

            //act
            var result = await target.SendMailAsync(mail);

            //assert
            result.MailsSentNo.Should().Be(0);
            result.MailsRejectedNo.Should().Be(1);
            result.RejectionReasons.Count.Should().Be(1);
            result.RejectionReasons.First().Value.Should().Be("Test msg");

        }

    }
}
