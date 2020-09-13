using Skybrud.Umbraco.GridData.Interfaces;
using Umbraco.Core.Models.PublishedContent;

namespace Skybrud.Umbraco.GridData.Converters {
    
    /// <summary>
    /// Interface decribing a grid converter for working with <see cref="IPublishedElement"/>.
    /// </summary>
    public interface IGridElementConverter : IGridConverter {

        bool GetSearchableText(GridContext context, IPublishedElement element, out string text);

    }

}