using System;
using System.Linq;
using Skybrud.Umbraco.GridData.Converters;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Singleton class used for configuring and using the grid.
    /// </summary>
    public class GridContext {

        #region Private fields

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the <see cref="GridContext"/> class.
        /// </summary>
        public static readonly GridContext Current = new GridContext();
        
        /// <summary>
        /// Gets the collection of Grid converters.
        /// </summary>
        public GridConverterCollection Converters { get; } = new GridConverterCollection();

        #endregion

        #region Constructors

        private GridContext() { }

        #endregion

        #region Member methods
        
        /// <summary>
        /// Gets the searchable text for the specified <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The searchable text.</returns>
        public virtual string GetSearchableText(IPublishedElement element) {
            foreach (IGridElementConverter converter in Converters.OfType<IGridElementConverter>()) {
                try {
                    if (converter.TryGetSearchableText(element, out string text)) return text;
                } catch (Exception ex)  {
                    global::Umbraco.Core.Composing.Current.Logger.Error<GridContext>(ex, $"Converter of type {converter} failed for GetSearchableText()");
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
            foreach (IGridConverter converter in Converters) {
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