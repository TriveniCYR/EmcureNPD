using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EmcureNPD.Web.Controllers
{
    [CustomAuthorizeAttribute]
    public class BaseController : Controller
    {
    }
}