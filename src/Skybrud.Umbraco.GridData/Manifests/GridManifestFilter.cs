using System.Collections.Generic;
using Umbraco.Cms.Core.Manifest;

namespace Skybrud.Umbraco.GridData.Manifests {

    /// <inheritdoc />
    public class GridManifestFilter : IManifestFilter {

        /// <inheritdoc />
        public void Filter(List<PackageManifest> manifests) {
            manifests.Add(new PackageManifest {
                AllowPackageTelemetry = true,
                PackageName = GridPackage.Name,
                Version = GridPackage.InformationalVersion
            });
        }

    }

}