using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Skybrud.Umbraco.GridData.Extensions {

    /// <summary>
    /// Class holding various extension methods for using the typed Grid.
    /// </summary>
    public static partial class TypedGridExtensionMethods {

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
            return content.Value<GridDataModel>(DefaultPropertyAlias) ?? GridDataModel.GetEmptyModel();
        }

        /// <summary>
        /// Gets the model for the typed Grid.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static GridDataModel GetTypedGrid(this IPublishedContent content, string propertyAlias) {
            return content.Value<GridDataModel>(propertyAlias) ?? GridDataModel.GetEmptyModel();
        }

        /// <summary>
        /// Gets the model for the typed Grid.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        public static GridDataModel GetGridModel(this IPublishedContent content) {
            return content.Value<GridDataModel>(DefaultPropertyAlias) ?? GridDataModel.GetEmptyModel();
        }

        /// <summary>
        /// Gets the model for the typed Grid.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static GridDataModel GetGridModel(this IPublishedContent content, string propertyAlias) {
            return content.Value<GridDataModel>(propertyAlias) ?? GridDataModel.GetEmptyModel();
        }

        /// <summary>
        /// Gets the HTML for the Grid model based on the specified <paramref name="framework"/>.
        /// </summary>
        /// <param name="html">The instance of <see cref="HtmlHelper"/>.</param>
        /// <param name="model">The Grid model to be rendered.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, GridDataModel model, string framework = DefaultFramework) {

            // Return an empty string if the model is empty.
            if (model == null) return new HtmlString(String.Empty);

            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, model);

        }

        /// <summary>
        /// Gets the HTML for the Grid model based on the specified <paramref name="framework"/>.
        /// </summary>
        /// <param name="html">The instance of <see cref="HtmlHelper"/>.</param>
        /// <param name="property">The property holding the Grid model.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, IPublishedProperty property, string framework = DefaultFramework) {

            // Get the property value
            GridDataModel value = property.Value() as GridDataModel;
            if (value == null) return new HtmlString(String.Empty);

            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, value);
        
        }

        /// <summary>
        /// Gets the HTML for the Grid model based on default options.
        /// </summary>
        /// <param name="html">The instance of <see cref="HtmlHelper"/>.</param>
        /// <param name="content">The parent content item.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, IPublishedContent content) {
            return html.GetTypedGridHtml(content, DefaultPropertyAlias, DefaultFramework);
        }

        /// <summary>
        /// Gets the HTML for the Grid model based on default options.
        /// </summary>
        /// <param name="html">The instance of <see cref="HtmlHelper"/>.</param>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, IPublishedContent content, string propertyAlias) {
            return html.GetTypedGridHtml(content, propertyAlias, DefaultFramework);
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="html">The instance of <see cref="HtmlHelper"/>.</param>
        /// <param name="content">The parent content item.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this HtmlHelper html, IPublishedContent content, string propertyAlias, string framework) {

            // Get the property with the specifeid alias
            IPublishedProperty property = content.GetProperty(propertyAlias);
            if (property == null) throw new NullReferenceException("No property type found with alias " + propertyAlias);

            // Get the property value
            GridDataModel value = property.Value() as GridDataModel;
            if (value == null) return new HtmlString(String.Empty);
            
            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, value);
        
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="property">The property holding the Grid model.</param>
        /// <param name="html">The instance of <see cref="HtmlHelper"/>.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this IPublishedProperty property, HtmlHelper html, string framework = DefaultFramework) {

            // Get the property value
            GridDataModel value = property.Value() as GridDataModel;
            if (value == null) return new HtmlString(String.Empty);

            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, value);
        
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="html">The instance of <see cref="HtmlHelper"/>.</param>
        public static HtmlString GetTypedGridHtml(this IPublishedContent content, HtmlHelper html) {
            return GetTypedGridHtml(content, html, DefaultPropertyAlias, DefaultFramework);
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="html">The instance of <see cref="HtmlHelper"/>.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static HtmlString GetTypedGridHtml(this IPublishedContent content, HtmlHelper html, string propertyAlias) {
            return GetTypedGridHtml(content, html, propertyAlias, DefaultFramework);
        }

        /// <summary>
        /// Gets the HTML for the Grid model.
        /// </summary>
        /// <param name="content">The parent content item.</param>
        /// <param name="html">The instance of <see cref="HtmlHelper"/>.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        /// <param name="framework">The framework used to render the Grid.</param>
        public static HtmlString GetTypedGridHtml(this IPublishedContent content, HtmlHelper html, string propertyAlias, string framework) {
            
            // Get the property with the specifeid alias
            IPublishedProperty property = content.GetProperty(propertyAlias);
            if (property == null) throw new NullReferenceException("No property type found with alias " + propertyAlias);

            // Get the property value
            GridDataModel value = property.Value() as GridDataModel;
            if (value == null) return new HtmlString(String.Empty);

            // Load the partial view based on the specified framework
            return html.Partial("TypedGrid/" + framework, value);
        
        }

        #endregion

    }

}