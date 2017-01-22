using System.Web;
using System.Web.Mvc;

namespace Skybrud.Umbraco.GridData.Extensions {

    /// <summary>
    /// Class holding various extension methods for using the typed Grid.
    /// </summary>
    public static partial class TypedGridExtensionMethods {
        
        /// <summary>
        /// Gets the HTML of the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="helper">The instance of <see cref="HtmlHelper"/> used for rendering the control.</param>
        /// <param name="control">The control to be rendered.</param>
        /// <returns>An instance of <see cref="HtmlString"/>.</returns>
        public static HtmlString RenderGridControl(this HtmlHelper helper, GridControl control) {
            if (helper == null || control == null) return new HtmlString("");
            return control.GetHtml(helper);
        }

        /// <summary>
        /// Gets the HTML of the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="helper">The instance of <see cref="HtmlHelper"/> used for rendering the control.</param>
        /// <param name="control">The control to be rendered.</param>
        /// <param name="partial">The partial view to be used for the rendering.</param>
        /// <returns>An instance of <see cref="HtmlString"/>.</returns>
        public static HtmlString RenderGridControl(this HtmlHelper helper, GridControl control, string partial) {
            if (helper == null || control == null) return new HtmlString("");
            return control.GetHtml(helper, partial);
        }

    }

}