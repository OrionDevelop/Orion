namespace Orion.UWP.Services.Interfaces
{
    public interface IConfigurationService
    {
        T Load<T>(string str);

        void Save<T>(string str, T value);
    }
}