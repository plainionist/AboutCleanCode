using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeOverComments
{
    public class ExceptionParser
    {
        public ExceptionInfo Parse(string exceptionText)
        {
            var lines = CleanupWhitespaces(exceptionText);

            if (lines[0].StartsWith("System.AggregateException"))
            {
                lines = RemoveAggregateException(lines);
            }

            var info = new ExceptionInfo();

            (info.ExceptionType, info.Message) = ParseExceptionTypeAndMethod(lines);
            info.StackTrace = ParseStackTrace(lines);
            info.WordCloud = ComputeWordCloud(info);

            return info;
        }

        private static List<string> RemoveAggregateException(List<string> lines)
        {
            lines = lines
                .Skip(1)
                .ToList();

            lines[0] = lines[0].Replace("--->", "").Trim();

            return lines;
        }

        private static IReadOnlyCollection<CodeType> ComputeWordCloud(ExceptionInfo info)
        {
            var applicationStackTrace = info.StackTrace
                .Reverse()
                .SkipWhile(x => x.StartsWith("System.") || x.StartsWith("MS.Win32"));

            var wordsWithCount = applicationStackTrace
                .SelectMany(x => x.Split('.'))
                .Where(x => !string.IsNullOrEmpty(x))
                .GroupBy(x => x);

            return wordsWithCount
                .Select(x => new CodeType{
                    FullName = x.Key,
                    Occurrence = x.Count()
                })
                .ToList();
        }

        private static IReadOnlyList<string> ParseStackTrace(IReadOnlyList<string> lines)
        {
            var stackTrace = lines.Skip(1);

            if (ContainsInnerExceptions(stackTrace))
            {
                stackTrace = stackTrace
                    .TakeWhile(x => x.StartsWith("at "))
                    .ToList();
            }
            else
            {
                stackTrace = stackTrace
                    .Where(x => x.StartsWith("at "))
                    .ToList();
            }

            // Noise reduction: we are only interested in APIs names
            return stackTrace
                // cut of "at "
                .Select(x => x.Substring(3))
                // remove method parameters
                .Select(x => x.IndexOf('(') != -1 ? x.Substring(0, x.IndexOf('(')) : x)
                // remove compiler generated "displayclass"
                .Select(x => x.IndexOf('+') != -1 ? x.Substring(0, x.IndexOf('+')) : x)
                .Select(x => x.Trim())
                .ToList();
        }

        private static bool ContainsInnerExceptions(IEnumerable<string> stackTrace)
        {
            return stackTrace.Contains("--- End of inner exception stack trace ---")
                || stackTrace.Contains("--- End of stack trace from previous location ---")
                || stackTrace.Contains("Inner Exception: ");
        }

        private static (string, string) ParseExceptionTypeAndMethod(IReadOnlyList<string> lines)
        {
            var firstLine = lines[0];
            var sepPos = firstLine.IndexOf(':');
            var exceptionType = firstLine.Substring(0, sepPos);
            var message = firstLine.Substring(sepPos + 1).Trim();
            return (exceptionType, message);
        }

        private static List<string> CleanupWhitespaces(string exceptionText)
        {
            return exceptionText
                .Trim(' ', '\t', '\r', '\n')
                .Split(Environment.NewLine)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();
        }
    }
}