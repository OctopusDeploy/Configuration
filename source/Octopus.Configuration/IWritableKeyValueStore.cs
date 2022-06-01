using System;

namespace Octopus.Configuration
{
    public interface IWritableKeyValueStore : IKeyValueStore
    {
        /// <summary>
        /// Sets a configuration value, if this is supported by the startup strategy
        /// </summary>
        /// <param name="name">The setting's name</param>
        /// <param name="value">The setting's value</param>
        /// <param name="protectionLevel">The protection level of the value, <see cref="ProtectionLevel" /></param>
        /// <returns>True if the value was updated, or false if updates are not supported.</returns>
        bool Set(string name, string? value, ProtectionLevel protectionLevel = ProtectionLevel.None);

        /// <summary>
        /// Sets a configuration value, if this is supported by the startup strategy
        /// </summary>
        /// <param name="name">The setting's name</param>
        /// <param name="value">The setting's value</param>
        /// <param name="protectionLevel">The protection level of the value, <see cref="ProtectionLevel" /></param>
        /// <returns>True if the value was updated, or false if updates are not supported.</returns>
        bool Set<TData>(string name, TData value, ProtectionLevel protectionLevel = ProtectionLevel.None);

        /// <summary>
        /// Removes a configuration value, if this is supported by the startup strategy
        /// </summary>
        /// <param name="name">The setting's name</param>
        /// <returns>True if the value was removed, or false if updates are not supported.</returns>
        bool Remove(string name);

        /// <summary>
        /// Saves the configuration, if this is supported by the startup strategy
        /// </summary>
        /// <returns>True if the configuration was saved, or false if updates are not supported.</returns>
        bool Save();
    }
}