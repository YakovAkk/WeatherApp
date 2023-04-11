using System.Net;

namespace WeatherApp.Model.Response.Base
{
    public class ResponseModelBase
    {
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
