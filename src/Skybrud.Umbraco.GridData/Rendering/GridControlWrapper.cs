namespace Skybrud.Umbraco.GridData.Rendering {

    public class GridControlWrapper<T> {

        public GridControl Control { get; private set; }

        public GridEditor Editor {
            get { return Control == null ? null : Control.Editor; }
        }

        public T Value { get; private set; }

        public GridControlWrapper(GridControl control, T value) {
            Control = control;
            Value = value;
        }

    }

}