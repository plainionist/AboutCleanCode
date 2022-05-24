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
            Given.CodeFile(
                fileName: "SeriesDescriptionComposerTests.cs",
                code: @"using NUnit.Framework;
                    namespace Company.Product.Tests.ApplicationLogic.CalculationEngine.VolumeCustomization
                    {
                        [TestFixture]
                        public class SeriesDescriptionComposerTests
                        {
                            /// <req-key>req_sw_calc_F944X124</req-key>
                            [Test(Description = ""Ensure that an exception is thrown if an unknown result type is passed to the composer"")]
                            public void Compose_NoResultType_ThrowsException()
                            {
                            }
                        }
                    }");
            When.Utsp().IsGenerated();
            Then.TestCase(shortName: "Compose_NoResultType_ThrowsException")
                .HasDescription("Ensure that an exception is thrown if an unknown result type is passed to the composer");
        }

        [Test]
        public void AddParamsInDescription()
        {
            Given.CodeFile(
                fileName: "SeriesDescriptionComposerTests.cs",
                code: @"using NUnit.Framework;
                    namespace Company.Product.Tests.ApplicationLogic.CalculationEngine.VolumeCustomization
                    {
                        [TestFixture]
                        public class SeriesDescriptionComposerTests
                        {
                            /// <summary>Some doc in the xml</summary>
                            /// <param name=""Scenario1"">Description of param 1</param>
                            /// <param name=""Scenario2"">Description of param 2</param>
                            /// <req-key>req_sw_calc_F944X124</req-key>
                            [Test(Description = ""and some in the attribute"")]
                            public void Compose_NoResultType_ThrowsException()
                            {
                            }
                        }
                       
                    }");
            When.Utsp().IsGenerated();
            Then.TestCase(shortName: "Compose_NoResultType_ThrowsException")
                .HasDescription("Some doc in the xml( Parameter \"Scenario1\" : \"Description of param 1\";  Parameter \"Scenario2\" : \"Description of param 2\")");
        }

        [Test]
        public void MultipleRequirementKeys()
        {
            Given.CodeFile(
                fileName: "SeriesDescriptionComposerTests.cs",
                code: @"using NUnit.Framework;
                    namespace Company.Product.Contours.Tests
                    {
                        [TestFixture]
                        public class ContourMeasurementTests
                        {
                            /// <summary>Tests the ContourMeasurement constructor with an empty list.</summary>
                            /// <req-key>req_sw_calc_F33X001</req-key>
                            /// <req-key>req_sw_calc_F34X002</req-key>
                            /// <req-key>req_sw_calc_F35X003</req-key>
                            [Test(Description = ""Tests ContourMeasurement constructor with an empty list."")]
                            public void TestConstructorWithEmptyList()
                            {
                            }
                        }
                    }");
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
