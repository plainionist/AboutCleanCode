using DocBot.Bot.Utsp.Specs.TDK;
using NUnit.Framework;

namespace DocBot.Bot.Utsp.Specs
{
    [TestFixture]
    sealed class WriteTestCases : TestBase<Builder>
    {
        [Test]
        public void DescriptionExtractedFromDescriptionPropertyOfTestAttribute()
        {
            Given.CodeFile(fileName: "SeriesDescriptionComposerTests1.cs");
            When.Utsp().IsGenerated();
            Then.TestCase(shortName: "Compose_NoResultType_ThrowsException")
                .HasDescription("Ensure that an exception is thrown if an unknown result type is passed to the composer");
        }

        [Test]
        public void AddParamsInDescription()
        {
            Given.CodeFile(fileName: "SeriesDescriptionComposerTests2.cs");
            When.Utsp().IsGenerated();
            Then.TestCase(shortName: "Compose_NoResultType_ThrowsException")
                .HasDescription("Some doc in the xml( Parameter \"Scenario1\" : \"Description of param 1\";  Parameter \"Scenario2\" : \"Description of param 2\")");
        }

        [Test]
        public void MultipleRequirementKeys()
        {
            Given.CodeFile(fileName: "ContourMeasurementTests.cs");
            When.Utsp().IsGenerated();
            Then.TestCase(shortName: "TestConstructorWithEmptyList")
                .HasRequirementKeys(
                    "req_sw_calc_F33X001",
                    "req_sw_calc_F34X002",
                    "req_sw_calc_F35X003"
                );
        }
    }
}
