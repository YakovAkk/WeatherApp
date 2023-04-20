using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.Services;
using WeatherApp.Services.Base;
using Xamarin.Forms;

[assembly: Dependency(typeof(WeatherService))]
namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly string _apiKey = "a894b274525501189dae3335a81164cc";
        private readonly HttpClient _client;

        public WeatherService()
        {
            _client = new HttpClient();
        }   
        public async Task<WeatherModel> GetWeatherForCity(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";

            try
            {
                string response = await _client.GetStringAsync(url);

                return JsonConvert.DeserializeObject<WeatherModel>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
