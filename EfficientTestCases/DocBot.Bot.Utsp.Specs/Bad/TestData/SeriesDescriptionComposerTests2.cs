using NUnit.Framework;

namespace Company.Product.Tests.ApplicationLogic.CalculationEngine.VolumeCustomization
{
    [TestFixture]
    public class SeriesDescriptionComposerTests
    {
        /// <summary>Some doc in the xml</summary>
        /// <param name=""Scenario1"">Description of param 1</param>
        /// <param name=""Scenario2"">Description of param 2</param>
        /// <req-key>req_sw_calc_F944X124</req-key>
        [Test(Description = "and some in the attribute")]
        public void Compose_NoResultType_ThrowsException()
        {
        }
    }

}