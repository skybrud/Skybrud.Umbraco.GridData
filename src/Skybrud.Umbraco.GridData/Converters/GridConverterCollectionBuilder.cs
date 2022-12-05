using Umbraco.Cms.Core.Composing;

namespace Skybrud.Umbraco.GridData.Converters {

    /// <inheritdoc />
    public class GridConverterCollectionBuilder : OrderedCollectionBuilderBase<GridConverterCollectionBuilder, GridConverterCollection, IGridConverter> {

        /// <inheritdoc />
        protected override GridConverterCollectionBuilder This => this;

    }

}