using System;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.Umbraco.GridData {
    
    /// <summary>
    /// This project started out as a proof of concept for and my colleague René Pjengaard. While I couldn't think of
    /// a proper name at the time, the name of this class was mostly to annoy René.
    /// 
    /// The class has now been replaced by GridContext, but this class is left behind for good memories. It is however
    /// not recommended to use this class.
    /// </summary>
    public class PiggyBank {
 
        private static readonly PiggyBank NomNom = new PiggyBank();

        public static PiggyBank OneLittlePiggy {
            get { return NomNom; }
        }

        public Func<JToken, IGridControlValue> this[string comeHereLittlePiggy] {
            get { return GridContext.Current[comeHereLittlePiggy]; }
            set { GridContext.Current[comeHereLittlePiggy] = value; }
        }

        private PiggyBank() { }

        public bool TryGetValue(string alias, out Func<JToken, IGridControlValue> func) {
            return GridContext.Current.TryGetValue(alias, out func);
        }

        public static IGridControlValue ConvertMediaValue(JToken token) {
            return GridContext.ConvertMediaValue(token);
        }

        public static IGridControlValue ConvertEmbedValue(JToken token) {
            return GridContext.ConvertEmbedValue(token);
        }

        public static IGridControlValue ConvertRichTextValue(JToken token) {
            return GridContext.ConvertRichTextValue(token);
        }

        public static IGridControlValue ConvertMacroValue(JToken token) {
            return GridContext.ConvertMacroValue(token);
        }

        public static IGridControlValue ConvertTextValue(JToken token) {
            return GridContext.ConvertTextValue(token);
        }
    
    }

}