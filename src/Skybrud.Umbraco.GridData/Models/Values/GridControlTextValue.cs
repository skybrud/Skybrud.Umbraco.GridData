using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Class representing the text value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlTextValue : GridControlValueBase {

        #region Properties

        /// <summary>
        /// Gets a string representing the value.
        /// </summary>
        public string Value { get; protected set; }

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlTextValue"/>, this means
        /// checking whether the specified text is not an empty string (using <see cref="string.IsNullOrWhiteSpace"/>).
        /// </summary>
        public override bool IsValid => !string.IsNullOrWhiteSpace(Value);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the value of the specified grid <paramref name="control"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the parent grid control.</param>
        public GridControlTextValue(GridControl control) : base(control) {
            Value = Json.Value<string>() ?? string.Empty;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Writes a string representation of this value to <paramref name="writer"/>.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <param name="writer">The writer.</param>
        public override void WriteSearchableText(GridContext context, TextWriter writer) {
            writer.WriteLine(Value);
        }

        /// <summary>
        /// Returns a string representation of this value.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>A string representation of this value.</returns>
        public override string GetSearchableText(GridContext context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        /// <summary>
        /// Gets a string representing the raw value of the control.
        /// </summary>
        /// <returns>An instance of <see cref="string"/>.</returns>
        public override string ToString() {
            return Value;
        }

        #endregion

    }

}