using System;

namespace Octopus.Configuration
{
    public interface IKeyValueStore
    {
        string? Get(string name, ProtectionLevel protectionLevel = ProtectionLevel.None);

        TData Get<TData>(string name, TData defaultValue = default, ProtectionLevel protectionLevel = ProtectionLevel.None);
    }
}