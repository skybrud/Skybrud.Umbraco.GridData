using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Extensions.Json;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Class representing a row in an Umbraco Grid.
    /// </summary>
    public class GridRow : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <see cref="GridSection"/>.
        /// </summary>
        public GridSection Section { get; private set; }

        /// <summary>
        /// Gets the unique ID of the row.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the alias of the row. This property is not part of the official Umbraco Grid in 7.2.x,
        /// but will be available in 7.3 according to http://issues.umbraco.org/issue/U4-6533.
        /// </summary>
        public string Alias { get; private set; }

        /// <summary>
        /// Gets the name of the row.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets an array of all areas in the row.
        /// </summary>
        public GridArea[] Areas { get; private set; }

        /// <summary>
        /// Gets a dictionary representing the styles of the row.
        /// </summary>
        public GridDictionary Styles { get; private set; }

        /// <summary>
        /// Gets a dictionary representing the configuration (called Settings in the backoffice) of the row.
        /// </summary>
        public GridDictionary Config { get; private set; }

        /// <summary>
        /// Gets a reference to the previous row.
        /// </summary>
        public GridRow PreviousRow { get; internal set; }

        /// <summary>
        /// Gets a reference to the next row.
        /// </summary>
        public GridRow NextRow { get; internal set; }

        /// <summary>
        /// Gets whether the row has any areas.
        /// </summary>
        public bool HasAreas {
            get { return Areas.Length > 0; }
        }

        /// <summary>
        /// Gets the first area of the row. If the row doesn't contain any areas, this property will return
        /// <code>null</code>.
        /// </summary>
        public GridArea FirstRow {
            get { return Areas.FirstOrDefault(); }
        }

        /// <summary>
        /// Gets the last area of the row. If the row doesn't contain any areas, this property will return
        /// <code>null</code>.
        /// </summary>
        public GridArea LastRow {
            get { return Areas.LastOrDefault(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the row.</param>
        protected GridRow(JObject obj) : base(obj) { }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets an array of all nested controls. 
        /// </summary>
        public GridControl[] GetAllControls() {
            return (
                from area in Areas
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
                from area in Areas
                from control in area.Controls
                where predicate(control)
                select control
            ).ToArray();
        }

        /// <summary>
        /// Gets a textual representation of the row - eg. to be used in Examine.
        /// </summary>
        /// <returns>Returns an instance of <see cref="System.String"/> representing the value of the row.</returns>
        public virtual string GetSearchableText() {
            return Areas.Aggregate("", (current, area) => current + area.GetSearchableText());
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses a row from the specified <code>obj</code>.
        /// </summary>
        /// <param name="section">The parent section of the row.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridRow Parse(GridSection section, JObject obj) {

            // Some input validation
            if (obj == null) throw new ArgumentNullException("obj");
            
            // Parse basic properties
            GridRow row = new GridRow(obj) {
                Section = section,
                Id = obj.GetString("id"),
                Alias = obj.GetString("alias"),
                Name = obj.GetString("name"),
                Styles = obj.GetObject("styles", GridDictionary.Parse),
                Config = obj.GetObject("config", GridDictionary.Parse)
            };

            // Parse the areas
            row.Areas = obj.GetArray("areas", x => GridArea.Parse(row, x)) ?? new GridArea[0];

            // Update "PreviousArea" and "NextArea" properties
            for (int i = 1; i < row.Areas.Length; i++) {
                row.Areas[i - 1].NextArea = row.Areas[i];
                row.Areas[i].PreviousArea = row.Areas[i - 1];
            }

            // Return the row
            return row;

        }

        #endregion

    }

}