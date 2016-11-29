using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Values {

    /// <summary>
    /// Class representing the text value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlTextValue : IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        public GridControl Control { get; private set; }

        /// <summary>
        /// Gets a reference to the underlying instance of <code>JToken</code>.
        /// </summary>
        [JsonIgnore]
        public JToken JToken { get; private set; }
        
        /// <summary>
        /// Gets a string representing the value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; protected set; }

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlTextValue"/>, this means
        /// checking whether the specified text is not an empty string (using <see cref="String.IsNullOrWhiteSpace"/>).
        /// </summary>
        [JsonIgnore]
        public virtual bool IsValid {
            get { return !String.IsNullOrWhiteSpace(Value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <see cref="GridControl"/> and <see cref="JToken"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="token">An instance of <see cref="JToken"/> representing the value of the control.</param>
        protected GridControlTextValue(GridControl control, JToken token) {
            Control = control;
            JToken = token;
            Value = token.Value<string>() + "";
        }

        #endregion

        #region Member methods

        public virtual string GetSearchableText() {
            return Value;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets a text value from the specified <see cref="JToken"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="token">The instance of <see cref="JToken"/> to be parsed.</param>
        public static GridControlTextValue Parse(GridControl control, JToken token) {
            return token == null ? null : new GridControlTextValue(control, token);
        }

        #endregion

    }

}