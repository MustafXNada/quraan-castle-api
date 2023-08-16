using Microsoft.AspNetCore.Mvc;
using quraan_castle_api.Models_BLL.Responses;
using System.Globalization;

namespace quraan_castle_api
{
    public class ApiResponseModel<T>
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public T data { get; set; }
        public DateTime at { get; set; }

    }
    public class ApiErrorModel
    {
        public string message { get; set; }
        public string code { get; set; }
        public DateTime at { get;set;  }  
    }
    public static class ApiResponses
    {
        public static IActionResult Success<T>(T data)
        {
            return new OkObjectResult(new ApiResponseModel<T> { isSuccess = true, data = data, at = DateTime.UtcNow }) ;
        }

        public static IActionResult Success<T>(T data , string message)
        {
            return new OkObjectResult(new ApiResponseModel<T> { isSuccess = true,message = message, data = data, at = DateTime.UtcNow });
        }

        public static IActionResult Fail(string code , string message)
        {
            return new OkObjectResult(
                new ApiResponseModel<ApiErrorModel>
                {
                    isSuccess = false,
                    data = new ApiErrorModel
                    {
                        code = code,
                        message = message,
                    }
                }
                );
          
        }

    }
}
