using FluentAssertions;
using MailService.Common.Bus.Event;
using MailService.Common.Exceptions;
using MailService.Contracts.Commands;
using MailService.Contracts.Events;
using MailService.Domain;
using MailService.Domain.Handlers;
using MailService.Domain.MailSenders;
using MailService.Domain.Repositories;
using MailService.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MailService.Tests.Handlers
{
    [TestFixture]
    public class SendMailCmdHandlerTests : AutoMockerTests
    {
        private Mock<IMailWriteRepository> _mailWriteRepositoryMock;
        private Mock<IMailSenderFacade> _mailSenderFacade;
        private Mock<IEventBus> _eventBusMock;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            _mailWriteRepositoryMock = Mocker.GetMock<IMailWriteRepository>();
            _mailSenderFacade = Mocker.GetMock<IMailSenderFacade>();
            _eventBusMock = Mocker.GetMock<IEventBus>();
        }

        [Test]
        public async Task Should_SaveChangesAndSendEvent()
        {
            //arrange
            var mail = new MailBuilder().SetDefaults().Build();

            _mailWriteRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(mail);
            var target = Mocker.CreateInstance<SendMailCmdHandler>();
            var request = new SendMailCmd(Guid.NewGuid());

            //act
            await target.Handle(request, CancellationToken.None);

            //assert
            _mailWriteRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            _eventBusMock.Verify(x => x.Publish(It.IsAny<SendingMailsCompletedEvent>()), Times.Once);
        }

        [Test]
        public async Task For_NotExistingMail_Should_ThrowEx()
        {
            //arrange

            _mailWriteRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Mail)null); //not exisitng mail
            var target = Mocker.CreateInstance<SendMailCmdHandler>();
            var request = new SendMailCmd(Guid.NewGuid());

            //act
            Func<Task> act = async () => await target.Handle(request, CancellationToken.None);

            //assert
            act.Should().Throw<AppException>();
        }

    }
}
