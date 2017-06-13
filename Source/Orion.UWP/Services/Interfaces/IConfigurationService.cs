namespace Orion.UWP.Services.Interfaces
{
    public interface IConfigurationService
    {
        T Load<T>(string str, T defaultValue = default(T));

        void Save<T>(string str, T value);
    }
}