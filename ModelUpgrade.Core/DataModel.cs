using System;
using System.Collections.Generic;
using System.Text;

namespace ModelUpgrade.Core
{
    public sealed class DataModel
    {
        public DataModel() { }

        public DataModel(IVersionModel model, Func<IVersionModel, string> func)
        {
            Id = model.GetId();
            Data = func(model);
            ModelName = model.GetModelName();
        }

        public string Id { get; set; }

        public string Data { get; set; }

        public string ModelName { get; set; }
    }
}
