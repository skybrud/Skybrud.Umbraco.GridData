using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skybrud.Umbraco.GridData.Models.Values {
    
    /// <summary>
    /// Abstract class with a basic implementation of the <see cref="IGridControlValue"/> interface.
    /// </summary>
    public abstract class GridControlValueBase : GridJsonObject, IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent <see cref="GridControl"/>.
        /// </summary>
        [JsonIgnore]
        public GridControl? Control { get; }
        
        /// <summary>
        /// Gets whether the control is valid (eg. whether it has a value).
        /// </summary>
        [JsonIgnore]
        public virtual bool? IsValid => true;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the value of the control.</param>
        protected GridControlValueBase(GridControl control, JObject? obj) : base(obj) {
            Control = control;
        }

        #endregion

        #region Member methods
        
        public virtual void WriteSearchableText(GridContext? context, TextWriter writer) { }
        
        public virtual string? GetSearchableText(GridContext? context) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(context, writer);
            return sb.ToString();
        }

        #endregion

    }

}