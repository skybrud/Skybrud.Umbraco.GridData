using Skybrud.Umbraco.GridData.Interfaces;
using Umbraco.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Converters {

    /// <summary>
    /// Interface decribing a grid converter for working with <see cref="IPublishedElement"/>.
    /// </summary>
    public interface IGridElementConverter : IGridConverter {

        /// <summary>
        /// Attempts to get the the searchable text for the specified <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="text">When this method returns, contains the searchable text for <paramref name="element"/>.</param>
        /// <returns><c>true</c> if <paramref name="element"/> was recognized by this converter; otherwise <c>false</c>.</returns>
        bool TryGetSearchableText(IPublishedElement element, out string text);

    }

}