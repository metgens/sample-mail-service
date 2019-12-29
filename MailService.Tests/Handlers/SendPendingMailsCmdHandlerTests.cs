using FluentAssertions;
using MailService.Common.Bus.Event;
using MailService.Contracts.Commands;
using MailService.Contracts.Enums;
using MailService.Contracts.Events;
using MailService.Domain;
using MailService.Domain.Handlers;
using MailService.Domain.MailSenders;
using MailService.Domain.Repositories;
using MailService.Tests.Helpers;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Tests.Handlers
{
    [TestFixture]
    public class SendPendingMailsCmdHandlerTests : AutoMockerTests
    {
        private Mock<IMailWriteRepository> _mailWriteRepositoryMock;
        private Mock<IMailSenderFactory> _mailSenderFactoryMock;
        private Mock<IMailSender> _mailSenderMock;
        private Mock<IEventBus> _eventBusMock;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            _mailWriteRepositoryMock = Mocker.GetMock<IMailWriteRepository>();
            _mailSenderFactoryMock = Mocker.GetMock<IMailSenderFactory>();
            _mailSenderMock = Mocker.GetMock<IMailSender>();
            _eventBusMock = Mocker.GetMock<IEventBus>();

        }

        [Test]
        public async Task For_ProperData_Should_MarkMailAsSent()
        {
            //arrange
            var mb = new MailBuilder();
            var mails = new List<Mail> { mb.SetDefaults().Build(), mb.SetDefaults().Build() };

            _mailWriteRepositoryMock.Setup(x => x.GetPendingAsync(It.IsAny<int>())).ReturnsAsync(mails);
            _mailSenderFactoryMock.Setup(x => x.GetMailSender()).Returns(_mailSenderMock.Object);
            var target = Mocker.CreateInstance<SendPendingMailsCmdHandler>();
            var request = new SendPendingMailsCmd();

            //act
            await target.Handle(request, CancellationToken.None);

            //assert
            mails.All(x => x.Status == MailStatus.Sent).Should().BeTrue();
            mails.All(x => x.SentDate != null).Should().BeTrue();
        }

        [Test]
        public async Task For_ProperData_Should_PublishEventWithSentNoEquals2()
        {
            //arrange
            var mb = new MailBuilder();
            var mails = new List<Mail> { mb.SetDefaults().Build(), mb.SetDefaults().Build() };

            _mailWriteRepositoryMock.Setup(x => x.GetPendingAsync(It.IsAny<int>())).ReturnsAsync(mails);
            _mailSenderFactoryMock.Setup(x => x.GetMailSender()).Returns(_mailSenderMock.Object);
            var target = Mocker.CreateInstance<SendPendingMailsCmdHandler>();
            var request = new SendPendingMailsCmd();

            //act
            await target.Handle(request, CancellationToken.None);

            //assert
            _eventBusMock.Verify(x => x.Publish(It.Is<SendingMailsCompletedEvent>(y =>
            y.MailsSentNo == 2
            && y.MailsRejectedNo == 0)), Times.Once);
        }

        [Test]
        public async Task For_MailSenderError_Should_MarkMailAsRejected()
        {
            //arrange
            var mb = new MailBuilder();
            var mails = new List<Mail> { mb.SetDefaults().Build() };

            _mailWriteRepositoryMock.Setup(x => x.GetPendingAsync(It.IsAny<int>())).ReturnsAsync(mails);
            _mailSenderMock.Setup(x => x.SendMailAsync(It.IsAny<Mail>())).Throws(new Exception()); // mail sender error

            _mailSenderFactoryMock.Setup(x => x.GetMailSender()).Returns(_mailSenderMock.Object);
            var target = Mocker.CreateInstance<SendPendingMailsCmdHandler>();
            var request = new SendPendingMailsCmd();

            //act
            await target.Handle(request, CancellationToken.None);

            //assert
            mails.All(x => x.Status == MailStatus.Rejected).Should().BeTrue();
            mails.All(x => x.SentDate == null).Should().BeTrue();
        }

        [Test]
        public async Task For_MailSenderError_Should_PublishEventWithRejectedNoEquals1()
        {
            //arrange
            var mb = new MailBuilder();
            var mails = new List<Mail> { mb.SetDefaults().Build() };

            _mailWriteRepositoryMock.Setup(x => x.GetPendingAsync(It.IsAny<int>())).ReturnsAsync(mails);
            _mailSenderMock.Setup(x => x.SendMailAsync(It.IsAny<Mail>())).Throws(new Exception()); // mail sender error

            _mailSenderFactoryMock.Setup(x => x.GetMailSender()).Returns(_mailSenderMock.Object);
            var target = Mocker.CreateInstance<SendPendingMailsCmdHandler>();
            var request = new SendPendingMailsCmd();

            //act
            await target.Handle(request, CancellationToken.None);

            //assert
            _eventBusMock.Verify(x => x.Publish(It.Is<SendingMailsCompletedEvent>(y =>
            y.MailsSentNo == 0
            && y.MailsRejectedNo == 1)), Times.Once);
        }
    }
}
