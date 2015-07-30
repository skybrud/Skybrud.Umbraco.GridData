using System;
using System.IO;
using System.Web;
using Skybrud.Umbraco.GridData.Converters;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Core.Configuration.Grid;
using Umbraco.Core.IO;
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

        /// <summary>
        /// Gets or sets a reference to the grid configuration to be used. In a web project where
        /// <code>HttpContext.Current</code> is present, this property will automatically be initialized with the
        /// default configuration.
        /// </summary>
        public IGridConfig Config { get; set; } 

        #endregion

        #region Constructors

        private GridContext() {
            
            if (HttpContext.Current != null) {
                Config = UmbracoConfig.For.GridConfig(
                    ApplicationContext.Current.ProfilingLogger.Logger,
                    ApplicationContext.Current.ApplicationCache.RuntimeCache,
                    new DirectoryInfo(HttpContext.Current.Server.MapPath(SystemDirectories.AppPlugins)),
                    new DirectoryInfo(HttpContext.Current.Server.MapPath(SystemDirectories.Config)),
                    HttpContext.Current.IsDebuggingEnabled
                );
            }

        }

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