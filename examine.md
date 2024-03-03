---
order: 3
---

# Indexing with Examine

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