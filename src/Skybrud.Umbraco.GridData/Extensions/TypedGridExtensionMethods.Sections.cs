using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Skybrud.Umbraco.GridData.Extensions {

    /// <summary>
    /// Class holding various extension methods for using the typed Grid.
    /// </summary>
    public static partial class TypedGridExtensionMethods {

        /// <summary>
        /// Gets the HTML of the specified <paramref name="section"/>.
        /// </summary>
        /// <param name="helper">The instance of <see cref="HtmlHelper"/> used for rendering the row.</param>
        /// <param name="section">The section to be rendered.</param>
        /// <returns>An instance of <see cref="HtmlString"/>.</returns>
        public static HtmlString RenderGridSection(this HtmlHelper helper, GridSection section) {
            if (helper == null || section == null) return new HtmlString("");
            return section.GetHtml(helper);
        }

        /// <summary>
        /// Gets the HTML of the specified <paramref name="section"/>.
        /// </summary>
        /// <param name="helper">The instance of <see cref="HtmlHelper"/> used for rendering the row.</param>
        /// <param name="section">The section to be rendered.</param>
        /// <param name="partial">The partial view to be used for the rendering.</param>
        /// <returns>An instance of <see cref="HtmlString"/>.</returns>
        public static HtmlString RenderGridSection(this HtmlHelper helper, GridSection section, string partial) {
            if (helper == null || section == null) return new HtmlString("");
            return section.GetHtml(helper, partial);
        }

        /// <summary>
        /// Gets the HTML of the specified <paramref name="section" /> or falls back to <see cref="fallbackPartial"/> if no section view is found.
        /// </summary>
        /// <param name="helper">The instance of <see cref="T:System.Web.Mvc.HtmlHelper" /> used for rendering the section.</param>
        /// <param name="section">The section to be rendered.</param>
        /// <param name="fallbackPartial">The fallback partial view to be used if no section partial is found by <see cref="GridSection.Name"/> for the rendering.</param>
        /// <returns>An instance of <see cref="T:System.Web.HtmlString" />.</returns>
        public static HtmlString RenderGridSectionOrFallback(this HtmlHelper helper, GridSection section, string fallbackPartial) {

            if (helper == null || section == null) return new HtmlString("");

            // Determine the partial view
            string partial = section.Name;
            if (GridUtils.IsValidPartialName(partial)) {
                partial = "TypedGrid/Sections/" + partial;
            }

            // Do we have a partial view or should we use the fallback?
            return helper.ViewExists(partial) ? helper.RenderGridSection(section) : helper.RenderGridSection(section, fallbackPartial);

        }

    }

}