using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Factories;
using Umbraco.Cms.Infrastructure.ModelsBuilder;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Class representing a section in an Umbraco Grid.
    /// </summary>
    public class GridSection : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets the section name.
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// Gets a reference to the parent <see cref="GridDataModel"/>.
        /// </summary>
        public GridDataModel? Model { get; private set; }

        /// <summary>
        /// Gets the overall column width of the section.
        /// </summary>
        public int? Grid { get; private set; }

        /// <summary>
        /// Gets an array of all rows in the sections.
        /// </summary>
        public GridRow[]? Rows { get; private set; }

        /// <summary>
        /// Gets whether the section has any rows.
        /// </summary>
        public bool? HasRows => Rows?.Length > 0;

        /// <summary>
        /// Gets the first row of the section. If the section doesn't contain any rows, this property will return <c>null</c>.
        /// </summary>
        public GridRow? FirstRow => Rows?.FirstOrDefault();

        /// <summary>
        /// Gets the last row of the section. If the section doesn't contain any rows, this property will return <c>null</c>.
        /// </summary>
        public GridRow? LastRow => Rows?.LastOrDefault();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object, <paramref name="grid"/> model and <paramref name="factory"/>.
        /// </summary>
        /// <param name="json">An instance of <see cref="JObject"/> representing the section.</param>
        /// <param name="grid">The parent grid model.</param>
        /// <param name="factory">The factory used for parsing subsequent parts of the grid.</param>
        public GridSection(JObject json, GridDataModel grid, IGridFactory factory) : base(json) {

            Model = grid;
            Grid = json.GetInt32("grid");
            Name = grid.Name;
            Rows = json.GetArray("rows", x => factory.CreateGridRow(x, this)) ?? new GridRow[0];

            // Update "PreviousRow" and "NextRow" properties
            for (int i = 1; i < Rows.Length; i++)
            {
                Rows[i - 1].NextRow = Rows[i];
                Rows[i].PreviousRow = Rows[i - 1];
            }

        }

        #endregion

        #region Member methods
        
        public void WriteSearchableText(GridContext context, TextWriter writer) {
            if (Rows != null)
            {
                foreach (GridRow? row in Rows)
                {
                    row?.WriteSearchableText(context, writer);
                }
            }
        }

        /// <summary>
        /// Gets a textual representation of the section - eg. to be used in Examine.
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

    }

}