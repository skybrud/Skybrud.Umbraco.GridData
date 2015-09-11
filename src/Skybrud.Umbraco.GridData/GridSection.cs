using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing a section in an Umbraco Grid.
    /// </summary>
    public class GridSection : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <code>GridDataModel</code>.
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
        /// Gets the first row of the section. If the section doesn't contain
        /// any rows, this property will return <code>NULL</code>.
        /// </summary>
        public GridRow FirstRow {
            get { return Rows.FirstOrDefault(); }
        }

        /// <summary>
        /// Gets the last row of the section. If the section doesn't contain any rows, this property will return <code>NULL</code>.
        /// </summary>
        public GridRow LastRow {
            get { return Rows.LastOrDefault(); }
        }

        #endregion

        #region Constructors

        protected GridSection(JObject obj) : base(obj) { }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a section from the specified <code>obj</code>.
        /// </summary>
        /// <param name="model">The parent model of the section.</param>
        /// <param name="obj">The instance of <code>JObject</code> to be parsed.</param>
        public static GridSection Parse(GridDataModel model, JObject obj) {

            // Some input validation
            if (obj == null) throw new ArgumentNullException("obj");

            // Parse basic properties
            GridSection section = new GridSection(obj) {
                Model = model,
                Grid = obj.GetInt32("grid")
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