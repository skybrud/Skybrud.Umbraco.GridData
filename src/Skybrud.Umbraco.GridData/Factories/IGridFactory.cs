using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.Umbraco.GridData.Models;
using Skybrud.Umbraco.GridData.Models.Editors;
using Umbraco.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Factories {

    /// <summary>
    /// Interface describing a factory for initializing the various parts of the Grid.
    /// </summary>
    public interface IGridFactory {

        IGridDataModel ParseGridModel(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview);

        /// <summary>
        /// Returns a new <see cref="IGridDataModel"/> from the specified <paramref name="json"/> object.
        ///
        /// <paramref name="owner"/> and <paramref name="propertyType"/> may be specified if the grid value comes directly from a property value. If either aren't available, it's fine to specify <c>null</c> for both of them.
        /// </summary>
        /// <param name="owner">An instance of <see cref="IPublishedElement"/> representing the owner holding the grid value.</param>
        /// <param name="propertyType">An instance of <see cref="IPublishedPropertyType"/> representing the property holding the grid value.</param>
        /// <param name="json">The instance of <see cref="JObject"/> representing the grid model.</param>
        /// <returns>An instance of <see cref="IGridDataModel"/> representing the grid model.</returns>
        IGridDataModel CreateGridModel(IPublishedElement owner, IPublishedPropertyType propertyType, JObject json);

        /// <summary>
        /// Returns a new instance of <see cref="IGridSection"/> from the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> representing the grid section.</param>
        /// <param name="grid">An instance of <see cref="IGridDataModel"/> representing the parent grid model.</param>
        /// <returns>An instance of <see cref="IGridSection"/> representing the grid section.</returns>
        IGridSection CreateGridSection(JObject json, IGridDataModel grid);

        IGridRow CreateGridRow(JObject json, IGridSection section);

        IGridArea CreateGridArea(JObject json, IGridRow row);

        IGridControl CreateGridControl(JObject json, IGridArea area);

        IGridEditor CreateGridEditor(JObject json, IGridControl control);

    }

    public class DefaultGridFactory : IGridFactory {

        public virtual IGridDataModel ParseGridModel(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) {

            if (source is string json && json.Length > 0 && json[0] == '{') {
                return CreateGridModel(owner, propertyType, JsonUtils.ParseJsonObject(json));
            }

            return null;

        }

        /// <inheritdoc />
        public virtual IGridDataModel CreateGridModel(IPublishedElement owner, IPublishedPropertyType propertyType, JObject json) {
            return new GridDataModel(owner, propertyType, json, this);
        }

        /// <inheritdoc />
        public virtual IGridSection CreateGridSection(JObject json, IGridDataModel grid) {
            return new GridSection(json, grid, this);
        }

        /// <inheritdoc />
        public virtual IGridRow CreateGridRow(JObject json, IGridSection section) {
            return new GridRow(json, section, this);
        }

        /// <inheritdoc />
        public virtual IGridArea CreateGridArea(JObject json, IGridRow row) {
            return new GridArea(json, row, this);
        }

        /// <inheritdoc />
        public virtual IGridControl CreateGridControl(JObject json, IGridArea area) {
            return new GridControl(json, area, this);
        }

        /// <inheritdoc />
        public virtual IGridEditor CreateGridEditor(JObject json, IGridControl control) {
            return new GridEditor(json, control);
        }

    }


}