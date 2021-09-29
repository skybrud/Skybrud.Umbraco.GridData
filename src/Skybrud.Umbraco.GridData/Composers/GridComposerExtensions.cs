using Skybrud.Umbraco.GridData.Converters;
using Umbraco.Cms.Core.DependencyInjection;

// ReSharper disable InconsistentNaming

namespace Skybrud.Umbraco.GridData.Composers {
    
    /// <summary>
    /// Static class with extension methods for composing the grid.
    /// </summary>
    public static class GridComposerExtensions {

        /// <summary>
        /// Returns the current instance of <see cref="GridConverterCollectionBuilder"/>.
        /// </summary>
        /// <param name="builder">The current <see cref="IUmbracoBuilder"/>.</param>
        /// <returns>An instance of <see cref="GridConverterCollectionBuilder"/>.</returns>
        public static GridConverterCollectionBuilder GridConverters(this IUmbracoBuilder builder) {
            return builder.WithCollectionBuilder<GridConverterCollectionBuilder>();
        }

    }

}