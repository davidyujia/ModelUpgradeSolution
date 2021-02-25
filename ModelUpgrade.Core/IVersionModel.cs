using System;
using System.Collections.Generic;
using System.Text;

namespace ModelUpgrade.Core
{
    public interface IVersionModel
    {
        string GetId();
        string GetModelName();
    }
}
