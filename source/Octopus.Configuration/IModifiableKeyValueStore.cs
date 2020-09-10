using System;

namespace Octopus.Configuration
{
    public interface IModifiableKeyValueStore : IKeyValueStore
    {
        void Set(string name, string? value, ProtectionLevel protectionLevel = ProtectionLevel.None);
        void Set<TData>(string name, TData value, ProtectionLevel protectionLevel = ProtectionLevel.None);

        void Remove(string name);
    }
}