using System;
using Umbraco.Core.Configuration.Grid;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Property value converter for the Umbraco Grid.
    /// </summary>
    class GridPropertyValueConverter : PropertyValueConverterBase {

        private readonly IGridConfig _config;

        public GridPropertyValueConverter(IGridConfig config) {
            _config = config;
        }

        /// <summary>
        /// Gets a value indicating whether the converter supports a property type.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>A value indicating whether the converter supports a property type.</returns>
        public override bool IsConverter(PublishedPropertyType propertyType) {
            return propertyType.EditorAlias == "Umbraco.Grid";
        }
        
        public override object ConvertSourceToIntermediate(IPublishedElement owner, PublishedPropertyType propertyType, object source, bool preview) {
            return GridDataModel.Deserialize(source as string, propertyType.Alias, _config);
        }
        
        public override object ConvertIntermediateToObject(IPublishedElement owner, PublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
            return inter;
        }

        public override object ConvertIntermediateToXPath(IPublishedElement owner, PublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
            return null;
        }

        public override PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType) {
            return PropertyCacheLevel.None;
        }
        
        /// <summary>
        /// Gets the type of values returned by the converter.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The CLR type of values returned by the converter.</returns>
        public override Type GetPropertyValueType(PublishedPropertyType propertyType) {
            return typeof(GridDataModel);
        }

    }

}