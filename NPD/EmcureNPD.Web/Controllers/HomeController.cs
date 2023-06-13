using EmcureNPD.Business.Models;
using EmcureNPD.Web.Helpers;
using EmcureNPD.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace EmcureNPD.Web.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IConfiguration _cofiguration;
		private readonly IHelper _helper;
		public HomeController(ILogger<HomeController> logger, IHelper helper, IConfiguration cofiguration)
		{
			_logger = logger;
			_helper = helper;
			_cofiguration = cofiguration;
		}
		public IActionResult Index()
		{

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult AccessRestriction()
		{
			return View();
		}
		public IActionResult InternalServerError()
		{
			return View();
		}
		[HttpGet]
		public IActionResult IsDuplidateLogin()
		{
			APIRepository objapi = new APIRepository(_cofiguration);
			int CurrentUserId = _helper.GetLoggedInUserId();
			dynamic res = null;
			HttpResponseMessage TokenresponseMessage = objapi.APICommunication(APIURLHelper.ValidateToken + "/" + CurrentUserId, HttpMethod.Get, string.Empty).Result;
			if (TokenresponseMessage.IsSuccessStatusCode)
			{
				string jsonTResponse = TokenresponseMessage.Content.ReadAsStringAsync().Result;
				var oDbTokenDetail = JsonConvert.DeserializeObject<APIResponseEntity<UserSessionEntity>>(jsonTResponse);
				res = oDbTokenDetail;

				if (oDbTokenDetail != null && oDbTokenDetail._object.UserId == CurrentUserId  && _helper._isEmptyOrInvalid(_helper.GetToken(), oDbTokenDetail._object.VallidTo))
				{


					HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
					HttpContext.Session.Clear();

					HttpContext.Response.Cookies.Delete(UserHelper.EmcureNPDToken);
					//return Json(res);
				}
			}
			
			return Json(res);
		}
	}
}