using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing the value/model saved by an Umbraco Grid property.
    /// </summary>
    public class GridDataModel : GridJsonObject {

        #region Properties
        
        /// <summary>
        /// Gets whether the model is valid.
        /// </summary>
        public bool IsValid { get; private set; }

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

        #region Exposing properties from the JSON due to http://issues.umbraco.org/issue/U4-5750

        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Same as <code>Name</code>.
        /// </summary>
        [Obsolete]
        public string name {
            get { return Name; }
        }

        /// <summary>
        /// Gets the underlying JSON array for the <code>sections</code> property. 
        /// </summary>
        [Obsolete]
        public dynamic sections {
            get { return ((dynamic) JObject).sections; }
        }
        
        // ReSharper restore InconsistentNaming

        #endregion
        
        #endregion

        #region Constructors

        private GridDataModel(JObject obj) : base(obj) {
            Sections = new GridSection[0];
            IsValid = false;
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
        /// Gets an array of all nested controls with the specified editor <code>alias</code>. 
        /// </summary>
        /// <param name="alias">The editor alias of controls to be returned.</param>
        public GridControl[] GetAllControls(string alias) {
            return GetAllControls(x => x.Editor.Alias == alias);
        }

        /// <summary>
        /// Gets an array of all nested controls matching the specified <code>predicate</code>. 
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

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an empty (and invalid) model. This method can be used to get a fallback value for
        /// when an actual Grid model isn't available.
        /// </summary>
        /// <param name="propertyTypeAlias">The alias of the doctype property</param>
        public static GridDataModel GetEmptyModel(string propertyTypeAlias="")
        {
            var gdm = new GridDataModel(null);
            gdm.PropertyAlias = propertyTypeAlias;
            return gdm;
        }

        /// <summary>
        /// Deserializes the specified JSON string into an instance of <code>GridDataModel</code>.
        /// </summary>
        /// <param name="json">The JSON string to be deserialized.</param>
        /// <param name="propertyTypeAlias">The alias of the doctype property providing the grid data</param>
        public static GridDataModel Deserialize(string json, string propertyTypeAlias="") {

            // Validate the JSON
            if (json == null || !json.StartsWith("{") || !json.EndsWith("}")) return null;

            // Deserialize the JSON
            JObject obj = JObject.Parse(json);

            // Parse basic properties
            GridDataModel model = new GridDataModel(obj) {
                Raw = json,
                Name = obj.GetString("name"),
                IsValid = true,
                PropertyAlias = propertyTypeAlias
            };

            // Parse the sections
            model.Sections = obj.GetArray("sections", x => GridSection.Parse(model, x)) ?? new GridSection[0];

            // Return the model
            return model;

        }

        /// <summary>
        /// Parses the specified <code>JObject</code> into an instance of <code>GridDataModel</code>.
        /// </summary>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
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