using System.Collections.Generic;
using Skybrud.Umbraco.GridData.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Skybrud.Umbraco.GridData.Converters {

    /// <summary>
    /// Collection of <see cref="IGridConverter"/>.
    /// </summary>
    public class GridConverterCollection : BuilderCollectionBase<IGridConverter> {

        /// <summary>
        /// Gets the current converter collection.
        /// </summary>
        public static GridConverterCollection Current => global::Umbraco.Core.Composing.Current.Factory.GetInstance<GridConverterCollection>();

        /// <summary>
        /// Initializes a new converter collection based on the specified <paramref name="items"/>.
        /// </summary>
        /// <param name="items">The items to make up the collection.</param>
        public GridConverterCollection(IEnumerable<IGridConverter> items) : base(items) { }

    }

}