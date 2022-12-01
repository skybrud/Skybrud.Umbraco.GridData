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
    public class GridControlTextValue : IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        public GridControl? Control { get; }

        /// <summary>
        /// Gets a reference to the underlying instance of <see cref="JToken"/>.
        /// </summary>
        public JToken? JToken { get; }

        /// <summary>
        /// Gets a string representing the value.
        /// </summary>
        public string? Value { get; protected set; }

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlTextValue"/>, this means
        /// checking whether the specified text is not an empty string (using <see cref="string.IsNullOrWhiteSpace"/>).
        /// </summary>
        public virtual bool? IsValid => !string.IsNullOrWhiteSpace(Value);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="token"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="token">An instance of <see cref="JToken"/> representing the value of the control.</param>
        public GridControlTextValue(GridControl control, JToken? token) {
            Control = control;
            JToken = token;
            Value = token?.Value<string>();
        }

        #endregion

        #region Member methods
        
        public virtual void WriteSearchableText(GridContext? context, TextWriter writer) {
            if (Value != null) writer.WriteLine(Value);
        }
        
        public virtual string? GetSearchableText(GridContext? context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        /// <summary>
        /// Gets a string representing the raw value of the control.
        /// </summary>
        /// <returns>An instance of <see cref="string"/>.</returns>
        public override string? ToString() {
            return Value;
        }

        #endregion

    }

}