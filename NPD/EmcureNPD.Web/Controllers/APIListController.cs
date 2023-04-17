using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmcureNPD.Web.Controllers
{
    public class APIListController : BaseController
    {
        #region Properties

        private readonly IConfiguration _cofiguration;

        #endregion Properties

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