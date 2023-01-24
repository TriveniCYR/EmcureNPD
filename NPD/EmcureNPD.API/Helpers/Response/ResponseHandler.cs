using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmcureNPD.API.Helpers.Response
{
    public class ResponseHandler<T> : IResponseHandler<T> where T : class
    {
        public IActionResult Create(T Data, int? StatusCode, string Message = null, string ReturnToUrl = null, List<string> errorMessages = null)
        {
            APIResponseEntity<T> retResult = new APIResponseEntity<T>();
            retResult._object = Data;
            retResult._Message = Message;
            retResult._ReturnToUrl = ReturnToUrl;
            retResult._errorMessages = errorMessages;

            if (StatusCode == 200)
                retResult._Success = true;
            else
                retResult._Success = false;

            return new ObjectResult(retResult) { StatusCode = StatusCode };
        }

        public IActionResult CreateData(T Data, int? StatusCode)
        {
            return new ObjectResult(Data) { StatusCode = StatusCode };
        }
    }
}