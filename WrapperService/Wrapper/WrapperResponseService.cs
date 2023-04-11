using System.Net;
using WrapperService.Model;

namespace WrapperService.Wrapper
{
    public static class WrapperResponseService
    {
        public static WrapperViewModel Wrap<T>(T inputModel = null, string errorMessage = null) where T : class
        {
            if (errorMessage is not null)
            {
                return new WrapperViewModel()
                {
                    Data = null,
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessage = errorMessage
                };
            }

            return new WrapperViewModel()
            {
                Data = inputModel,
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = ""
            };
        }
    }
}
