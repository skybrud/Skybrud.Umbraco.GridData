using System;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Umbraco.GridData.Factories;
using Skybrud.Umbraco.GridData.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Property value converter for the Umbraco Grid.
    /// </summary>
    class GridValueConverter : PropertyValueConverterBase {

        private readonly IGridFactory _factory;

        public GridValueConverter(IGridFactory factory) {
            _factory = factory;
        }

        /// <summary>
        /// Gets a value indicating whether the converter supports a property type.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>A value indicating whether the converter supports a property type.</returns>
        public override bool IsConverter(IPublishedPropertyType propertyType) {
            return propertyType.EditorAlias == "Umbraco.Grid";
        }
        
        public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview) {

            if (source is string {Length: > 0} json && json[0] == '{') {
                return JsonUtils.ParseJsonObject(json);
            }

            return null;

        }
        
        public override object? ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview) {
            return _factory.CreateGridModel(owner, propertyType, inter as JObject, preview);
        }

        public override object? ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview) {
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
            return typeof(GridDataModel);
        }

    }

}