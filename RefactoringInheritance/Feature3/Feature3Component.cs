using AboutCleanCode.CommonLibrary;

namespace AboutCleanCode.Feature3
{
    public class FeatureComponent : AbstractComponent
    {
        public override void Init(IApplicationContext context)
        {
            // feature initialization goes here

            NotifyUserAboutMissingLicenses();
        }

        // other feature related code

        private void NotifyUserAboutMissingLicenses()
        {
            // 1. use LicensingContext to check missing licenses
            // 2. Prepare message for the user
            // 3. Open dialog

            // additionally inform our sales via e-mail
        }
    }
}