using System;
using System.Collections.Generic;
using NewestModel = ModelUpgradeSolution.Version2;
using DataModel = ModelUpgradeSolution.YourDataModel;

namespace ModelUpgradeSolution
{
    public class ConvertService
    {
        public DataModel Parse(IVersionModel model)
        {
            if (model == null)
            {
                return null;
            }

            model = model.UpgradeToNewest();

            return new DataModel
            {
                Id = model.GetId(),
                Data = model.Serialize(),
                ModelName = model.GetModel()
            };
        }

        public NewestModel Parse(DataModel model)
        {
            var targetVersion = typeof(NewestModel).Name;

            while (model.ModelName != targetVersion)
            {
                if (_upgradeFunc.Value.ContainsKey(model.ModelName))
                {
                    model = _upgradeFunc.Value[model.ModelName](model);
                }
                else
                {
                    throw new NotImplementedException("Model upgrade is not implemented");
                }
            }

            var obj = model.Data.Deserialize<NewestModel>();
            
            return obj;
        }

        private static DataModel Upgrade<TOld, TNew>(DataModel model, Func<TOld, TNew> upgrade)
            where TOld : IVersionModel
            where TNew : IVersionModel
        {
            var oldVersion = model.Data.Deserialize<TOld>();

            if (oldVersion == null)
            {
                return null;
            }

            var newVersion = upgrade(oldVersion);

            model.Data = newVersion.Serialize();

            model.ModelName = typeof(TNew).Name;

            return model;
        }
        
        private readonly Lazy<Dictionary<string, Func<DataModel, DataModel>>> _upgradeFunc = new(() => new Dictionary<string, Func<DataModel, DataModel>>
        {
            {nameof(Version1), model=> Upgrade<Version1, Version2>(model, Extensions.Upgrade)}
        });
    }
}
