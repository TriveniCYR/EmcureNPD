using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
    [CustomAuthorizeAttribute]
    public class BaseController : Controller
    {

    }
}