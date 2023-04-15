using Microsoft.AspNetCore.Mvc;

namespace EmcureNPD.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}