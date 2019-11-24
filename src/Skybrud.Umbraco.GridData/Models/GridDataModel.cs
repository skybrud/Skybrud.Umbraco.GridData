using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Extensions;
using Skybrud.Umbraco.GridData.Factories;
using Skybrud.Umbraco.GridData.Json;
using Umbraco.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Models {
    
    /// <summary>
    /// Class representing the value/model saved by an Umbraco Grid property.
    /// </summary>
    public class GridDataModel : GridJsonObject, IGridDataModel {

        #region Properties

        /// <summary>
        /// Gets whether the model is valid. The model is considered valid if it has been parsed from a JSON value and
        /// has at least one valid control.
        /// </summary>
        public bool IsValid {
            get { return JObject != null && GetAllControls().Any(x => x.IsValid); }
        }

        /// <summary>
        /// Gets the name of the selected layout.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a reference to the parent <see cref="IPublishedElement"/>, if the Grid model was loaded directly from a property value.
        /// </summary>
        [JsonIgnore]
        public IPublishedElement Owner { get; }

        /// <summary>
        /// Gets whether the grid model has a reference to it's <see cref="IPublishedElement"/> owner.
        /// </summary>
        [JsonIgnore]
        public bool HasOwner => Owner != null;

        /// <summary>
        /// Gets an array of the columns in the grid.
        /// </summary>
        public IGridSection[] Sections { get; }
        
        /// <summary>
        /// Gets a reference to the parent property type, if the Grid model was loaded directly from a property value.
        /// </summary>
        public IPublishedPropertyType PropertyType { get; }

        /// <summary>
        /// Gets whether a property type has been specified for the model.
        /// </summary>
        public bool HasPropertyType => PropertyType != null;

        #region Exposing properties from the JSON due to http://issues.umbraco.org/issue/U4-5750

        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Same as <see cref="Name"/>.
        /// </summary>
        [Obsolete]
        [JsonIgnore]
        public string name => Name;

        /// <summary>
        /// Gets the underlying JSON array for the <c>sections</c> property. 
        /// </summary>
        [Obsolete]
        [JsonIgnore]
        public dynamic sections => ((dynamic) JObject).sections;

        // ReSharper restore InconsistentNaming

        #endregion
        
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
        public GridDataModel(IPublishedElement owner, IPublishedPropertyType propertyType, JObject json, IGridFactory factory) : base(json) {
            Owner = owner;
            PropertyType = propertyType;
            Name = json.GetString("name");
            Sections = json.GetArray("sections", x => factory.CreateGridSection(x, this)) ?? new IGridSection[0];
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets an array of all nested controls. 
        /// </summary>
        public IGridControl[] GetAllControls() {
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
        public IGridControl[] GetAllControls(string alias) {
            return GetAllControls(x => x.Editor.Alias == alias);
        }

        /// <summary>
        /// Gets an array of all nested controls matching the specified <paramref name="predicate"/>. 
        /// </summary>
        /// <param name="predicate">The predicate (callback function) used for comparison.</param>
        public IGridControl[] GetAllControls(Func<IGridControl, bool> predicate) {
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
        /// Generates the HTML for the Grid model.
        /// </summary>
        /// <param name="helper">The HTML helper used for rendering the Grid.</param>
        public HtmlString GetHtml(HtmlHelper helper) {
            return helper.GetTypedGridHtml(this);
        }

        /// <summary>
        /// Generates the HTML for the Grid model.
        /// </summary>
        /// <param name="helper">The HTML helper used for rendering the Grid.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public HtmlString GetHtml(HtmlHelper helper, string framework) {
            return helper.GetTypedGridHtml(this, framework);
        }

        /// <summary>
        /// Gets a textual representation of the grid model - eg. to be used in Examine.
        /// </summary>
        /// <returns>Returns an instance of <see cref="System.String"/> representing the value of the grid model.</returns>
        public virtual string GetSearchableText() {
            return Sections.Aggregate(string.Empty, (current, section) => current + section.GetSearchableText());
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an empty (and invalid) model. This method can be used to get a fallback value for when an actual Grid model isn't available.
        /// </summary>
        public static IGridDataModel GetEmptyModel() {
            return new GridDataModel(null, null, null, null);
        }

        /// <summary>
        /// Gets an empty (and invalid) model. This method can be used to get a fallback value for when an actual Grid model isn't available.
        /// </summary>
        /// <param name="owner">An instance of <see cref="IPublishedElement"/> representing the owner holding the grid value.</param>
        /// <param name="propertyType">An instance of <see cref="IPublishedPropertyType"/> representing the property holding the grid value.</param>
        public static IGridDataModel GetEmptyModel(IPublishedElement owner, IPublishedPropertyType propertyType) {
            return new GridDataModel(owner, propertyType, null, null);
        }

        #endregion

    }

}