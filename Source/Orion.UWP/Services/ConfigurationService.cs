using Windows.Storage;

using Newtonsoft.Json;

using Orion.UWP.Services.Interfaces;

namespace Orion.UWP.Services
{
    internal class ConfigurationService : IConfigurationService
    {
        private readonly ApplicationDataContainer _roamingContainer;

        public ConfigurationService()
        {
            _roamingContainer = ApplicationData.Current.RoamingSettings;
        }

        public T Load<T>(string str)
        {
            return !_roamingContainer.Values.ContainsKey(str) ? default(T) : JsonConvert.DeserializeObject<T>((string) _roamingContainer.Values[str]);
        }

        public void Save<T>(string str, T value)
        {
            _roamingContainer.Values[str] = JsonConvert.SerializeObject(value);
        }
    }
}