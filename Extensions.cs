using System;
using System.Text.Json;
using NewestModel = ModelUpgradeSolution.Version2;

namespace ModelUpgradeSolution
{
    internal static class Extensions
    {
        public static T Deserialize<T>(this string s) where T : IVersionModel
        {
            return JsonSerializer.Deserialize<T>(s);
        }

        public static string Serialize(this IVersionModel model)
        {
            return JsonSerializer.Serialize((object)model);
        }

        /// <summary>
        /// 是否為最新版模型
        /// </summary>
        private static bool IsNewest(this IVersionModel model)
        {
            return model is NewestModel;
        }

        /// <summary>
        /// 升級到最新版事件模型
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static NewestModel UpgradeToNewest(this IVersionModel model)
        {
            while (!model.IsNewest())
            {
                model = model switch
                {
                    Version1 v1 => Upgrade(v1),
                    _ => throw new NotImplementedException("Model upgrade is not implemented")
                };
            }

            return model as NewestModel;
        }

        internal static Version2 Upgrade(Version1 model)
        {
            return new Version2
            {
                Uid = model.Id,
                UserName = model.Name,
                Age = model.Age.ToString()
            };
        }
    }
}
