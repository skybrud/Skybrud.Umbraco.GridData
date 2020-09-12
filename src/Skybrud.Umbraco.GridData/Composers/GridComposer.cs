using Skybrud.Umbraco.GridData.Converters;
using Skybrud.Umbraco.GridData.Converters.Umbraco;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Skybrud.Umbraco.GridData.Composers {

    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    internal class GridComposer : IUserComposer {

        public void Compose(Composition composition) {

            composition.RegisterUnique<GridContext>();

            composition.GridConverters().Append<UmbracoGridConverter>();

        }

    }

}