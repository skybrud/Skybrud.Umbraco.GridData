using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;
using Skybrud.Umbraco.GridData.Values;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// Singleton class used for configuring the grid.
    /// </summary>
    public class GridContext {

        #region Private fields

        readonly Dictionary<string, Func<JToken, IGridControlValue>> _valueConverters = new Dictionary<string, Func<JToken, IGridControlValue>>();

        readonly Dictionary<string, Func<JToken, IGridControlConfig>> _configConverters = new Dictionary<string, Func<JToken, IGridControlConfig>>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the <code>GridContext</code> class.
        /// </summary>
        public static readonly GridContext Current = new GridContext();

        /// <summary>
        /// Gets a dictionary of all registered value converters.
        /// </summary>
        public Dictionary<string, Func<JToken, IGridControlValue>> ValueConverters {
            get { return _valueConverters; }
        }

        /// <summary>
        /// Gets a dictionary of all registered config converters.
        /// </summary>
        public Dictionary<string, Func<JToken, IGridControlConfig>> ConfigConverters {
            get { return _configConverters; }
        }

        /// <summary>
        /// Gets or sets the converter with the specified <code>alias</code>.
        /// </summary>
        /// <param name="alias">The alias of the converter.</param>
        [Obsolete("Use the ValueConverters property instead.")]
        public Func<JToken, IGridControlValue> this[string alias] {
            get { return _valueConverters[alias]; }
            set { _valueConverters[alias] = value; }
        }

        #endregion

        #region Constructors

        private GridContext() {
            _valueConverters["media"] = ConvertMediaValue;
            _valueConverters["embed"] = ConvertEmbedValue;
            _valueConverters["rte"] = ConvertRichTextValue;
            _valueConverters["macro"] = ConvertMacroValue;
            _valueConverters["quote"] = ConvertTextValue;
            _valueConverters["headline"] = ConvertTextValue;
            _valueConverters["textstring"] = ConvertTextValue;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets an instance of <code>GridControlWrapper</code> based on the specified <code>control</code> and <code>value</code>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="control">The control to wrap.</param>
        /// <param name="value">The value to wrap.</param>
        public static GridControlWrapper<T> GetControlWrapper<T>(GridControl control, T value) {
            return new GridControlWrapper<T>(control, value);
        }

        /// <summary>
        /// Attemps to get a value converter for the specified <code>alias</code>.
        /// </summary>
        /// <param name="alias">The alias of the converter.</param>
        /// <param name="func"></param>
        [Obsolete("Use TryGetValueConverter")]
        public bool TryGetValue(string alias, out Func<JToken, IGridControlValue> func) {
            return _valueConverters.TryGetValue(alias, out func);
        }

        /// <summary>
        /// Attemps to get a value converter for the specified <code>alias</code>.
        /// </summary>
        /// <param name="alias">The alias of the converter.</param>
        /// <param name="func"></param>
        public bool TryGetValueConverter(string alias, out Func<JToken, IGridControlValue> func) {
            return _valueConverters.TryGetValue(alias, out func);
        }

        /// <summary>
        /// Attemps to get a config converter for the specified <code>alias</code>.
        /// </summary>
        /// <param name="alias">The alias of the converter.</param>
        /// <param name="func"></param>
        public bool TryGetConfigConverter(string alias, out Func<JToken, IGridControlConfig> func) {
            return _configConverters.TryGetValue(alias, out func);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Default converter for a media value.
        /// </summary>
        /// <param name="token">The instance of <code>JToken</code> representing the value.</param>
        public static IGridControlValue ConvertMediaValue(JToken token) {

            // At this point the token should be a JObject, but we cast it safely to be sure
            JObject obj = token as JObject;

            // Return the converted media value (or NULL if the object is already NULL)
            return obj == null ? null : obj.ToObject<GridControlMediaValue>();

        }

        /// <summary>
        /// Default converter for an embed value.
        /// </summary>
        /// <param name="token">The instance of <code>JToken</code> representing the value.</param>
        public static IGridControlValue ConvertEmbedValue(JToken token) {
            return new GridControlEmbedValue {
                Value = token.Value<string>()
            };
        }

        /// <summary>
        /// Default converter for a rich-text value.
        /// </summary>
        /// <param name="token">The instance of <code>JToken</code> representing the value.</param>
        public static IGridControlValue ConvertRichTextValue(JToken token) {
            return new GridControlRichTextValue {
                Value = token.Value<string>()
            };
        }

        /// <summary>
        /// Default converter for a macro value.
        /// </summary>
        /// <param name="token">The instance of <code>JToken</code> representing the value.</param>
        public static IGridControlValue ConvertMacroValue(JToken token) {

            // At this point the token should be a JObject, but we cast it safely to be sure
            JObject obj = token as JObject;

            // Return the converted macro value (or NULL if the object is already NULL)
            return obj == null ? null : obj.ToObject<GridControlMacroValue>();

        }

        /// <summary>
        /// Default converter for a text value.
        /// </summary>
        /// <param name="token">The instance of <code>JToken</code> representing the value.</param>
        public static IGridControlValue ConvertTextValue(JToken token) {
            return new GridControlTextValue {
                Value = token.Value<string>()
            };
        }

        #endregion

    }

}