using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model.InputModel;
using WeatherApp.Model.Response.Base;
using WeatherApp.Services;
using WeatherApp.Services.Base;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserService))]
namespace WeatherApp.Services
{
    public class UserService : IUserService
    {
        private const string _url = "https://10.1.10.231:7067/api";

        public async Task<ResponseModelBase> Login(UserLoginInputModel user)
        {
            var json = JsonConvert.SerializeObject(user);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            using (var client = new HttpClient(handler))
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync($"{_url}/Auth/Login", content);
                    var resp = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<ResponseModelBase>(resp);

                    return responseData;
                }
                catch (Exception ex)
                {
                    return new ResponseModelBase() { ErrorMessage = ex.Message, StatusCode = HttpStatusCode.InternalServerError };
                }
            }
        }
        public async Task<ResponseModelBase> SaveUser(UserRegistrationInputModel user)
        {
            var json = JsonConvert.SerializeObject(user);

            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            using (var client = new HttpClient(handler))
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync($"{_url}/Auth/Register", content);
                    var resp = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<ResponseModelBase>(resp);
                    
                    return responseData;
                }
                catch (Exception ex)
                {
                    return new ResponseModelBase() { ErrorMessage = ex.Message, StatusCode = HttpStatusCode.InternalServerError };
                }
            }
        }
    }
}
