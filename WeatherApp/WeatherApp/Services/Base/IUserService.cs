using System.Threading.Tasks;
using WeatherApp.Model.InputModel;
using WeatherApp.Model.Response;
using WeatherApp.Model.Response.Base;

namespace WeatherApp.Services.Base
{
    public interface IUserService
    {
        Task<ResponseModelBase> Login(UserLoginInputModel username);
        Task<ResponseModelBase> SaveUser(UserRegistrationInputModel user);
    }
}
