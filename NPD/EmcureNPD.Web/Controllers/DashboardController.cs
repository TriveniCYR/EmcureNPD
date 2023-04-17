using Microsoft.AspNetCore.Mvc;

namespace EmcureNPD.Web.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}