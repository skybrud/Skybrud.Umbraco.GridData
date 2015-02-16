Skybrud.Umbraco.GridData
========================

**Skybrud.Umbraco.GridData** is a small package with a strongly typed model for the new grid in Umbraco 7.2.

The package makes it easy to use the model in your MVC views, master pages or even in your custom logic - eg. to index the grid data in Examine for better searches.

## Download

You can download the package via NuGet:

https://www.nuget.org/packages/Skybrud.Umbraco.GridData

## Examples

The package has its own property value converter, so you can simply get the grid model as:

```C#
GridDataModel grid = Model.Content.GetPropertyValue<GridDataModel>("body");
```

If you have the raw JSON string, you can parse it like:

```C#
GridDataModel grid = GridDataModel.Deserialize(json);
```

### Indexing with Examine

The Gist below gives a quick example on how the Grid can be indexed in Examine:

* [Gist: Indexing the Umbraco Grid.md](https://gist.github.com/abjerner/bdd89e0788d274ec5a33)

#### Property Value Converter

By default in Umbraco, calling `Model.Content.GetPropertyValue("body")` (assuming that `body` is our grid property), an instance of `JObject` will be returned.

After installing the package, the property value converter will make sure that an instance of `GridDataModel` is returned instead.

Unfortunately this will also break the existing logic in Umbraco's `GetGridHtml` extension methods. If you still wish to use `GetGridHtml` in your project, you can disable the property value converter by calling the following line during startup:

```C#
GridPropertyValueConverter.IsEnabled = false;
```
