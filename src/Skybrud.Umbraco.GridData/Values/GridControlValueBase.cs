using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.Umbraco.GridData.Values {
    
    /// <summary>
    /// Abstract class with a basic implementation of the <see cref="IGridControlValue"/> interface.
    /// </summary>
    public abstract class GridControlValueBase : GridJsonObject, IGridControlValue {

        #region Properties

        [JsonIgnore]
        public GridControl Control { get; private set; }
        
        [JsonIgnore]
        public virtual bool IsValid {
            get { return true; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the control.</param>
        /// <param name="obj">An instance of <see cref="JObject"/> representing the value of the control.</param>
        protected GridControlValueBase(GridControl control, JObject obj) : base(obj) {
            Control = control;
        }

        #endregion

        #region Member methods

        public virtual string GetSearchableText() {
            return "";
        }

        #endregion

    }

}