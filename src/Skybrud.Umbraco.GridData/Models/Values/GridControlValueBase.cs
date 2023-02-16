using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Abstract class with a basic implementation of the <see cref="IGridControlValue"/> interface.
    /// </summary>
    public abstract class GridControlValueBase : IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the underlying instance of <see cref="JToken"/> the value was parsed from.
        /// </summary>
        [JsonIgnore]
        public JToken Json { get; }

        /// <summary>
        /// Gets a reference to the parent <see cref="GridControl"/>.
        /// </summary>
        [JsonIgnore]
        public GridControl Control { get; }

        /// <summary>
        /// Gets whether the control is valid (eg. whether it has a value).
        /// </summary>
        [JsonIgnore]
        public virtual bool IsValid => true;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <see cref="JToken"/> representing the value of the control.</param>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the parent grid control.</param>
        protected GridControlValueBase(JToken json, GridControl control) {
            Json = json;
            Control = control;
        }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> object.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the parent grid control.</param>
        protected GridControlValueBase(GridControl control) {
            Json = control.JObject.GetObject("value")!;
            Control = control;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Writes a string representation of this value to <paramref name="writer"/>.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <param name="writer">The writer.</param>
        public virtual void WriteSearchableText(GridContext context, TextWriter writer) { }

        /// <summary>
        /// Returns a string representation of this value.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>A string representation of this value.</returns>
        public virtual string GetSearchableText(GridContext context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        #endregion

    }

    /// <summary>
    /// Abstract class with a basic implementation of the <see cref="IGridControlValue"/> interface.
    /// </summary>
    public abstract class GridControlValueBase<TJson> : GridControlValueBase where TJson : JToken {

        /// <summary>
        /// Gets a reference to the underlying instance of <see cref="JToken"/> the value was parsed from.
        /// </summary>
        [JsonIgnore]
        public new TJson Json => (TJson) base.Json;

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="json"/> object.
        /// </summary>
        /// <param name="json">An instance of <typeparamref name="TJson"/> representing the value of the control.</param>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the parent grid control.</param>
        protected GridControlValueBase(TJson json, GridControl control) : base(json, control) { }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> object.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the parent grid control.</param>
        protected GridControlValueBase(GridControl control) : base(control) { }

    }

}