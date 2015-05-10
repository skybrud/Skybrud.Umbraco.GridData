using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Skybrud.Umbraco.GridData.Extensions {

    /// <summary>
    /// Class holding various extension methods for using the typed Grid.
    /// </summary>
    public static class TypedGridExtensionMethods {

        #region Constants

        /// <summary>
        /// Gets the default property alias of the Grid.
        /// </summary>
        public const string DefaultPropertyAlias = "bodyText";

        /// <summary>
        /// Gets the default framework for rendering the Grid.
        /// </summary>
        public const string DefaultFramework = "bootstrap3";

        #endregion

        #region Static methods

        /// <summary>
        /// Gets the model for the typed Grid.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        public static GridDataModel GetTypedGrid(this IPublishedContent content) {
            if (!GridPropertyValueConverter.IsEnabled) throw new Exception("The property value conveter for GridDataModel has been disabled");
            return content.GetPropertyValue<GridDataModel>(DefaultPropertyAlias);
        }

        /// <summary>
        /// Gets the model for the typed Grid.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static GridDataModel GetTypedGrid(this IPublishedContent content, string propertyAlias) {
            if (!GridPropertyValueConverter.IsEnabled) throw new Exception("The property value conveter for GridDataModel has been disabled");
            return content.GetPropertyValue<GridDataModel>(propertyAlias);
        }

        /// <summary>
        /// Gets the model for the typed Grid.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        public static GridDataModel GetGridDataModel(this IPublishedContent content) {
            if (!GridPropertyValueConverter.IsEnabled) throw new Exception("The property value conveter for GridDataModel has been disabled");
            return content.GetPropertyValue<GridDataModel>(DefaultPropertyAlias);
        }

        /// <summary>
        /// Gets the model for the typed Grid.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static GridDataModel GetGridDataModel(this IPublishedContent content, string propertyAlias) {
            if (!GridPropertyValueConverter.IsEnabled) throw new Exception("The property value conveter for GridDataModel has been disabled");
            return content.GetPropertyValue<GridDataModel>(propertyAlias);
        }

        /// <summary>
        /// Gets the HTML for the Grid model based on the specified <code>framework</code>.
        /// </summary>
        /// <param name="html">The instance of <code>HtmlHelper</code>.</param>
        /// <param name="model">The Grid model to be rendered.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, GridDataModel model, string framework = DefaultFramework) {

            // Return an empty string if the model is empty.
            if (model == null) return new HtmlString(String.Empty);

            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, model);

        }

        /// <summary>
        /// Gets the HTML for the Grid model based on the specified <code>framework</code>.
        /// </summary>
        /// <param name="html">The instance of <code>HtmlHelper</code>.</param>
        /// <param name="property">The property holding the Grid model.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, IPublishedProperty property, string framework = DefaultFramework) {
            
            // Check whether the property value is empty
            if (String.IsNullOrWhiteSpace(property.Value as string)) return new HtmlString(String.Empty);

            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, property.Value);
        
        }

        /// <summary>
        /// Gets the HTML for the Grid model based on default options.
        /// </summary>
        /// <param name="html">The instance of <code>HtmlHelper</code>.</param>
        /// <param name="content">The parent content item.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, IPublishedContent content) {
            return html.GetTypedGridHtml(content, DefaultPropertyAlias, DefaultFramework);
        }

        /// <summary>
        /// Gets the HTML for the Grid model based on default options.
        /// </summary>
        /// <param name="html">The instance of <code>HtmlHelper</code>.</param>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, IPublishedContent content, string propertyAlias) {
            return html.GetTypedGridHtml(content, propertyAlias, DefaultFramework);
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="html">The instance of <code>HtmlHelper</code>.</param>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, IPublishedContent content, string propertyAlias, string framework) {

            // Get the property with the specifeid alias
            IPublishedProperty property = content.GetProperty(propertyAlias);
            if (property == null) throw new NullReferenceException("No property type found with alias " + propertyAlias);

            // Check whether the property value is empty
            if (String.IsNullOrEmpty(property.Value as string)) return new HtmlString(String.Empty);
            
            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, property.Value);
        
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="property">The property holding the Grid model.</param>
        /// <param name="html">The instance of <code>HtmlHelper</code>.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this IPublishedProperty property, HtmlHelper html, string framework = DefaultFramework) {
            
            // Check whether the property value is empty
            if (String.IsNullOrEmpty(property.Value as string)) return new HtmlString(String.Empty);

            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, property.Value);
        
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="html">The instance of <code>HtmlHelper</code>.</param>
        public static HtmlString GetTypedGridHtml(this IPublishedContent content, HtmlHelper html) {
            return GetTypedGridHtml(content, html, DefaultPropertyAlias, DefaultFramework);
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="html">The instance of <code>HtmlHelper</code>.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static HtmlString GetTypedGridHtml(this IPublishedContent content, HtmlHelper html, string propertyAlias) {
            return GetTypedGridHtml(content, html, propertyAlias, DefaultFramework);
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="html">The instance of <code>HtmlHelper</code>.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this IPublishedContent content, HtmlHelper html, string propertyAlias, string framework) {
            
            // Get the property with the specifeid alias
            IPublishedProperty property = content.GetProperty(propertyAlias);
            if (property == null) throw new NullReferenceException("No property type found with alias " + propertyAlias);

            // Check whether the property value is empty
            if (String.IsNullOrEmpty(property.Value as string)) return new HtmlString(String.Empty);

            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, property.Value);
        
        }

        #endregion

    }

}