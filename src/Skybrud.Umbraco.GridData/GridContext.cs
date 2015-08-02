using System;
using System.IO;
using System.Web;
using Skybrud.Umbraco.GridData.Converters;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;
using Umbraco.Core.Logging;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Singleton class used for configuring and using the grid.
    /// </summary>
    public class GridContext {

        #region Private fields

        readonly GridConverterCollection _converters = new GridConverterCollection();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the <code>GridContext</code> class.
        /// </summary>
        public static readonly GridContext Current = new GridContext();
        
        /// <summary>
        /// Gets the collection of Grid converters.
        /// </summary>
        public GridConverterCollection Converters {
            get { return _converters; }
        }

        #endregion

        #region Constructors

        private GridContext() { }

        #endregion

        #region Member methods
        
        /// <summary>
        /// Gets an instance of <code>GridControlWrapper</code> based on the specified <code>control</code>.
        /// </summary>
        /// <param name="control">The control to wrap.</param>
        public GridControlWrapper GetControlWrapper(GridControl control) {
            foreach (IGridConverter converter in Converters) {
                try {
                    GridControlWrapper wrapper;
                    if (converter.GetControlWrapper(control, out wrapper)) return wrapper; 
                } catch (Exception ex) {
                    LogHelper.Error<GridContext>("Converter of type " + converter + " failed for GetControlWrapper()", ex);
                }
            }
            return null;
        }

        #endregion

    }

}