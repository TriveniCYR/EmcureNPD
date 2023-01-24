using EmcureNPD.Business.Models;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
    public class APIListController : BaseController
    {
        #region Properties
        private readonly IConfiguration _cofiguration;
        #endregion

        public APIListController(IConfiguration configuration)
        {
            _cofiguration = configuration;
        }
        public IActionResult APIList()
        {
            return View();
        }
    }
}
