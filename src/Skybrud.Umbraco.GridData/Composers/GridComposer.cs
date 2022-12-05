using Microsoft.Extensions.DependencyInjection;
using Skybrud.Umbraco.GridData.Converters.Umbraco;
using Skybrud.Umbraco.GridData.Factories;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;

namespace Skybrud.Umbraco.GridData.Composers {
    
    internal class GridComposer : IComposer {

        public void Compose(IUmbracoBuilder builder) {

            builder.Services.AddSingleton<GridContext>();
            builder.Services.AddUnique<IGridFactory, DefaultGridFactory>();
            
            builder.GridConverters().Append<UmbracoGridConverter>();

        }

    }

}