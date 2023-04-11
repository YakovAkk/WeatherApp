using System;
using System.Net;
using WeatherApp.Model.InputModel;
using WeatherApp.Services.Base;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class LoginPage : ContentPage
    {
        private readonly IUserService _userService;
        public LoginPage()
        {
            InitializeComponent();
            _userService = DependencyService.Get<IUserService>();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var name = UserName.Text;
            var password = Password.Text;

            var userInputModel = new UserLoginInputModel() { Name = name, Password = password };

            var data = await _userService.Login(userInputModel);

            if(data.StatusCode != HttpStatusCode.OK)
            {
                await DisplayAlert("Error", data.ErrorMessage, "Ok");
                ClearAllFields();
                return;
            }

            ClearAllFields();

            await DisplayAlert("Success", "You are welcome", "Ok");

            await Navigation.PushAsync(new MainPage());
        }

        
        private void ClearAllFields()
        {
            UserName.Text = string.Empty;
            Password.Text = string.Empty;
        }

        private async void NavigationToRegistrationPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}