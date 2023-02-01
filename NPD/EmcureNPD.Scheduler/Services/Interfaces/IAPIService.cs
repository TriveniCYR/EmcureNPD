using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Scheduler.Services.Interfaces
{
    public interface IAPIService
    {
        Task<HttpResponseMessage> APICommunication(string URL, HttpMethod invokeType, string token, HttpContent body = null, string ContentType = "application/json", List<IFormFile> formFile = null, string bodyKey = null);
    }
}
