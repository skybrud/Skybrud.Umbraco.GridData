using System.Web;
using System.Web.Mvc;

namespace Skybrud.Umbraco.GridData.Extensions {
    using System.Text.RegularExpressions;

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


        /// <summary>
        /// Gets the HTML of the specified <paramref name="row" /> or fallsback to <see cref="fallbackPartial"/> if no row view is found.
        /// </summary>
        /// <param name="helper">The instance of <see cref="T:System.Web.Mvc.HtmlHelper" /> used for rendering the row.</param>
        /// <param name="row">The row to be rendered.</param>
        /// <param name="fallbackPartial">The fallback partial view to be used if no row partial is found by <see cref="GridRow.Name"/> for the rendering.</param>
        /// <returns>An instance of <see cref="T:System.Web.HtmlString" />.</returns>
        public static HtmlString RenderGridRowOrFallback(this HtmlHelper helper, GridRow row, string fallbackPartial)
        {
            if (helper == null || row == null)
            {
                return new HtmlString(string.Empty);
            }

            var partial = row.Name;
            if (Regex.IsMatch(partial, "^[a-zA-Z0-9-_]+$"))
            {
                partial = "TypedGrid/Rows/" + partial;
            }

            if (helper.ViewExists(partial))
            {
                return helper.RenderGridRow(row);
            }

            return helper.RenderGridRow(row, fallbackPartial);
        }
    }
}