using AboutCleanCode.CommonLibrary;

namespace AboutCleanCode.Feature5
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
            // disable this aspect of the feature completely
        }
    }
}