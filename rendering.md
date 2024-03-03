---
order: 2
---

# Rendering the grid

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