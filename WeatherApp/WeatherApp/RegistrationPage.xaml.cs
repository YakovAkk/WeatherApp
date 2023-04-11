using System.Net;
using System.Security.Cryptography;
using System.Text;
using WeatherApp.Model.InputModel;
using WeatherApp.Services.Base;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class RegistrationPage : ContentPage
    {
        private readonly IUserService _userService;
        public RegistrationPage()
        {
            InitializeComponent();
            _userService = DependencyService.Get<IUserService>();
        }

        private async void Register_Clicked(object sender, System.EventArgs e)
        {
            var name = UserName.Text;
            var password = Password.Text;
            var confirmedPassword = ConfirmPassword.Text;

            if (!IsValid(name, password, confirmedPassword))
            {
                return;
            }

            var user = new UserRegistrationInputModel()
            {
                Name = name,
                Password = password,
                ConfirmPassword = confirmedPassword
            };

            var data = await _userService.SaveUser(user);

            if (data.StatusCode != HttpStatusCode.OK)
            {
                await DisplayAlert("Error", data.ErrorMessage, "Ok");
                ClearAllFields();
                return;
            }

            await DisplayAlert("Success", "You can sign in now!", "Ok");
            ClearAllFields();

            await Navigation.PushAsync(new LoginPage());
        }

        private bool IsValid(string name, string password, string confirmedPassword)
        {
            bool isValid = true;
            InvalidUserNameLable.Text = string.Empty;
            InvalidPasswordLable.Text = string.Empty;
            InvalidConfirmPasswordLable.Text = string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                InvalidUserNameLable.Text = "Name can't be empty or white space!";
                isValid = false;
            }
            if (string.IsNullOrEmpty(password))
            {
                InvalidPasswordLable.Text = "Password can't be empty or white space!";
                isValid = false;
            }
            if (string.IsNullOrEmpty(confirmedPassword))
            {
                InvalidConfirmPasswordLable.Text = "Password can't be empty or white space!";
                isValid = false;
            }

            return isValid;
        }

        private void ClearAllFields()
        {
            UserName.Text = string.Empty;
            Password.Text = string.Empty;
            ConfirmPassword.Text = string.Empty;
        }

        private async void NavigationToLoginPage_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}