using System;
using System.Collections.Generic;
using System.Text;

namespace ModelUpgrade.Core
{
    /// <summary>
    /// Model upgrade interface
    /// </summary>
    public interface IModelUpgrade
    {
        /// <summary>
        /// Deserializes the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s">string</param>
        /// <returns></returns>
        T Deserialize<T>(string s) where T : IVersionModel;

        /// <summary>
        /// Serializes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        string Serialize(IVersionModel model);

        /// <summary>
        /// Upgrades the specified model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        IVersionModel Upgrade<T>(T model) where T : IVersionModel;
    }
}
