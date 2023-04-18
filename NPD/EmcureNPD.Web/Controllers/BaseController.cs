using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using EmcureNPD.Web.Filters;

namespace EmcureNPD.Web.Controllers
{
    [CustomAuthorizeAttribute]
    [ExceptionsFilter]
    public class BaseController : Controller
    {
    }
}