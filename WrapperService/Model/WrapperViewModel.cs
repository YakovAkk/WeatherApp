using System.Net;

namespace WrapperService.Model
{
    public class WrapperViewModel
    {
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
