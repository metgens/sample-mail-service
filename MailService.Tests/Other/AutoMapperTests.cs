using AutoMapper;
using MailService.Domain.Handlers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailService.Tests.Other
{
    [TestFixture]
    internal class AutoMapperTests
    {

        [Test]
        public void Should_AllDestinationsHaveSource()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                var profiles = typeof(GetAllMailsQueryHandler).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            });
            configuration.AssertConfigurationIsValid(); //throws exception if something is wrong

            Assert.True(true);
        }
    }

}
