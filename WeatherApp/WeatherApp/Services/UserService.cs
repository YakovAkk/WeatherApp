using DataBase.Database;
using DataBase.Entities;
using System;
using System.IO;
using WeatherApp.Services;
using WeatherApp.Services.Base;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserService))]
namespace WeatherApp.Services
{
    public class UserService : IUserService
    {
        private readonly DB _db;
        public UserService()
        {
            if (_db == null)
                _db = new DB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.sqlite3"));
        }

        public UserEntity GetUserByName(string username)
        {
            return _db.GetUserByName(username);
        }

        public int SaveUser(UserEntity user)
        {
            return _db.SaveUser(user);
        }
    }
}
