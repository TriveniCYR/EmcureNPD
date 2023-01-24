using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmcureNPD.API.Helpers.Response
{
    public interface IResponseHandler<T> where T : class
    {
        IActionResult Create(T Data, int? StatusCode, string Message = null, string ReturnToUrl = null, List<string> errorMessages = null);

        IActionResult CreateData(T Data, int? StatusCode);
    }
}