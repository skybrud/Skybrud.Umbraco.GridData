using Skybrud.Umbraco.GridData.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Skybrud.Umbraco.GridData.Extensions {

    /// <summary>
    /// Class holding various extension methods for using the typed Grid.
    /// </summary>
    public static class TypedGridExtensionMethods {

        #region Constants

        /// <summary>
        /// Gets the default property alias of the Grid.
        /// </summary>
        public const string DefaultPropertyAlias = "bodyText";

        /// <summary>
        /// Gets the default framework for rendering the Grid.
        /// </summary>
        public const string DefaultFramework = "bootstrap3";

        #endregion

        #region Static methods

        /// <summary>
        /// Gets the model for the typed Grid.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        public static GridDataModel GetGridModel(this IPublishedContent content) {
            return content.Value<GridDataModel>(DefaultPropertyAlias) ?? GridDataModel.GetEmptyModel();
        }

        /// <summary>
        /// Gets the model for the typed Grid.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static GridDataModel GetGridModel(this IPublishedContent content, string propertyAlias) {
            return content.Value<GridDataModel>(propertyAlias) ?? GridDataModel.GetEmptyModel();
        }

        #endregion

    }

}