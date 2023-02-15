using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Skybrud.Umbraco.GridData.Converters;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// Singleton class used for configuring and using the grid.
    /// </summary>
    public class GridContext {

        private readonly ILogger<GridContext> _logger;
        private readonly GridConverterCollection _converterCollection;

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified dependencies.
        /// </summary>
        /// <param name="logger">A reference to the current logger.</param>
        /// <param name="converterCollection">A reference to the current <see cref="GridConverterCollection"/>.</param>
        public GridContext(ILogger<GridContext> logger, GridConverterCollection converterCollection) {
            _logger = logger;
            _converterCollection = converterCollection;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Writes a string representation of <paramref name="element"/> to <paramref name="writer"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="writer">The writer.</param>
        public virtual void WriteSearchableText(IPublishedElement element, TextWriter writer) {
            foreach (IGridConverter converter in _converterCollection) {
                try {
                    if (converter.WriteSearchableText(this, element, writer)) return;
                } catch (Exception ex) {
                    _logger.LogError(ex, $"Converter of type {converter} failed for WriteSearchableText()");
                }
            }
        }

        /// <summary>
        /// Returns a string representation of the specified <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>A string representation of <paramref name="element"/>.</returns>
        public virtual string GetSearchableText(IPublishedElement element) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(element, writer);
            return sb.ToString();
        }

        #endregion

    }

}