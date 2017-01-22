using System.Web;
using System.Web.Mvc;

namespace Skybrud.Umbraco.GridData.Extensions {

    /// <summary>
    /// Class holding various extension methods for using the typed Grid.
    /// </summary>
    public static partial class TypedGridExtensionMethods {
        
        /// <summary>
        /// Gets the HTML of the specified <paramref name="row"/>.
        /// </summary>
        /// <param name="helper">The instance of <see cref="HtmlHelper"/> used for rendering the row.</param>
        /// <param name="row">The row to be rendered.</param>
        /// <returns>An instance of <see cref="HtmlString"/>.</returns>
        public static HtmlString RenderGridRow(this HtmlHelper helper, GridRow row) {
            if (helper == null || row == null) return new HtmlString("");
            return row.GetHtml(helper);
        }

        /// <summary>
        /// Gets the HTML of the specified <paramref name="row"/>.
        /// </summary>
        /// <param name="helper">The instance of <see cref="HtmlHelper"/> used for rendering the row.</param>
        /// <param name="row">The row to be rendered.</param>
        /// <param name="partial">The partial view to be used for the rendering.</param>
        /// <returns>An instance of <see cref="HtmlString"/>.</returns>
        public static HtmlString RenderGridRow(this HtmlHelper helper, GridRow row, string partial) {
            if (helper == null || row == null) return new HtmlString("");
            return row.GetHtml(helper, partial);
        }

    }

}