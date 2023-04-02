using Newtonsoft.Json;
using System;
using System.Net.Http;
using WeatherApp.Model;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        private readonly string _apiKey = "a894b274525501189dae3335a81164cc";
        public MainPage()
        {
            InitializeComponent();
        }

        private async void getWeather_Clicked(object sender, EventArgs e)
        {
            string city = userInput.Text.Trim();
            if (city.Length < 2)
            {
                await DisplayAlert("Error", "City's name must be bigger!", "Ok");
                return;
            }
            HttpClient client = new HttpClient();

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";

            try
            {
                string response = await client.GetStringAsync(url);

                var weather = JsonConvert.DeserializeObject<WeatherModel>(response);

                Name.Text = $"City name {city}";
                FeelingTemperature.Text = $"Feeling telperatire is {weather.Main.Feels_like} Celsius";
                Temperature.Text = $"The temperature is {weather.Main.Temp} Celsius";
                Pressure.Text = $"The pressure is {weather.Main.Pressure}";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
