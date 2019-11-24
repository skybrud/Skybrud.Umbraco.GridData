using System;
using Skybrud.Umbraco.GridData.Factories;
using Skybrud.Umbraco.GridData.Models;
using Umbraco.Core.Configuration.Grid;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Property value converter for the Umbraco Grid.
    /// </summary>
    class GridPropertyValueConverter : PropertyValueConverterBase {

        private readonly IGridConfig _config;

        private readonly IGridFactory _factory;

        public GridPropertyValueConverter(IGridConfig config) {
            _config = config;
            _factory = new DefaultGridFactory();
        }

        /// <summary>
        /// Gets a value indicating whether the converter supports a property type.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>A value indicating whether the converter supports a property type.</returns>
        public override bool IsConverter(IPublishedPropertyType propertyType) {
            return propertyType.EditorAlias == "Umbraco.Grid";
        }
        
        public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) {
            return _factory.ParseGridModel(owner, propertyType, source, preview);
        }
        
        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
            return inter;
        }

        public override object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
            return null;
        }

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) {
            return PropertyCacheLevel.Snapshot;
        }
        
        /// <summary>
        /// Gets the type of values returned by the converter.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The CLR type of values returned by the converter.</returns>
        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) {
            return typeof(IGridDataModel);
        }

    }

}