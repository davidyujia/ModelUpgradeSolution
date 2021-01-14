using System;

namespace ModelUpgradeSolution
{
    public interface IVersionModel
    {
        string GetId();
        string GetModel();
    }

    public abstract class BaseVersionModel : IVersionModel
    {
        public abstract string GetId();

        public string GetModel()
        {
            return GetType().Name;
        }
    }

    public sealed class Version1 : BaseVersionModel
    {
        public override string GetId()
        {
            return Id;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }

    public sealed class Version2 : BaseVersionModel
    {
        public override string GetId()
        {
            return Uid;
        }

        public string Uid { get; set; }
        public string UserName { get; set; }
        public string Age { get; set; }
    }

    public sealed class YourDataModel
    {
        public string Id { get; set; }

        public string Data { get; set; }

        public string ModelName { get; set; }
    }
}
