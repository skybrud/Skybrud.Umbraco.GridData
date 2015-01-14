using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.Umbraco.GridData {
    
    public class GridPropertyValueConverter : IPropertyValueConverter {

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
            return GridDataModel.Deserialize(str) ?? new GridDataModel();

        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview) {
            return null;
        }
    
    }

}