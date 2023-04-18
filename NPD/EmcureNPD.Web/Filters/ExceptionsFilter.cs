using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace EmcureNPD.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ExceptionsFilter : ExceptionFilterAttribute,IExceptionFilter, IAsyncExceptionFilter
    {
        //private readonly ILogger<ExceptionsFilter> logger;
       
        public ExceptionsFilter()//ILogger<ExceptionsFilter> logger
        {
            //this.logger = logger;
           
        }

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode = (context.Exception as WebException != null &&
                       ((HttpWebResponse)(context.Exception as WebException).Response) != null) ?
                        ((HttpWebResponse)(context.Exception as WebException).Response).StatusCode
                        : getErrorCode(context.Exception.GetType());

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";

            var responseModel = new APIResponseEntity<string>() { _errorMessages = new List<string> { context.Exception.Message }, _Success = false };

            var result = JsonConvert.SerializeObject(responseModel);

            //logger.LogCritical(context.Exception, context.Exception.Message);
            try
            {
                APIRepository objapi = new APIRepository(APIURLHelper.Configuration);

                context.HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);

                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.LogException, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(context.Exception))).Result;


            }
            catch (Exception e)
            {

            }
            response.ContentLength = result.Length;
            response.WriteAsync(result);
        }
        public void OnExceptionAsync(ExceptionContext context)
        {
            HttpStatusCode statusCode = (context.Exception as WebException != null &&
                       ((HttpWebResponse)(context.Exception as WebException).Response) != null) ?
                        ((HttpWebResponse)(context.Exception as WebException).Response).StatusCode
                        : getErrorCode(context.Exception.GetType());

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";

            var responseModel = new APIResponseEntity<string>() { _errorMessages = new List<string> { context.Exception.Message }, _Success = false };

            var result = JsonConvert.SerializeObject(responseModel);

            //logger.LogCritical(context.Exception, context.Exception.Message);
            try
            {
                APIRepository objapi = new APIRepository(APIURLHelper.Configuration);

                context.HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);

                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.LogException, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(context.Exception))).Result;


            }
            catch (Exception e)
            {

            }
            response.ContentLength = result.Length;
            response.WriteAsync(result);
        }
        private HttpStatusCode getErrorCode(Type exceptionType)
        {
            Exceptions tryParseResult;
            if (Enum.TryParse<Exceptions>(exceptionType.Name, out tryParseResult))
            {
                switch (tryParseResult)
                {
                    case Exceptions.NullReferenceException:
                        return HttpStatusCode.LengthRequired;

                    case Exceptions.FileNotFoundException:
                        return HttpStatusCode.NotFound;

                    case Exceptions.OverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Exceptions.OutOfMemoryException:
                        return HttpStatusCode.ExpectationFailed;

                    case Exceptions.InvalidCastException:
                        return HttpStatusCode.PreconditionFailed;

                    case Exceptions.ObjectDisposedException:
                        return HttpStatusCode.Gone;

                    case Exceptions.UnauthorizedAccessException:
                        return HttpStatusCode.Unauthorized;

                    case Exceptions.NotImplementedException:
                        return HttpStatusCode.NotImplemented;

                    case Exceptions.NotSupportedException:
                        return HttpStatusCode.NotAcceptable;

                    case Exceptions.InvalidOperationException:
                        return HttpStatusCode.MethodNotAllowed;

                    case Exceptions.TimeoutException:
                        return HttpStatusCode.RequestTimeout;

                    case Exceptions.ArgumentException:
                        return HttpStatusCode.BadRequest;

                    case Exceptions.StackOverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Exceptions.FormatException:
                        return HttpStatusCode.UnsupportedMediaType;

                    case Exceptions.IOException:
                        return HttpStatusCode.NotFound;

                    case Exceptions.IndexOutOfRangeException:
                        return HttpStatusCode.ExpectationFailed;

                    default:
                        return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}