using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.Services.Base
{
    public interface IWeatherService
    {
        Task<WeatherModel> GetWeatherForCity(string city);
    }
}
