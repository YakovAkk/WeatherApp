using Newtonsoft.Json;
using System;
using System.Net.Http;
using WeatherApp.Model;
using WeatherApp.Services;
using WeatherApp.Services.Base;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        
        private readonly IWeatherService _weatherService;
        public MainPage()
        {
            InitializeComponent();
            _weatherService = DependencyService.Get<IWeatherService>();
        }

        private async void getWeather_Clicked(object sender, EventArgs e)
        {
            string city = userInput.Text.Trim();
            if (city.Length < 2)
            {
                await DisplayAlert("Error", "City's name must be bigger!", "Ok");
                return;
            }

            var weather = await _weatherService.GetWeatherForCity(city);

            if(weather == null)
            {
                await DisplayAlert("Error","Something went wrong", "Ok");
                return;
            }

            Name.Text = $"City name {city}";
            FeelingTemperature.Text = $"Feeling telperatire is {weather.Main.Feels_like} Celsius";
            Temperature.Text = $"The temperature is {weather.Main.Temp} Celsius";
            Pressure.Text = $"The pressure is {weather.Main.Pressure}";
        }
    }
}
