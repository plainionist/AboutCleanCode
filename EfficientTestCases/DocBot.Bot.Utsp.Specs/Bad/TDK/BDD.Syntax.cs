using System;

namespace DocBot.Bot.Utsp.Specs.TDK
{
    public interface IGiven
    {
    }

    public interface IThen
    {
    }

    public interface IWhen
    {
    }

    public class Builder : IGiven, IThen, IWhen { }

    public static class IGivenExtensions
    {
        private static Builder Builder(this IGiven self) => (Builder)self;

        public static void CodeFile(this IGiven self, string fileName) => self.Builder().CodeFile(fileName, "");
        public static void CodeFile(this IGiven self, string fileName, string code) => self.Builder().CodeFile(fileName, code);
        public static void UtspDefinition(this IGiven self, string content) => self.Builder().UtspDefinition(content);
        public static void SourceRepositorySpec(this IGiven self, string location, string version) => self.Builder().SourceRepositorySpec(location, version);
        public static void CreationEnvironment(this IGiven self, string hostname, string createdBy, DateTimeOffset createdAt, TimeSpan documentUtcOffset) => self.Builder().CreationEnvironment(hostname, createdBy, createdAt, documentUtcOffset);
    }

    public static class IWhenExtensions
    {
        private static Builder Builder(this IWhen self) => (Builder)self;

        public static IWhenUtsp Utsp(this IWhen self) => self.Builder().Utsp();
    }

    public interface IWhenUtsp
    {
        void IsGenerated(bool _skipCompilation = false);
    }

    public static class IThenExtensions
    {
        private static Builder Builder(this IThen self) => (Builder)self;

        public static IThenTestFixture TestFixture(this IThen self, string className) => self.Builder().TestFixture(className);
        public static IThenTestCase TestCase(this IThen self, string shortName) => self.Builder().TestCase(shortName);
        public static IThenFailsWith FailsWith(this IThen self) => self.Builder().FailsWith();
        public static IThenWrk Wrk(this IThen self) => self.Builder().Wrk();
        public static IThenMeta Meta(this IThen self) => self.Builder().Meta();
        public static IThenSourceRepository SourceRepository(this IThen self) => self.Builder().SourceRepository();
        public static IThenVariables Variables(this IThen self) => self.Builder().Variables();
        public static IThenHistory History(this IThen self) => self.Builder().History();
        public static IThenReferences References(this IThen self) => self.Builder().References();
    }

    public interface IThenReferences
    {
        void Contains(string name, string description, string id);
    }

    public interface IThenHistory
    {
        void ContainsAt(int pos, string version, string date, string author, string description);
    }

    public interface IThenSourceRepository
    {
        IThenSourceRepository IsOfType(string vcsType, string vcsVersionType);
        IThenSourceRepository IsLocation(string value);
        IThenSourceRepository IsVersion(string value);
    }

    public interface IThenMeta
    {
        void HasDocumentId(string number, string type, string part, string version);
        void HasAuthor(string author, string department);
        void HasTitle(string value);
        void HasDocumentDate(string value);
        void HasProductName(string value);
        void HasProductVersion(string value);
        void HasTestObject(string value);
        void HasDocumentRevision(string value);
        void HasDocumentGenerationDateTime(string s);
    }
    public interface IThenVariables
    {
        void Contains(string name, string value);
    }

    public interface IThenWrk
    {
        IThenWrk HasLinesWithoutHeader(params string[] lines);
        IThenWrk HasHeader(params string[] lines);
    }

    public interface IProject { }

    public interface IThenTestFixture
    {
        void IsIgnored();
        IThenTestFixture HasDescription(string value);
        IThenTestFixture DescriptionIsNA();
        IThenTestFixture HasShortName(string value);
        IThenTestFixture HasFullName(string value);
        IThenTestFixture HasAssembly(Func<IProject, string> value);
        void HasTestCases(params string[] methods);
    }

    public interface IThenTestCase
    {
        IThenTestCase HasDescription(string value);
        IThenTestCase HasShortName(string value);
        IThenTestCase HasFullName(string value);
        IThenTestCase HasSourceFile(Func<IProject, string> value);
        IThenTestCase HasRequirementKeys(params string[] keys);
        IThenTestCase HasExpectedResult(string value);
        IThenTestCase HasTestSteps(int count);
        IThenTestStep TestStep(int pos);
    }

    public interface IThenTestStep
    {
        IThenTestStep HasPreconditionLabel(string value);
        IThenTestStep HasCaption(string value);
        IThenTestStep HasDescription(string value);
        IThenTestStep HasExpectedResults(params string[] value);
        IThenTestStep TestStep(int pos);
    }

    public interface IThenFailsWith
    {
        void MissingSafetyKey(string _fqTestMethodName);
        void MissingTestCaseDescription(string _fqTestMethodName);
        void MissingXmlDocumentation(string _fqTestMethodName);
        void Anything(string _fqTestMethodName);
        void Message(string message);
    }
}
