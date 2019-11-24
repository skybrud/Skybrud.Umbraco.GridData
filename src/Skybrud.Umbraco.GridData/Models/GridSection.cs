using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Factories;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Class representing a section in an Umbraco Grid.
    /// </summary>
    public class GridSection : GridJsonObject, IGridSection {

        #region Properties

        /// <summary>
        /// Gets the section name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a reference to the parent <see cref="IGridDataModel"/>.
        /// </summary>
        public IGridDataModel Model { get; }

        /// <summary>
        /// Gets the overall column width of the section.
        /// </summary>
        public int Grid { get; }

        /// <summary>
        /// Gets an array of all rows in the sections.
        /// </summary>
        public IGridRow[] Rows { get; }

        /// <summary>
        /// Gets whether the section has any rows.
        /// </summary>
        public bool HasRows => Rows.Length > 0;

        /// <summary>
        /// Gets the first row of the section. If the section doesn't contain any rows, this property will return
        /// <code>null</code>.
        /// </summary>
        public IGridRow FirstRow => Rows.FirstOrDefault();

        /// <summary>
        /// Gets the last row of the section. If the section doesn't contain any rows, this property will return
        /// <code>null</code>.
        /// </summary>
        public IGridRow LastRow => Rows.LastOrDefault();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object, <paramref name="grid"/> model and <paramref name="factory"/>.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the section.</param>
        /// <param name="grid">The parent grid model.</param>
        /// <param name="factory">The factory used for parsing subsequent parts of the grid.</param>
        public GridSection(JObject json, IGridDataModel grid, IGridFactory factory) : base(json) {

            Model = grid;
            Grid = json.GetInt32("grid");
            Name = grid.Name;
            Rows = json.GetArray("rows", x => factory.CreateGridRow(json, this)) ?? new IGridRow[0];

            // Update "PreviousRow" and "NextRow" properties
            for (int i = 1; i < Rows.Length; i++) {
                // TODO: Due to the factory, we can no longer assume rows are GridRow
                ((GridRow) Rows[i - 1]).NextRow = Rows[i];
                ((GridRow) Rows[i]).PreviousRow = Rows[i - 1];
            }

        }

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
        public HtmlString GetHtml(HtmlHelper helper) {
            return GetHtml(helper, Name);
        }

        /// <summary>
        /// Generates the HTML for the Grid row.
        /// </summary>
        /// <param name="helper">The <see cref="HtmlHelper"/> used for rendering the Grid row.</param>
        /// <param name="partial">The alias or virtual path to the partial view for rendering the Grid row.</param>
        /// <returns>Returns the Grid row as an instance of <see cref="HtmlString"/>.</returns>
        public HtmlString GetHtml(HtmlHelper helper, string partial) {

            // Some input validation
            if (helper == null) throw new ArgumentNullException(nameof(helper));
            if (string.IsNullOrWhiteSpace(partial)) throw new ArgumentNullException(nameof(partial));

            // Prepend the path to the "Sections" folder if not already specified
            if (GridUtils.IsValidPartialName(partial)) {
                partial = "TypedGrid/Sections/" + partial;
            }

            // Render the partial view
            return helper.Partial(partial, this);

        }

        #endregion

    }

}