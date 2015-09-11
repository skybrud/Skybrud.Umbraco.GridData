using Skybrud.Umbraco.GridData.Rendering;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// This project started out as a proof of concept for and with my colleague René Pjengaard. While I couldn't think
    /// of a proper name at the time, the name of this class was mostly to annoy René.
    /// 
    /// The class has now been replaced by GridContext, but this class is left behind for good memories. It is however
    /// not recommended to use this class.
    /// </summary>
    public class PiggyBank {

        private static readonly PiggyBank NomNom = new PiggyBank();

        /// <summary>
        /// Gets one little piggy.
        /// </summary>
        public static PiggyBank OneLittlePiggy {
            get { return NomNom; }
        }

        private PiggyBank() { }
        
        /// <summary>
        /// Gets an instance of <code>GridControlWrapper</code> based on the specified <code>control</code>.
        /// </summary>
        /// <param name="control">The control to wrap.</param>
        public GridControlWrapper GetControlWrapper(GridControl control) {
            return GridContext.Current.GetControlWrapper(control);
        }
    
    }

}