using Skybrud.Umbraco.GridData.Converters;
using Umbraco.Cms.Core.DependencyInjection;

// ReSharper disable InconsistentNaming

namespace Skybrud.Umbraco.GridData.Composers {
    
    public static class GridComposerExtensions {

        public static GridConverterCollectionBuilder GridConverters(this IUmbracoBuilder builder) {
            return builder.WithCollectionBuilder<GridConverterCollectionBuilder>();
        }

    }

}