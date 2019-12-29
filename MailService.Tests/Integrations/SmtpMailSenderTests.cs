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

namespace MailService.Tests.Integrations
{
    [TestFixture]
    public class SmtpMailSenderTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [Ignore("Run manually")]
        public void Should_SendEmailWithAttachment()
        {

        }
    }
}