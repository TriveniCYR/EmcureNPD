using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EmcureCERI.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ErrorController(
           IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this._sharedLocalizer = sharedLocalizer;
        }

        public ActionResult Index()
        {
            ViewBag.Title = _sharedLocalizer["Regular Error"].Value;
            return View();
        }

        public ActionResult NotFound404()
        {
            ViewBag.Title = _sharedLocalizer["Error 404 - File not Found"].Value;
            return View("Index");
        }
    }
}