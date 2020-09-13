using Umbraco.Core.Composing;

// ReSharper disable InconsistentNaming

namespace Skybrud.Umbraco.GridData.Converters {
    
    /// <summary>
    /// Provides extension methods to the <see cref="Composition"/> class.
    /// </summary>
    public static class GridCompositionExtensions {

        /// <summary>
        /// Gets the video picker provider collection builder.
        /// </summary>
        /// <param name="composition">The composition.</param>
        public static GridConverterCollectionBuilder GridConverters(this Composition composition) {
            return composition.WithCollectionBuilder<GridConverterCollectionBuilder>();
        }

    }

}