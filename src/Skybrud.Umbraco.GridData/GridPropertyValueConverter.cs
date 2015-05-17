using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Property value converter for the Umbraco Grid.
    /// </summary>
    public class GridPropertyValueConverter : IPropertyValueConverter {

        /// <summary>
        /// Gets or sets whether this property value converter is enabled.
        /// </summary>
        public static bool IsEnabled = true;

        public bool IsConverter(PublishedPropertyType propertyType) {
            return IsEnabled && propertyType.PropertyEditorAlias == "Umbraco.Grid";
        }

        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview) {
            return source;
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview) {

            // Get the value as a string
            string str = source as string;

            // Deserialize the string
            return GridDataModel.Deserialize(str) ?? GridDataModel.GetEmptyModel();

        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview) {
            return null;
        }
    
    }

}