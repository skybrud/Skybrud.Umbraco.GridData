using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Property value converter for the Umbraco Grid.
    /// </summary>
    public class GridPropertyValueConverter : IPropertyValueConverterMeta {

        /// <summary>
        /// Gets a value indicating whether the converter supports a property type.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>A value indicating whether the converter supports a property type.</returns>
        public bool IsConverter(PublishedPropertyType propertyType) {
            return propertyType.PropertyEditorAlias == "Umbraco.Grid";
        }

        /// <summary>
        /// Converts a property Data value to a Source value.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <param name="source">The data value.</param>
        /// <param name="preview">A value indicating whether conversion should take place in preview mode.</param>
        /// <returns>The result of the conversion.</returns>
        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview) {
            return source;
        }

        /// <summary>
        /// Converts a property Source value to an Object value.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <param name="source">The source value.</param>
        /// <param name="preview">A value indicating whether conversion should take place in preview mode.</param>
        /// <returns>The result of the conversion.</returns>
        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview) {

            // Get the value as a string
            string str = source as string;

            // Deserialize the string
            return GridDataModel.Deserialize(str, propertyType.PropertyTypeAlias) ?? GridDataModel.GetEmptyModel(propertyType.PropertyTypeAlias);

        }

        /// <summary>
        /// Converts a property Source value to an XPath value.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <param name="source">The source value.</param>
        /// <param name="preview">A value indicating whether conversion should take place in preview mode.</param>
        /// <returns>The result of the conversion.</returns>
        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview) {
            return null;
        }

        /// <summary>
        /// Gets the property cache level of a specified value.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <param name="cacheValue">The property value.</param>
        /// <returns>The property cache level of the specified value.</returns>
        public PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType, PropertyCacheValue cacheValue) {
            PropertyCacheLevel propertyCacheLevel;
            switch (cacheValue) {
                case PropertyCacheValue.Source:
                    propertyCacheLevel = PropertyCacheLevel.Content;
                    break;
                case PropertyCacheValue.Object:
                    propertyCacheLevel = PropertyCacheLevel.ContentCache;
                    break;
                case PropertyCacheValue.XPath:
                    propertyCacheLevel = PropertyCacheLevel.Content;
                    break;
                default:
                    propertyCacheLevel = PropertyCacheLevel.None;
                    break;
            }
            return propertyCacheLevel;
        }
        
        /// <summary>
        /// Gets the type of values returned by the converter.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The CLR type of values returned by the converter.</returns>
        public virtual Type GetPropertyValueType(PublishedPropertyType propertyType) {
            return typeof(GridDataModel);
        }

    }

}