using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData {
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    /// <summary>
    /// Class representing a section in an Umbraco Grid.
    /// </summary>
    public class GridSection : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets the section name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a reference to the parent <see cref="GridDataModel"/>.
        /// </summary>
        public GridDataModel Model { get; private set; }

        /// <summary>
        /// Gets the overall column width of the section.
        /// </summary>
        public int Grid { get; private set; }

        /// <summary>
        /// Gets an array of all rows in the sections.
        /// </summary>
        public GridRow[] Rows { get; private set; }

        /// <summary>
        /// Gets whether the section has any rows.
        /// </summary>
        public bool HasRows {
            get { return Rows.Length > 0; }
        }

        /// <summary>
        /// Gets the first row of the section. If the section doesn't contain any rows, this property will return
        /// <code>null</code>.
        /// </summary>
        public GridRow FirstRow {
            get { return Rows.FirstOrDefault(); }
        }

        /// <summary>
        /// Gets the last row of the section. If the section doesn't contain any rows, this property will return
        /// <code>null</code>.
        /// </summary>
        public GridRow LastRow {
            get { return Rows.LastOrDefault(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the section.</param>
        protected GridSection(JObject obj) : base(obj) { }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets a textual representation of the section - eg. to be used in Examine.
        /// </summary>
        /// <returns>Returns an instance of <see cref="System.String"/> representing the value of the section.</returns>
        public virtual string GetSearchableText() {
            return Rows.Aggregate("", (current, row) => current + row.GetSearchableText());
        }

        /// <summary>
        /// Generates the HTML for the Grid row.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid row.</param>
        /// <returns>Returns the Grid row as an instance of <see cref="HtmlString"/>.</returns>
        public HtmlString GetHtml(HtmlHelper helper)
        {
            return GetHtml(helper, Name);
        }

        /// <summary>
        /// Generates the HTML for the Grid row.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid row.</param>
        /// <param name="partial">The alias or virtual path to the partial view for rendering the Grid row.</param>
        /// <returns>Returns the Grid row as an instance of <see cref="HtmlString"/>.</returns>
        public HtmlString GetHtml(HtmlHelper helper, string partial)
        {

            // Some input validation
            if (helper == null) throw new ArgumentNullException("helper");
            if (String.IsNullOrWhiteSpace(partial)) throw new ArgumentNullException("partial");

            // Prepend the path to the "Sections" folder if not already specified
            if (Regex.IsMatch(partial, "^[a-zA-Z0-9-_]+$"))
            {
                partial = "TypedGrid/Sections/" + partial;
            }

            // Render the partial view
            return helper.Partial(partial, this);

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a section from the specified <paramref name="model"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="model">The parent model of the section.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridSection Parse(GridDataModel model, JObject obj) {

            // Some input validation
            if (obj == null) throw new ArgumentNullException("obj");

            // Parse basic properties
            GridSection section = new GridSection(obj) {
                Model = model,
                Grid = obj.GetInt32("grid"),
                Name = model.Name
            };

            // Parse the rows
            section.Rows = obj.GetArray("rows", x => GridRow.Parse(section, x)) ?? new GridRow[0];

            // Update "PreviousRow" and "NextRow" properties
            for (int i = 1; i < section.Rows.Length; i++) {
                section.Rows[i - 1].NextRow = section.Rows[i];
                section.Rows[i].PreviousRow = section.Rows[i - 1];
            }

            // Return the section
            return section;
        }

        #endregion

    }

}