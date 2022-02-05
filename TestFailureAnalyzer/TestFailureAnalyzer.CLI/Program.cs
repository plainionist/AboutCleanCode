﻿using System;
using TestFailureAnalyzer.IO;

namespace TestFailureAnalyzer.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var mailClient = new MailClient();
            var testResultsRepository = new TestDatabaseClient();
            var configReader = new ConfigurationReader();

            var interactor = new MailNotificationInteractor(
                configReader,
                testResultsRepository,
                mailClient
            );
        }
    }
}
