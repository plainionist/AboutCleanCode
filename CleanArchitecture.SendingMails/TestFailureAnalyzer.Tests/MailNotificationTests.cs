using System;
using NUnit.Framework;
using TestFailureAnalyzer.Adapters;

namespace TestFailureAnalyzer.Tests
{
    public class MailNotificationTests
    {
        [Test]
        public void InternalErrorIsNotifiedViaMail()
        {
            var mail = TestAPI.SimulateInternalErrorOccurred(new InvalidOperationException("ups!"));

            Assert.That(mail.Body, Contains.Substring("Ups"));
        }
    }
}