using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;
using Skybrud.Umbraco.GridData.Factories;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Class representing the value/model saved by an Umbraco Grid property.
    /// </summary>
    public class GridDataModel : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets whether the model is valid. The model is considered valid if it has been parsed from a JSON value and
        /// has at least one valid control.
        /// </summary>
        public bool IsValid {
            get { return JObject != null! && GetAllControls().Any(x => x.IsValid); }
        }

        /// <summary>
        /// Gets the name of the selected layout.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets an array of the columns in the grid.
        /// </summary>
        public IReadOnlyList<GridSection> Sections { get; }

        /// <summary>
        /// Gets a reference to the parent <see cref="IPublishedElement"/>, if the Grid model was loaded directly from a property value.
        /// </summary>
        [JsonIgnore]
        public IPublishedElement? Owner { get; }

        /// <summary>
        /// Gets whether the grid model has a reference to it's <see cref="IPublishedElement"/> owner.
        /// </summary>
        [JsonIgnore]
        [MemberNotNullWhen(true, "Owner")]
        public bool HasOwner => Owner != null;

        /// <summary>
        /// Gets a reference to the parent property type, if the Grid model was loaded directly from a property value.
        /// </summary>
        public IPublishedPropertyType? PropertyType { get; }

        /// <summary>
        /// Gets whether a property type has been specified for the model.
        /// </summary>
        [MemberNotNullWhen(true, "PropertyType")]
        public bool HasPropertyType => PropertyType != null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        ///
        /// <paramref name="owner"/> and <paramref name="propertyType"/> may be specified if the grid value comes directly from a property value. If either aren't available, it's fine to specify <c>null</c> for both of them.
        /// </summary>
        /// <param name="owner">An instance of <see cref="IPublishedElement"/> representing the owner holding the grid value.</param>
        /// <param name="propertyType">An instance of <see cref="IPublishedPropertyType"/> representing the property holding the grid value.</param>
        /// <param name="json">An instance of <see cref="JObject"/> representing the grid model.</param>
        /// <param name="factory">The factory used for parsing subsequent parts of the grid.</param>
        public GridDataModel(IPublishedElement? owner, IPublishedPropertyType? propertyType, JObject? json, IGridFactory? factory) : base(json ?? new JObject()) {

            Owner = owner;
            PropertyType = propertyType;
            Name = json.GetString("name")!;

            if (factory is null) {
                Sections = Array.Empty<GridSection>();
            } else {
                Sections = json.GetArray("sections", x => factory.CreateGridSection(x, this)) ?? Array.Empty<GridSection>();
            }

        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets an array of all nested controls.
        /// </summary>
        public GridControl[] GetAllControls() {
            return (
                from section in Sections
                from row in section.Rows
                from area in row.Areas
                from control in area.Controls
                select control
            ).ToArray();
        }

        /// <summary>
        /// Gets an array of all nested controls with the specified editor <paramref name="alias"/>.
        /// </summary>
        /// <param name="alias">The editor alias of controls to be returned.</param>
        public GridControl[] GetAllControls(string alias) {
            return GetAllControls(x => x.Editor.Alias == alias);
        }

        /// <summary>
        /// Gets an array of all nested controls matching the specified <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The predicate (callback function) used for comparison.</param>
        public GridControl[] GetAllControls(Func<GridControl, bool> predicate) {
            return (
                from section in Sections
                from row in section.Rows
                from area in row.Areas
                from control in area.Controls
                where predicate(control)
                select control
            ).ToArray();
        }

        /// <summary>
        /// Writes a string representation of the grid data model to <paramref name="writer"/>.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <param name="writer">The writer.</param>
        public void WriteSearchableText(GridContext context, TextWriter writer) {
            foreach (GridSection section in Sections) section.WriteSearchableText(context, writer);
        }

        /// <summary>
        /// Gets a textual representation of the grid model - eg. to be used in Examine.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>An instance of <see cref="string"/> representing the value of the element.</returns>
        public string GetSearchableText(GridContext context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an empty (and invalid) model. This method can be used to get a fallback value for when an actual Grid model isn't available.
        /// </summary>
        public static GridDataModel GetEmptyModel() {
            return new GridDataModel(null, null, null, null);
        }

        /// <summary>
        /// Gets an empty (and invalid) model. This method can be used to get a fallback value for when an actual Grid model isn't available.
        /// </summary>
        /// <param name="owner">An instance of <see cref="IPublishedElement"/> representing the owner holding the grid value.</param>
        /// <param name="propertyType">An instance of <see cref="IPublishedPropertyType"/> representing the property holding the grid value.</param>
        public static GridDataModel GetEmptyModel(IPublishedElement owner, IPublishedPropertyType propertyType) {
            return new GridDataModel(owner, propertyType, null, null);
        }

        #endregion

    }

}