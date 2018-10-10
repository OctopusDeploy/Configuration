
namespace Octopus.Configuration
{
    public interface IKeyValueStore
    {
        string Get(string name, bool machineKeyEncrypted = false);
        TData Get<TData>(string name, TData defaultValue = default(TData), bool machineKeyEncrypted = false);
        void Set(string name, string value, bool machineKeyEncrypted = false);
        void Set<TData>(string name, TData value, bool machineKeyEncrypted = false);

        void Remove(string name);

        void Save();
    }
}