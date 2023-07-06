using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Net.Http;
namespace EmcureNPD.Web.Controllers
{
    public class WishListController : Controller
    {
		#region Properties

		private readonly IConfiguration _cofiguration;
		private readonly IStringLocalizer<Errors> _stringLocalizerError;
		private readonly IStringLocalizer<Shared> _stringLocalizerShared;
		private readonly IStringLocalizer<Master> _stringLocalizerMaster;
		private readonly IHelper _helper;

		#endregion Properties
		public WishListController(IConfiguration configuration, IStringLocalizer<Master> stringLocalizerMaster,

			IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared, IHelper helper)
		{
			
			_cofiguration = configuration;
			_stringLocalizerError = stringLocalizerError;
			_stringLocalizerShared = stringLocalizerShared;
			_stringLocalizerMaster = stringLocalizerMaster;
			_helper = helper;
		}
		public IActionResult WishListView()
        {
			try
			{
				//HttpResponseMessage responseMessage = new HttpResponseMessage();
				//HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
				//APIRepository objapi = new(_cofiguration);

				//responseMessage = objapi.APICommunication(APIURLHelper.GetAllWishList, HttpMethod.Post, token).Result;

				//if (responseMessage.IsSuccessStatusCode)
				//{
				//	string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
				//	var data = JsonConvert.DeserializeObject<DataTableResponseModel>(jsonResponse);
				//	// return data.Data;
				//	return View(data);
				//}
				//else
				//{
				//	return View();
				//}
				return View();
			}
			catch (Exception ex)
			{
				_helper.LogExceptions(ex);
				throw;
			}
		}
    }
}
