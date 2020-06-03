using Skybrud.Umbraco.GridData.Rendering;

namespace Skybrud.Umbraco.GridData {

    /// <summary>
    /// This project started out as a proof of concept for and with my colleague René Pjengaard. While I couldn't think
    /// of a proper name at the time, the name of this class was mostly to annoy René.
    /// 
    /// The class has now been replaced by the <see cref="GridContext"/> class, but this class is left behind for good
    /// memories. It is however not recommended to use this class.
    /// </summary>
    public class PiggyBank {
        
        /// <summary>
        /// Gets one little piggy.
        /// </summary>
        public static PiggyBank OneLittlePiggy { get; } = new PiggyBank();

        private PiggyBank() { }
        
        /// <summary>
        /// Gets an instance of <see cref="GridControlWrapper"/> based on the specified <paramref name="control"/>.
        /// </summary>
        /// <param name="control">The control to wrap.</param>
        public GridControlWrapper GetControlWrapper(GridControl control) {
            return GridContext.Current.GetControlWrapper(control);
        }
    
    }

}