# ModelUpgradeSolution

```cs
// You need to create it what inheritance IModelUpgrade
var modelUpgrade = new YourModelUpgrade();

// Create a converter.
var converter = new ModelConverter<YourVersion3Model>(modelUpgrade);

// Sample data, it's from database.
var dbData = new DataModel(new YourVersion1Model
{
    Uid = "TestV1",
    Name = "Test1"
}, modelUpgrade.Serialize);

// Parses your saved data to the v3 model.
var v3 = converter.Parse(dbData);
// Parses v3 model to data model for saving.
var v3DbModel = converter.Parse(v3);
```
