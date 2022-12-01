using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Factories {

    /// <summary>
    /// Interface describing a factory for initializing the various parts of the Grid.
    /// </summary>
    public interface IGridFactory {

        /// <summary>
        /// Returns a new <see cref="GridDataModel"/> from the specified <paramref name="json"/> object.
        /// 
        /// <paramref name="owner"/> and <paramref name="propertyType"/> may be specified if the grid value comes directly from a property value. If either aren't available, it's fine to specify <c>null</c> for both of them.
        /// </summary>
        /// <param name="owner">An instance of <see cref="IPublishedElement"/> representing the owner holding the grid value.</param>
        /// <param name="propertyType">An instance of <see cref="IPublishedPropertyType"/> representing the property holding the grid value.</param>
        /// <param name="json">The instance of <see cref="JObject"/> representing the grid model.</param>
        /// <param name="preview"></param>
        /// <returns>An instance of <see cref="GridDataModel"/> representing the grid model.</returns>
        GridDataModel? CreateGridModel(IPublishedElement owner, IPublishedPropertyType propertyType, JObject? json, bool preview);

        /// <summary>
        /// Returns a new instance of <see cref="GridSection"/> from the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> representing the grid section.</param>
        /// <param name="grid">An instance of <see cref="GridDataModel"/> representing the parent grid model.</param>
        /// <returns>An instance of <see cref="GridSection"/> representing the grid section.</returns>
        GridSection? CreateGridSection(JObject json, GridDataModel grid);

        GridRow CreateGridRow(JObject json, GridSection section);

        GridArea CreateGridArea(JObject json, GridRow row);

        GridControl CreateGridControl(JObject json, GridArea area);

        GridEditor? CreateGridEditor(JObject json);

    }

}