using System;
using System.Linq;
using Skybrud.Umbraco.GridData.Converters;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.PublishedCache;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Singleton class used for configuring and using the grid.
    /// </summary>
    public class GridContext {

        #region Private fields

        private readonly GridConverterCollection _converters;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the <see cref="GridContext"/> class.
        /// </summary>
        public static readonly GridContext Current = global::Umbraco.Core.Composing.Current.Factory.GetInstance<GridContext>();

        #endregion

        #region Constructors

        public GridContext(GridConverterCollection converters) {
            _converters = converters;
        }

        #endregion

        #region Member methods

        public virtual string GetSearchableText(IPublishedElement element) {
            foreach (IGridElementConverter converter in _converters.OfType<IGridElementConverter>()) {
                try {
                    if (converter.GetSearchableText(this, element, out string text)) return text;
                } catch (Exception ex)  {
                    global::Umbraco.Core.Composing.Current.Logger.Error<GridContext>(ex, "Converter of type " + converter + " failed for GetSearchableText()");
                }
            }
            return string.Empty;
        }
        
        /// <summary>
        /// Gets an instance of <see cref="GridControlWrapper"/> based on the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The control to wrap.</param>
        /// <returns>An instance of <see cref="GridControlWrapper"/>.</returns>
        public GridControlWrapper GetControlWrapper(GridControl control) {
            foreach (IGridConverter converter in _converters) {
                try {
                    if (converter.GetControlWrapper(control, out GridControlWrapper wrapper)) return wrapper; 
                } catch (Exception ex) {
                    global::Umbraco.Core.Composing.Current.Logger.Error<GridContext>(ex, "Converter of type " + converter + " failed for GetControlWrapper()");
                }
            }
            return null;
        }

        #endregion

    }

}