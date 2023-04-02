using DataBase.Entities;
using System;
using System.Security.Cryptography;
using System.Text;
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

            if (!IsValid(name.Trim(), password.Trim(), confirmedPassword.Trim()))
            {
                return;
            }

            var userExist = _userService.GetUserByName(name);

            if (userExist != null)
            {
                await DisplayAlert("Error", "User is already registered!", "Ok");
                ClearAllFields();
                return;
            }

            if (confirmedPassword != password)
            {
                await DisplayAlert("Error", "Both passwords must be equal", "Ok");
                ClearAllFields();
                return;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new UserEntity()
            {
                Name = name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _userService.SaveUser(user);

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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private async void NavigationToLoginPage_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}