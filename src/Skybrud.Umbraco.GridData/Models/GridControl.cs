using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Models.Config;
using Skybrud.Umbraco.GridData.Models.Values;

namespace Skybrud.Umbraco.GridData.Models {

    /// <summary>
    /// Class representing a control in an Umbraco Grid.
    /// </summary>
    public class GridControl : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets a reference to the entire <see cref="GridDataModel"/>.
        /// </summary>
        [JsonIgnore]
        public GridDataModel Model => Section.Model;

        /// <summary>
        /// Gets a reference to the parent <see cref="GridSection"/>.
        /// </summary>
        [JsonIgnore]
        public GridSection Section => Row.Section;

        /// <summary>
        /// Gets a reference to the parent <see cref="GridRow"/>.
        /// </summary>
        [JsonIgnore]
        public GridRow Row => Area.Row;

        /// <summary>
        /// Gets a reference to the parent <see cref="GridArea"/>.
        /// </summary>
        [JsonIgnore]
        public GridArea Area { get; }

        /// <summary>
        /// Gets the value of the control. Alternately use the <see cref="GetValue{T}"/> method to get the type safe value.
        /// </summary>
        [JsonProperty("value")]
        public IGridControlValue? Value { get; internal set; }

        /// <summary>
        /// Gets a reference to the editor of the control.
        /// </summary>
        [JsonProperty("editor")]
        public GridEditor Editor { get; internal set; }

        /// <summary>
        /// Gets a reference to the previous control.
        /// </summary>
        public GridControl? PreviousControl { get; internal set; }

        /// <summary>
        /// Gets a reference to the next control.
        /// </summary>
        public GridControl? NextControl { get; internal set; }

        /// <summary>
        /// Gets whether the control and it's value is valid.
        /// </summary>
        [JsonIgnore]
        public bool IsValid => Value is { IsValid: true };

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance from the specified <paramref name="json"/> object and <paramref name="area"/>.
        /// </summary>
        /// <param name="json">The instance of <see cref="JObject"/> representing the control.</param>
        /// <param name="area">An instance of <see cref="GridArea"/> representing the parent area.</param>
        internal GridControl(JObject json, GridArea area) : base(json) {
            Area = area;
            Value = null!;
            Editor = null!;
        }

        internal GridControl(GridControl control) : base(control.JObject) {
            Area = control.Area;
            Value = control.Value;
            Editor = control.Editor;
            PreviousControl = control.PreviousControl;
            NextControl = control.NextControl;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the value of the control casted to the type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value to be returned.</typeparam>
        public T? GetValue<T>() where T : IGridControlValue {
            return Value is T value ? value : default;
        }

        /// <summary>
        /// Writes a string representation of the control to <paramref name="writer"/>.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <param name="writer">The writer.</param>
        public void WriteSearchableText(GridContext context, TextWriter writer) {
            Value?.WriteSearchableText(context, writer);
        }

        /// <summary>
        /// Returns the value of the control as a searchable text - eg. to be used in Examine.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <returns>An instance of <see cref="string"/> with the value as a searchable text.</returns>
        public string GetSearchableText(GridContext context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        #endregion

    }

    /// <summary>
    /// Class representing a grid control where the value is of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class GridControl<TValue> : GridControl where TValue : IGridControlValue {

        /// <summary>
        /// Gets the value of the control.
        /// </summary>
        [JsonProperty("value")]
        public new TValue Value => (TValue) base.Value!;

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The control.</param>
        public GridControl(GridControl control) : base(control) { }

    }

    /// <summary>
    /// Class representing a grid control where the value is of type <typeparamref name="TValue"/> and an editor with a config of type <typeparamref name="TConfig"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TConfig">The type of the editor config.</typeparam>
    public class GridControl<TValue, TConfig> : GridControl<TValue> where TValue : IGridControlValue where TConfig : IGridEditorConfig {

        /// <summary>
        /// Gets a reference to the editor of the control.
        /// </summary>
        [JsonProperty("editor")]
        public new GridEditor<TConfig> Editor { get; }

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="editor"/>.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="editor">The editor.</param>
        public GridControl(GridControl control, GridEditor<TConfig> editor) : base(control) {
            Editor = editor;
        }

    }

}