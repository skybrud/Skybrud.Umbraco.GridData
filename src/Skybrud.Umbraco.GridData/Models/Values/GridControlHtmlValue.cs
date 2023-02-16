using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Skybrud.Umbraco.GridData.Json.Converters;

namespace Skybrud.Umbraco.GridData.Models.Values {

    /// <summary>
    /// Class representing the HTML value of a control.
    /// </summary>
    [JsonConverter(typeof(GridControlValueStringConverter))]
    public class GridControlHtmlValue : GridControlTextValue {

        #region Properties

        /// <summary>
        /// Gets an instance of <see cref="IHtmlContent"/> representing the text value.
        /// </summary>
        [JsonIgnore]
        public IHtmlContent HtmlValue { get; }

        /// <summary>
        /// Gets whether the value is valid. For an instance of <see cref="GridControlHtmlValue"/>, this means
        /// checking whether the specified text is not an empty string.
        /// </summary>
        [JsonIgnore]
        public override bool IsValid => !string.IsNullOrWhiteSpace(Regex.Replace(Value, "<(p|/p)>", string.Empty));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the value of the specified grid <paramref name="control"/>.
        /// </summary>
        /// <param name="control">An instance of <see cref="GridControl"/> representing the parent grid control.</param>
        public GridControlHtmlValue(GridControl control) : base(control) {
            HtmlValue = new HtmlString(Value);
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Writes a string representation of this value to <paramref name="writer"/>.
        /// </summary>
        /// <param name="context">The current grid context.</param>
        /// <param name="writer">The writer.</param>
        public override void WriteSearchableText(GridContext context, TextWriter writer) {
            writer.WriteLine(Regex.Replace(Value, "<.*?>", " "));
        }

        #endregion

    }

}