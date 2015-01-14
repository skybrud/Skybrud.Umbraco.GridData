using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Values;

namespace Skybrud.Umbraco.GridData {
    
    public class GridContext {

        #region Private fields

        readonly Dictionary<string, Func<JToken, IGridControlValue>> _converters = new Dictionary<string, Func<JToken, IGridControlValue>>();

        #endregion

        #region Properties

        public static readonly GridContext Current = new GridContext();

        /// <summary>
        /// Gets or sets the converter with the specified <code>alias</code>.
        /// </summary>
        /// <param name="alias">The alias of the converter.</param>
        public Func<JToken, IGridControlValue> this[string alias] {
            get { return _converters[alias]; }
            set { _converters[alias] = value; }
        }

        #endregion

        #region Constructors

        private GridContext() {
            _converters["media"] = ConvertMediaValue;
            _converters["embed"] = ConvertEmbedValue;
            _converters["rte"] = ConvertRichTextValue;
            _converters["macro"] = ConvertMacroValue;
            _converters["quote"] = ConvertTextValue;
            _converters["headline"] = ConvertTextValue;
        }

        #endregion

        #region Member methods

        public bool TryGetValue(string alias, out Func<JToken, IGridControlValue> func) {
            return _converters.TryGetValue(alias, out func);
        }

        #endregion

        #region Static methods

        public static IGridControlValue ConvertMediaValue(JToken token) {

            // At this point the token should be a JObject, but we cast it safely to be sure
            JObject obj = token as JObject;

            // Return the converted media value (or NULL if the object is already NULL)
            return obj == null ? null : obj.ToObject<GridControlMediaValue>();

        }

        public static IGridControlValue ConvertEmbedValue(JToken token) {
            return new GridControlEmbedValue {
                Value = token.Value<string>()
            };
        }

        public static IGridControlValue ConvertRichTextValue(JToken token) {
            return new GridControlRichTextValue {
                Value = token.Value<string>()
            };
        }

        public static IGridControlValue ConvertMacroValue(JToken token) {

            // At this point the token should be a JObject, but we cast it safely to be sure
            JObject obj = token as JObject;

            // Return the converted macro value (or NULL if the object is already NULL)
            return obj == null ? null : obj.ToObject<GridControlMacroValue>();

        }

        public static IGridControlValue ConvertTextValue(JToken token) {
            return new GridControlTextValue {
                Value = token.Value<string>()
            };
        }

        #endregion

    }

}