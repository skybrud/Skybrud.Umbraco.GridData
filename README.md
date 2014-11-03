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
