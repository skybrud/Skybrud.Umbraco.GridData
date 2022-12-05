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

        public GridContext(ILogger<GridContext> logger, GridConverterCollection converterCollection) {
            _logger = logger;
            _converterCollection = converterCollection;
        }

        #endregion

        #region Member methods

        public virtual void WriteSearchableText(IPublishedElement element, TextWriter writer) {
            foreach (IGridConverter converter in _converterCollection) {
                try {
                    if (converter.WriteSearchableText(this, element, writer)) return;
                } catch (Exception ex) {
                    _logger.LogError(ex, $"Converter of type {converter} failed for WriteSearchableText()");
                }
            }
        }

        public virtual string GetSearchableText(IPublishedElement element) {
            StringBuilder sb = new();
            using TextWriter writer = new StringWriter(sb);
            WriteSearchableText(element, writer);
            return sb.ToString();
        }

        #endregion

    }

}