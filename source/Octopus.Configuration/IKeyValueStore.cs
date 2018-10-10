
namespace Octopus.Configuration
{
    public interface IKeyValueStore
    {
        string Get(string name, ProtectionLevel protectionLevel = ProtectionLevel.None);
        TData Get<TData>(string name, TData defaultValue = default(TData), ProtectionLevel protectionLevel  = ProtectionLevel.None);
        
        void Set(string name, string value, ProtectionLevel protectionLevel  = ProtectionLevel.None);
        void Set<TData>(string name, TData value, ProtectionLevel protectionLevel  = ProtectionLevel.None);

        void Remove(string name);

        void Save();
    }
}