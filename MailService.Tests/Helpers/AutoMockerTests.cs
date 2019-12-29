using Microsoft.Extensions.Logging;
using Moq.AutoMock;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Tests.Helpers
{
    [TestFixture]
    public class AutoMockerTests
    {
        protected object LoggerMock { get; set; }
        protected AutoMocker Mocker { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            Mocker = new AutoMocker();
            LoggerMock = Mocker.GetMock<ILogger>();
        }

    }

}
