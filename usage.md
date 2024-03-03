---
order: 1
---

# Usage

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