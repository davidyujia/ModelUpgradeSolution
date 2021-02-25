using System;
using System.Collections.Generic;
using System.Text;

namespace ModelUpgrade.Core
{
    public interface IModelUpgrade
    {
        T Deserialize<T>(string s) where T : IVersionModel;
        string Serialize(IVersionModel model);

        IVersionModel Upgrade<T>(T model) where T : IVersionModel;
    }
}
