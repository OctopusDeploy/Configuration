using System;

namespace Octopus.Configuration
{
    public interface IKeyValueStore
    {
        string? Get(string name, ProtectionLevel protectionLevel = ProtectionLevel.None);

        // ReSharper disable once RedundantTypeSpecificationInDefaultExpression
        TData Get<TData>(string name, TData defaultValue = default(TData), ProtectionLevel protectionLevel = ProtectionLevel.None);
    }
}