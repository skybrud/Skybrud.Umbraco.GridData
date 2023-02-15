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
        /// Gets the default framework for rendering the Grid.
        /// </summary>
        public const string DefaultFramework = "bootstrap3";

        #endregion

        #region Static methods

        /// <summary>
        /// Returns a <see cref="GridDataModel"/> instance representing the value of the property with the specified
        /// <paramref name="propertyAlias"/>. If the property doesn't exist or it's value doesn't match a
        /// <see cref="GridDataModel"/> instance, a <see cref="GridDataModel"/> instance representing an empty grid
        /// model is returned instead.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static GridDataModel GetGridModel(this IPublishedContent? content, string propertyAlias) {
            return content?.Value<GridDataModel>(propertyAlias) ?? GridDataModel.GetEmptyModel();
        }

        /// <summary>
        /// Returns a <see cref="GridDataModel"/> instance representing the value of the property with the specified
        /// <paramref name="propertyAlias"/>. If the property doesn't exist or it's value doesn't match a
        /// <see cref="GridDataModel"/> instance, <see langword="null"/> is returned instead.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static GridDataModel? GetGridModelorNull(this IPublishedContent? content, string propertyAlias) {
            return content?.Value<GridDataModel>(propertyAlias);
        }

        /// <summary>
        /// Attempts to get a <see cref="GridDataModel"/> instance from the property with the specified <paramref name="propertyAlias"/>.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        /// <param name="result">When this method returns, holds the <see cref="GridDataModel"/> instance if successful; otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
        public static bool TryGetGridModel(this IPublishedContent? content, string propertyAlias, out GridDataModel? result) {
            result = content?.Value<GridDataModel>(propertyAlias);
            return result != null;
        }

        #endregion

    }

}