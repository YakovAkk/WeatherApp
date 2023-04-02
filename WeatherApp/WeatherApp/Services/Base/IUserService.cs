using DataBase.Entities;

namespace WeatherApp.Services.Base
{
    public interface IUserService
    {
        UserEntity GetUserByName(string username);
        int SaveUser(UserEntity user);
    }
}
