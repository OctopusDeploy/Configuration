using System;

namespace Octopus.Configuration
{
    public interface IPersistedKeyValueStore : IModifiableKeyValueStore
    {
        void Save();
    }
}