using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
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
       
        public ExceptionsFilter()
        {
           
        }

        public override void OnException(ExceptionContext context)
        {
            var routeData = new RouteValueDictionary();
            HttpStatusCode statusCode = (context.Exception as WebException != null &&
                       ((HttpWebResponse)(context.Exception as WebException).Response) != null) ?
                        ((HttpWebResponse)(context.Exception as WebException).Response).StatusCode
                        : getErrorCode(context.Exception.GetType());

            var responseModel = new APIResponseEntity<string>() { _errorMessages = new List<string> { context.Exception.Message }, _Success = false };
            var result = JsonConvert.SerializeObject(responseModel);
            try
            {
                APIRepository objapi = new APIRepository(APIURLHelper.Configuration);
                context.HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.LogException, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(context.Exception))).Result;
            }
            catch (Exception e)
            {

            }
           
            if (statusCode == HttpStatusCode.InternalServerError)
            {
                routeData = new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "InternalServerError"
                });
            }
            context.ExceptionHandled = true;
            context.Result = new RedirectToRouteResult(routeData);
            context.Result.ExecuteResultAsync(context);
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