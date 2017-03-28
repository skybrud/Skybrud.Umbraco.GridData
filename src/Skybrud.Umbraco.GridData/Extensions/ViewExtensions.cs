namespace Skybrud.Umbraco.GridData.Extensions
{
    using System.Web.Mvc;

    /// <summary>
    /// Class holding various extension methods for handling a view.
    /// </summary>
    internal static class ViewExtensions
    {
        /// <summary>
        /// Check if a view exists.
        /// </summary>
        /// <param name="helper">The instance of <see cref="HtmlHelper"/> used for checking the view.</param>
        /// <param name="viewPath">The partial view to be used for the checking.</param>
        /// <returns>True if view is found else false.</returns>
        public static bool ViewExists(this HtmlHelper helper, string viewPath)
        {
            return ViewEngines.Engines.FindPartialView(helper.ViewContext.Controller.ControllerContext, viewPath).View != null;
        }
    }
}
