using System.Net.Http.Headers;
using System.Net;
using EmcureNPD.Scheduler.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EmcureNPD.Scheduler.Services.Implementations
{
    public class APIService : IAPIService
    {
        public static string baseURL;
        private IConfiguration Configuration;

        public APIService(IConfiguration configuration)
        {
            Configuration = configuration;
            baseURL = Configuration.GetSection("API").GetSection("EmcureNPD.API").Value;
        }

        #region APICommunication - Common Method for API calling

        public async Task<HttpResponseMessage> APICommunication(string URL, HttpMethod invokeType, string token, HttpContent body = null, string ContentType = "application/json", List<IFormFile> formFile = null, string bodyKey = null)
        {
            HttpResponseMessage oHttpResponseMessage = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    if (!string.IsNullOrEmpty(token))
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    if (invokeType.Method == HttpMethod.Get.ToString())
                    {
                        return await client.GetAsync(URL);
                    }
                    else if (invokeType.Method == HttpMethod.Post.ToString())
                    {
                        if (formFile != null)
                        {
                            // file upload logic will go here
                        }

                        if (body != null)
                            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        return await client.PostAsync(URL, body);
                    }
                    else if (invokeType.Method == HttpMethod.Put.ToString())
                    {
                        if (body != null)
                            body.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        return await client.PostAsync(URL, body);
                    }
                    else if (invokeType.Method == HttpMethod.Delete.ToString())
                    {
                        return await client.DeleteAsync(URL);
                    }
                }
            }
            catch (Exception ex)
            {
                oHttpResponseMessage.StatusCode = HttpStatusCode.InternalServerError;
            }
            return oHttpResponseMessage;
        }

        #endregion APICommunication - Common Method for API calling
    }
}
