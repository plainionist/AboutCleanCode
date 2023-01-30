using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExceptionAnalyzer
{
    public class ExceptionParser
    {

        public ExceptionInfo ExtractException(string errorMessage)
        {
            var exceptionType = GetExceptionType(errorMessage);
            var messages = PreProcessingErrorMessages(errorMessage, exceptionType);
            var exceptionMessages = new StringBuilder();
            var exceptionCallStacks = new StringBuilder();

            foreach (var x in messages)
            {
                var trimmedMessage = x.TrimStart();
                SeparateMessageAndCallStack(exceptionMessages, exceptionCallStacks, trimmedMessage);
            }

            return new()
            {
                Type = exceptionType,
                Message = exceptionMessages.ToString(),
                StackTrace = exceptionCallStacks.ToString()
            };
        }

        private static void SeparateMessageAndCallStack(StringBuilder exceptionMessages, StringBuilder exceptionCallStacks, string trimmedMessage)
        {
            if (trimmedMessage.Contains('(') && trimmedMessage.Contains(')'))
            {
                var message = trimmedMessage.Substring(0, trimmedMessage.IndexOf('('));
                if (message.Trim().Count(s => s == '.') < message.Trim().Count(s => s == ' '))
                {
                    exceptionMessages.Append(trimmedMessage + "\n");
                }
                else
                {
                    exceptionCallStacks.Append(trimmedMessage + "\n");
                }
            }
            else
            {
                exceptionMessages.Append(trimmedMessage + "\n");
            }
        }

        private string GetExceptionType(string errorMessage) => errorMessage.Split(':').First();

        private string[] PreProcessingErrorMessages(string errorMessage, string exceptionType)
        {
            var message = errorMessage.Replace(exceptionType + ":", "");

            string singleErrorMessage = Regex.Replace(message, @"\r|\n|\t", " ");

            string[] separator = { " at " };
            return singleErrorMessage.Split(separator, StringSplitOptions.None);
        }
    }
}