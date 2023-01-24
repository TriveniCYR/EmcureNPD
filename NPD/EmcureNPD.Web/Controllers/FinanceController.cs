using Microsoft.AspNetCore.Mvc;

namespace EmcureNPD.Web.Controllers
{
	public class FinanceController : Controller
	{
		public IActionResult PIDFFinance()
		{
			return View();
		}
	}
}
