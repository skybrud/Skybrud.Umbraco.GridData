using Skybrud.Umbraco.GridData.LeBlender.Converters;
using Umbraco.Core;

namespace Skybrud.Umbraco.GridData.LeBlender {

    public class Startup : ApplicationEventHandler {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            GridContext.Current.Converters.Add(new LeBlenderGridConverter());
        }

    }

}