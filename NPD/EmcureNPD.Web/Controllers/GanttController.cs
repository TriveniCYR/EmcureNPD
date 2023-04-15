using Microsoft.AspNetCore.Mvc;

namespace EmcureNPD.Web.Controllers
{
    public class GanttController : BaseController
    {
        [HttpGet]
        public IActionResult Ganttc()
        {
            return View();
        }
    }
}