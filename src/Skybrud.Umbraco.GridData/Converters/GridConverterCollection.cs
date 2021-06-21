using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

namespace Skybrud.Umbraco.GridData.Converters {

    /// <summary>
    /// Collection of <see cref="IGridConverter"/>.
    /// </summary>
    public class GridConverterCollection : BuilderCollectionBase<IGridConverter> {

        /// <summary>
        /// Initializes a new converter collection based on the specified <paramref name="items"/>.
        /// </summary>
        /// <param name="items">The items to make up the collection.</param>
        public GridConverterCollection(IEnumerable<IGridConverter> items) : base(items) { }

    }

}