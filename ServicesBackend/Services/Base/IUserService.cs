using ServicesBackend.Model.InputModel;
namespace ServicesBackend.Services.Base
{
    public interface IUserService
    {
        Task<string> RegisterAsync(UserRegisterInputModel user);
        Task<bool> LoginAsync(UserLoginInputModel user);
    }
}
