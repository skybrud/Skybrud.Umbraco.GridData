using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Umbraco.GridData.Converters.Umbraco;
using Skybrud.Umbraco.GridData.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Skybrud.Umbraco.GridData.Converters {


    /// <summary>
    /// Collection of <see cref="IGridConverter"/>.
    /// </summary>
    public class GridConverterCollection : BuilderCollectionBase<IGridConverter> {

        /// <summary>
        /// Gets the current converter collection.
        /// </summary>
        public static GridConverterCollection Current => global::Umbraco.Core.Composing.Current.Factory.GetInstance<GridConverterCollection>();

        /// <summary>
        /// Initializes a new converter collection based on the specified <paramref name="items"/>.
        /// </summary>
        /// <param name="items">The items to make up the collection.</param>
        public GridConverterCollection(IEnumerable<IGridConverter> items) : base(items) { }

    }


    

    public class GridConverterCollectionBuilder : OrderedCollectionBuilderBase<GridConverterCollectionBuilder, GridConverterCollection, IGridConverter> {
        
        /// <inheritdoc />
        protected override GridConverterCollectionBuilder This => this;

    }
    
    /// <summary>
    /// Provides extension methods to the <see cref="Composition"/> class.
    /// </summary>
    public static class GridCompositionExtensions {

        /// <summary>
        /// Gets the video picker provider collection builder.
        /// </summary>
        /// <param name="composition">The composition.</param>
        public static GridConverterCollectionBuilder GridConverters(this Composition composition) {
            return composition.WithCollectionBuilder<GridConverterCollectionBuilder>();
        }

    }


















    /// <summary>
    /// Collection of <see cref="IGridConverter"/>.
    /// </summary>
    public class GridConverterCollectionOld : IEnumerable<IGridConverter> {

        #region Private fields

        private readonly List<IGridConverter> _converters = new List<IGridConverter> {
            new UmbracoGridConverter()
        };
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the amount of converters added to the collection.
        /// </summary>
        public int Count => _converters.Count;

        #endregion

        #region Member methods

        /// <summary>
        /// Adds the specified <paramref name="converter"/> to the collection.
        /// </summary>
        /// <param name="converter">The converter to be added.</param>
        public void Add(IGridConverter converter) {
            _converters.Add(converter);
        }

        /// <summary>
        /// Adds the specified <paramref name="converter"/> to the collection at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="converter">The converter to be added.</param>
        public void AddAt(int index, IGridConverter converter) {
            _converters.Insert(index, converter);
        }

        /// <summary>
        /// Clears the collection.
        /// </summary>
        public void Clear() {
            _converters.Clear();
        }

        /// <summary>
        /// Removes the specified <paramref name="converter"/> from the collection.
        /// </summary>
        /// <param name="converter">The converter to be removed.</param>
        public void Remove(IGridConverter converter) {
            _converters.Remove(converter);
        }

        /// <summary>
        /// Removes all converters of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the converters to be removed.</typeparam>
        public void Remove<T>() where T : IGridConverter {
            foreach (T converter in _converters.ToArray().OfType<T>()) {
                _converters.Remove(converter);
            }
        }

        /// <summary>
        /// Removes the converter at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the converter to be removed.</param>
        public void RemoveAt(int index) {
            _converters.RemoveAt(index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="List{IGridConverter}"/>.
        /// </summary>
        /// <returns>A <see cref="List{T}.Enumerator"/> for the <see cref="List{IGridConverter}"/>.</returns>
        public IEnumerator<IGridConverter> GetEnumerator() {
            return _converters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        #endregion

    }

}