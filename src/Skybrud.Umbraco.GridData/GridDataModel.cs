using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData {

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
            get { return JObject != null && GetAllControls().Any(x => x.IsValid); }
        }

        /// <summary>
        /// Gets the raw JSON value this model was parsed from.
        /// </summary>
        public string Raw { get; private set; }

        /// <summary>
        /// Gets the name of the selected layout.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets an array of the columns in the grid.
        /// </summary>
        public GridSection[] Sections { get; private set; }

        /// <summary>
        /// Gets the alias of the document type property used for the grid.
        /// </summary>
        public string PropertyAlias { get; private set; }

        /// <summary>
        /// Gets whether a property alias has been specified for the model.
        /// </summary>
        public bool HasPropertyAlias => !String.IsNullOrWhiteSpace(PropertyAlias);

        #region Exposing properties from the JSON due to http://issues.umbraco.org/issue/U4-5750

        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Same as <see cref="Name"/>.
        /// </summary>
        [Obsolete]
        public string name => Name;

        /// <summary>
        /// Gets the underlying JSON array for the <code>sections</code> property. 
        /// </summary>
        [Obsolete]
        public dynamic sections => ((dynamic) JObject).sections;

        // ReSharper restore InconsistentNaming

        #endregion
        
        #endregion

        #region Constructors

        private GridDataModel(JObject obj) : base(obj) {
            Sections = new GridSection[0];
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
            return Sections.Aggregate("", (current, section) => current + section.GetSearchableText());
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an empty (and invalid) model. This method can be used to get a fallback value for
        /// when an actual Grid model isn't available.
        /// </summary>
        public static GridDataModel GetEmptyModel() {
            return new GridDataModel(null) {
                PropertyAlias = ""
            };
        }

        /// <summary>
        /// Gets an empty (and invalid) model. This method can be used to get a fallback value for
        /// when an actual Grid model isn't available.
        /// </summary>
        /// <param name="propertyTypeAlias">The alias of the property the Grid model is representing.</param>
        public static GridDataModel GetEmptyModel(string propertyTypeAlias) {
            return new GridDataModel(null) {
                PropertyAlias = propertyTypeAlias
            };
        }

        /// <summary>
        /// Deserializes the specified <paramref name="json"/> string into an instance of <see cref="GridDataModel"/>.
        /// </summary>
        /// <param name="json">The JSON string to be deserialized.</param>
        public static GridDataModel Deserialize(string json) {
            return Deserialize(json, "");
        }

        /// <summary>
        /// Deserializes the specified <paramref name="json"/> string into an instance of <see cref="GridDataModel"/>.
        /// </summary>
        /// <param name="json">The JSON string to be deserialized.</param>
        /// <param name="propertyTypeAlias">The alias of the property the Grid model is representing.</param>
        public static GridDataModel Deserialize(string json, string propertyTypeAlias) {

            // Validate the JSON
            if (json == null || !json.StartsWith("{") || !json.EndsWith("}")) return null;

            // Deserialize the JSON
            JObject obj = JObject.Parse(json);

            // Parse basic properties
            GridDataModel model = new GridDataModel(obj) {
                Raw = json,
                Name = obj.GetString("name"),
                PropertyAlias = propertyTypeAlias
            };

            // Parse the sections
            model.Sections = obj.GetArray("sections", x => GridSection.Parse(model, x)) ?? new GridSection[0];

            // Return the model
            return model;

        }

        /// <summary>
        /// Parses the specified <paramref name="obj"/> into an instance of <see cref="GridDataModel"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        [Obsolete("Use Deserialize method instead")]
        public static GridDataModel Parse(JObject obj) {
            if (obj == null) return null;
            return new GridDataModel(obj) {
                Raw = obj.ToString(),
                Name = obj.GetString("name"),
                Sections = obj.GetArray("sections", x => GridSection.Parse(null, obj)) ?? new GridSection[0]
            };
        }

        #endregion

    }

}