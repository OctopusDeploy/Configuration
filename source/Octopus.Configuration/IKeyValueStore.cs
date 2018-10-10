
namespace Octopus.Configuration
{
    public interface IKeyValueStore
    {
        string Get(string name, ProtectionLevel machineKeyEncrypted = ProtectionLevel.None);
        TData Get<TData>(string name, TData defaultValue = default(TData), ProtectionLevel machineKeyEncrypted  = ProtectionLevel.None);
        
        void Set(string name, string value, ProtectionLevel machineKeyEncrypted  = ProtectionLevel.None);
        void Set<TData>(string name, TData value, ProtectionLevel machineKeyEncrypted  = ProtectionLevel.None);

        void Remove(string name);

        void Save();
    }
}