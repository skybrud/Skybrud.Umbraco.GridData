# Skybrud Grid Data [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md) [![NuGet](https://img.shields.io/nuget/vpre/Skybrud.Umbraco.GridData.svg)](https://www.nuget.org/packages/Skybrud.Umbraco.GridData) [![NuGet](https://img.shields.io/nuget/dt/Skybrud.Umbraco.GridData.svg)](https://www.nuget.org/packages/Skybrud.Umbraco.GridData) [![Umbraco Marketplace](https://img.shields.io/badge/umbraco-marketplace-%233544B1)](https://marketplace.umbraco.com/package/skybrud.umbraco.griddata)

**Skybrud.Umbraco.GridData** is a package with a strongly typed model for the grid in Umbraco. The package makes it easy to work the grid in your MVC views, master pages or even in your custom logic - eg. to index the grid data in Examine for better searches.

Version 5 of this package specifically targets Umbraco 10 and above, but past major versions also support older versions of Umbraco. For the Umbraco 9 package, see <a href="https://github.com/skybrud/Skybrud.Umbraco.GridData/tree/v4/main"><code><strong>v4/main</strong></code></a> branch. For the Umbraco 8 package, see <a href="https://github.com/skybrud/Skybrud.Umbraco.GridData/tree/v3/main"><code><strong>v3/main</strong></code></a> branch.





<br /><br />

## Links

- <a href="#installation">Installation</a>
- <a href="#examples">Examples</a>
- <a href="#indexing-with-examine">Indexing with Examine</a>
- <a href="#rendering-the-grid">Rendering the grid</a>
- <a href="#extending-the-grid">Extending the grid</a>




<br /><br />
## Installation

The Umbraco 10+ version of this package is only available via <a href="https://www.nuget.org/packages/Skybrud.Umbraco.GridData" target="_blank">NuGet</a>. To install the package, you can use either .NET CLI:

```
dotnet add package Skybrud.Umbraco.GridData --version 5.0.2
```

or the older NuGet Package Manager:

```
Install-Package Skybrud.Umbraco.GridData -Version 5.0.2
```

**Umbraco 9**  
For the Umbraco 9 version of this package, see the [**v4/main**](https://github.com/skybrud/Skybrud.Umbraco.GridData/tree/v4/main) branch instead.

**Umbraco 8**  
For the Umbraco 8 version of this package, see the [**v3/main**](https://github.com/skybrud/Skybrud.Umbraco.GridData/tree/v3/main) branch instead.





<br /><br />

## Examples

The package has its own property value converter, so you can simply get the grid model as:

```C#
GridDataModel grid = Model.Content.GetPropertyValue<GridDataModel>("content");
```

If you have the raw JSON string, you can parse it like:

```C#
GridDataModel grid = GridDataModel.Deserialize(json);
```

But you can also just call an extension method to get the grid model:

```C#
GridDataModel grid = Model.Content.GetGridModel("content");
```

The benefit of the extension method is that it will always return an instance of `GridDataModel` - even if the property doesn't exists or doesn't have a value, so you don't have to check whether the returned value is `null`. However if you need it, you can use the `IsValid` property to validate that the model is valid (eg. not empty).





<br /><br />

## Indexing with Examine

As of `v2.0`, the `GridDataModel` contains a `GetSearchableText` method that will return a textual representation of the entire grid model - see the example below:

```C#
@using Skybrud.Umbraco.GridData
@using Skybrud.Umbraco.GridData.Extensions
@inherits UmbracoTemplatePage
@{

    GridDataModel grid = Model.Content.GetGridModel("content");

    <pre>@grid.GetSearchableText()</pre>

}
```

The `GetSearchableText` method works by traversing all the controls of the grid, and calling a similar `GetSearchableText` method on each control. The end result will then be a string combined of the returned values from all the controls.

This of course requires that each control (or the model of it's value, really) can provide a textual representation of it's value.

If you need further control of the indexing, you can have a look at this example Gist:

* [Gist: Indexing the Umbraco Grid.md](https://gist.github.com/abjerner/bdd89e0788d274ec5a33)





<br /><br />

## Rendering the grid

The package supports a number of different ways to render the grid. If we start out with the entire grid model, you can do something like (`Fanoe` is the framework/view that should be used for rendering the grid):

```C#
@using Skybrud.Umbraco.GridData
@using Skybrud.Umbraco.GridData.Extensions
@inherits UmbracoTemplatePage
@{

    GridDataModel grid = Model.Content.GetGridModel("content");

    @Html.GetTypedGridHtml(grid, "Fanoe")

}
```

This works by first getting the grid value, and then rendering the model into the current view. This can also be done in a single line instead (`Model.Content` as specified for the first parameter is an instance of `IPublishedContent`):

```C#
@using Skybrud.Umbraco.GridData.Extensions
@inherits UmbracoTemplatePage

@Html.GetTypedGridHtml(Model.Content, "content", "Fanoe")
```

Since both examples specifies the `Fanoe` view, the package will look for a partial view located at `~/Views/Partials/TypedGrid/Fanoe.cshtml` and with an instance of `GridDataModel` as the model. You can find an example of this partial view at the link below:

https://github.com/abjerner/UmbracoGridDataDemo/blob/master/dev/web/Views/Partials/TypedGrid/Fanoe.cshtml

You can also have a look at an example partial view for rendering the individual rows of the grid:

https://github.com/abjerner/UmbracoGridDataDemo/blob/master/dev/web/Views/Partials/TypedGrid/Rows/Default.cshtml





<br /><br />

## Extending the grid

The package will only provide models for the grid editors thats comes with Umbraco by default (as well as the editors from the Fanoe starter kit), but it is also possible to create your own models for custom controls.

This process might however be a bit complex, so I've written an article for [**Skrift.io**](http://skrift.io/) that describes this a bit further:

http://skrift.io/articles/archive/strongly-typed-models-in-the-umbraco-grid/
