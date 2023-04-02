using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

            if (!IsValid(name, password))
            {
                return;
            }

            if (!IsValid(name.Trim(), password.Trim()))
            {
                return;
            }

            var user = _userService.GetUserByName(name);

            if(user == null)
            {
                await DisplayAlert("Error", "User doesn't exist!", "Ok");
                ClearAllFields();
                return;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                await DisplayAlert("Error", "Password is incorrect!", "Ok");
                ClearAllFields();
                return;
            }

            ClearAllFields();

            await DisplayAlert("Success", "You are welcome", "Ok");

            await Navigation.PushAsync(new MainPage());
        }

        private bool IsValid(string name, string password)
        {
            bool isValid = true;
            InvalidUserNameLable.Text = string.Empty;
            InvalidPasswordLable.Text = string.Empty;
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

            return isValid;
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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}