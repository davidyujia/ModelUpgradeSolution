using System;
using System.Text.Json;
using ModelUpgrade.Core;

namespace SampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var modelUpgrade = new MyModelUpgrade();
            var converter = new ModelConverter<Version2>(modelUpgrade);

            // Data From Database
            var dbData = new DataModel(new Version1
            {
                Uid = "TestV1",
                Name = "Test1"
            }, modelUpgrade.Serialize);

            var v2 = converter.Parse(dbData);
        }
    }

    class MyModelUpgrade : IModelUpgrade
    {
        public T Deserialize<T>(string s) where T : IVersionModel
        {
            return JsonSerializer.Deserialize<T>(s);
        }

        public string Serialize(IVersionModel model)
        {
            return JsonSerializer.Serialize((object)model);
        }

        public IVersionModel Upgrade<T>(T model) where T : IVersionModel
        {
            return model switch
            {
                Version1 v1 => V1ToV2(v1),
                _ => throw new Exception()
            };
        }

        private static Version2 V1ToV2(Version1 v1)
        {
            return new Version2
            {
                Id = v1.Uid,
                ProjectName = v1.Name
            };
        }
    }

    class Version1 : IVersionModel
    {
        public string GetId()
        {
            return Uid;
        }

        public string GetModelName()
        {
            return GetType().Name;
        }

        public string Uid { get; set; }
        public string Name { get; set; }
    }

    class Version2 : IVersionModel
    {
        public string GetId()
        {
            return Id;
        }

        public string GetModelName()
        {
            return GetType().Name;
        }
        public string Id { get; set; }
        public string ProjectName { get; set; }
    }
}
