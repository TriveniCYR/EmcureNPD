using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmcureCERI.Web.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Models.PrescriberViewModels;
using Microsoft.AspNetCore.Authorization;
using EmcureCERI.Business.Models;
using EmcureCERI.Business.Core.ServiceImplementations;
using Newtonsoft.Json;
using EmcureCERI.Web.Helper;
using Microsoft.Extensions.Configuration;
using EmcureCERI.Business.Contract;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace EmcureCERI.Web.Controllers
{
    public class HomeController : Controller
    {
        private EmcureCERIDBContext db = new EmcureCERIDBContext();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmcureCERIDBContext _context;
        private readonly IPrescriberService _prescriber;
        private readonly IPatientService _patient;
        private readonly IConfiguration _configuration;
        private readonly IMessageService _messageService;
        private readonly IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment, IMessageService messageService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IPrescriberService prescriber, IPatientService patient)
        {
            this._messageService = messageService;
            this._configuration = configuration;
            this._httpContextAccessor = httpContextAccessor;
            this._prescriber = prescriber;
            this._patient = patient;
            this._environment = environment;
            _context = new EmcureCERIDBContext();
        }

        public IActionResult Index()
        { 
            if (User.Identity.IsAuthenticated)
            {
                //if (User.IsInRole("Admin"))
                //{
                //    return RedirectToAction("Prescriber", "Admin");
                //}
                //else
                //{
                //    if (User.IsInRole("Prescriber"))
                //    {
                //        int userId = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                //        if (!_context.AspNetUsers.Where(o => o.UserId == userId).Select(o => o.IsChangePassword).FirstOrDefault())
                //        {
                //            return RedirectToAction("ChangePassword", "Manage");
                //        }
                //        return RedirectToAction("Index", "Prescriber");
                //    }
                //    else
                //    {
                //        return View();
                //    }
                //}

                return RedirectToAction("Index", "Dashboard");
                //return RedirectToAction("GetDashboardData", "Dashboard");
                //string fromdate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                //string todate = DateTime.Now.ToString("yyyy-MM-dd");
                //DashboardRequestVM objdashboardRequestVM = new DashboardRequestVM();
                //objdashboardRequestVM.fromDate = fromdate;
                //objdashboardRequestVM.toDate = todate;
                //return RedirectToAction("GetDashboardData1", "Dashboard",new { dashboardRequestVM = objdashboardRequestVM });
            }
            else
            {
                return RedirectToAction("Login", "Account");

               
            }
            //return View();

        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactVM model)
        {
            if (ModelState.IsValid)
            {
                string body = _messageService.ContactUs(model.Name, model.Email, model.Query);
                string contactTo = _configuration["ContactTo"].ToString();
                EmailHelper email = new EmailHelper();
                email.SendMail(contactTo, "", "Contact Us", body);
            }
            return RedirectToAction("ContactUs", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult PrivacyPolicyforPatients()
        {
            return View();
        }
        public IActionResult PrivacyPolicyforGuardians()
        {
            return View();
        }
        public IActionResult PrivacyPolicyforPrescribes()
        {
            return View();
        }
        public IActionResult PrivacyPolicyforAdverseEvents()
        {
            return View();
        }
        public IActionResult PrivacyPolicyforQueries()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult GetSPC()
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (cultureName)
            {
                case "nl-BE":
                    var path1 = Path.Combine(_environment.WebRootPath, "doc/Dutch");
                    byte[] fileBytes1 = System.IO.File.ReadAllBytes(@path1 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName1 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes1, System.Net.Mime.MediaTypeNames.Application.Octet, fileName1);
                case "de-DE":
                    var path2 = Path.Combine(_environment.WebRootPath, "doc/German");
                    byte[] fileBytes2 = System.IO.File.ReadAllBytes(@path2 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName2 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes2, System.Net.Mime.MediaTypeNames.Application.Octet, fileName2);
                case "en-GB":
                    var path3 = Path.Combine(_environment.WebRootPath, "doc/English");
                    byte[] fileBytes3 = System.IO.File.ReadAllBytes(@path3 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName3 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes3, System.Net.Mime.MediaTypeNames.Application.Octet, fileName3);
                case "es-ES":
                    var path4 = Path.Combine(_environment.WebRootPath, "doc/Spanish");
                    byte[] fileBytes4 = System.IO.File.ReadAllBytes(@path4 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName4 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes4, System.Net.Mime.MediaTypeNames.Application.Octet, fileName4);
                case "fr-FR":
                    var path5 = Path.Combine(_environment.WebRootPath, "doc/French");
                    byte[] fileBytes5 = System.IO.File.ReadAllBytes(@path5 + "/" + "Cidofovir UK SmPC.pdf");
                    string fileName5 = "Cidofovir UK SmPC.pdf";
                    return File(fileBytes5, System.Net.Mime.MediaTypeNames.Application.Octet, fileName5);
            }
            return RedirectToAction("NotFound404", "Error");
        }

        [HttpGet]
        public IActionResult GetPIL()
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (cultureName)
            {
                case "nl-BE":
                    var path1 = Path.Combine(_environment.WebRootPath, "doc/Dutch");
                    byte[] fileBytes1 = System.IO.File.ReadAllBytes(@path1 + "/" + "Cidofovir Patiëntenbijsluiter.pdf");
                    string fileName1 = "Cidofovir Patiëntenbijsluiter.pdf";
                    return File(fileBytes1, System.Net.Mime.MediaTypeNames.Application.Octet, fileName1);
                case "de-DE":
                    var path2 = Path.Combine(_environment.WebRootPath, "doc/German");
                    byte[] fileBytes2 = System.IO.File.ReadAllBytes(@path2 + "/" + "Cidofovir-Patienteninformationsblatt.pdf");
                    string fileName2 = "Cidofovir-Patienteninformationsblatt.pdf";
                    return File(fileBytes2, System.Net.Mime.MediaTypeNames.Application.Octet, fileName2);
                case "en-GB":
                    var path3 = Path.Combine(_environment.WebRootPath, "doc/English");
                    byte[] fileBytes3 = System.IO.File.ReadAllBytes(@path3 + "/" + "Cidofovir Patient Information Leaflet.pdf");
                    string fileName3 = "Cidofovir Patient Information Leaflet.pdf";
                    return File(fileBytes3, System.Net.Mime.MediaTypeNames.Application.Octet, fileName3);
                case "es-ES":
                    var path4 = Path.Combine(_environment.WebRootPath, "doc/Spanish");
                    byte[] fileBytes4 = System.IO.File.ReadAllBytes(@path4 + "/" + "Folleto de Información para el paciente de cidofovir.pdf");
                    string fileName4 = "Folleto de Información para el paciente de cidofovir.pdf";
                    return File(fileBytes4, System.Net.Mime.MediaTypeNames.Application.Octet, fileName4);
                case "fr-FR":
                    var path5 = Path.Combine(_environment.WebRootPath, "doc/French");
                    byte[] fileBytes5 = System.IO.File.ReadAllBytes(@path5 + "/" + "Cidofovir Information Patient Feuillet.pdf");
                    string fileName5 = "Cidofovir Information Patient Feuillet.pdf";
                    return File(fileBytes5, System.Net.Mime.MediaTypeNames.Application.Octet, fileName5);
            }
            return RedirectToAction("NotFound404", "Error");
        }

        [HttpGet]
        public IActionResult GetCoverLetter()
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (cultureName)
            {
                case "nl-BE":
                    var path1 = Path.Combine(_environment.WebRootPath, "doc/Dutch");
                    byte[] fileBytes1 = System.IO.File.ReadAllBytes(@path1 + "/" + "VOORBLAD.pdf");
                    string fileName1 = "VOORBLAD.pdf";
                    return File(fileBytes1, System.Net.Mime.MediaTypeNames.Application.Octet, fileName1);
                case "de-DE":
                    var path2 = Path.Combine(_environment.WebRootPath, "doc/German");
                    byte[] fileBytes2 = System.IO.File.ReadAllBytes(@path2 + "/" + "MOTIVATIONSSCHREIBEN.pdf");
                    string fileName2 = "MOTIVATIONSSCHREIBEN.pdf";
                    return File(fileBytes2, System.Net.Mime.MediaTypeNames.Application.Octet, fileName2);
                case "en-GB":
                    var path3 = Path.Combine(_environment.WebRootPath, "doc/English");
                    byte[] fileBytes3 = System.IO.File.ReadAllBytes(@path3 + "/" + "COVER LETTER.pdf");
                    string fileName3 = "COVER LETTER.pdf";
                    return File(fileBytes3, System.Net.Mime.MediaTypeNames.Application.Octet, fileName3);
                case "es-ES":
                    var path4 = Path.Combine(_environment.WebRootPath, "doc/Spanish");
                    byte[] fileBytes4 = System.IO.File.ReadAllBytes(@path4 + "/" + "CARTA DE PRESENTACIÓN.pdf");
                    string fileName4 = "CARTA DE PRESENTACIÓN.pdf";
                    return File(fileBytes4, System.Net.Mime.MediaTypeNames.Application.Octet, fileName4);
                case "fr-FR":
                    var path5 = Path.Combine(_environment.WebRootPath, "doc/French");
                    byte[] fileBytes5 = System.IO.File.ReadAllBytes(@path5 + "/" + "LETTRE DE MOTIVATION.pdf");
                    string fileName5 = "LETTRE DE MOTIVATION.pdf";
                    return File(fileBytes5, System.Net.Mime.MediaTypeNames.Application.Octet, fileName5);
            }
            return RedirectToAction("NotFound404", "Error");
        }

        [HttpGet]
        public IActionResult GetProtocolSynopsis()
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (cultureName)
            {
                case "nl-BE":
                    var path1 = Path.Combine(_environment.WebRootPath, "doc/Dutch");
                    byte[] fileBytes1 = System.IO.File.ReadAllBytes(@path1 + "/" + "Bijlage 1.6 Protocolsamenvatting.pdf");
                    string fileName1 = "Bijlage 1.6 Protocolsamenvatting.pdf";
                    return File(fileBytes1, System.Net.Mime.MediaTypeNames.Application.Octet, fileName1);
                case "de-DE":
                    var path2 = Path.Combine(_environment.WebRootPath, "doc/German");
                    byte[] fileBytes2 = System.IO.File.ReadAllBytes(@path2 + "/" + "Anhang 1.6 Protokollzusammenfassung.pdf");
                    string fileName2 = "Anhang 1.6 Protokollzusammenfassung.pdf";
                    return File(fileBytes2, System.Net.Mime.MediaTypeNames.Application.Octet, fileName2);
                case "en-GB":
                    var path3 = Path.Combine(_environment.WebRootPath, "doc/English");
                    byte[] fileBytes3 = System.IO.File.ReadAllBytes(@path3 + "/" + "Annex 1.6 Protocol Synopsis.pdf");
                    string fileName3 = "Annex 1.6 Protocol Synopsis.pdf";
                    return File(fileBytes3, System.Net.Mime.MediaTypeNames.Application.Octet, fileName3);
                case "es-ES":
                    var path4 = Path.Combine(_environment.WebRootPath, "doc/Spanish");
                    byte[] fileBytes4 = System.IO.File.ReadAllBytes(@path4 + "/" + "Anexo 1.6 Sinopsis del Protocolo.pdf");
                    string fileName4 = "Anexo 1.6 Sinopsis del Protocolo.pdf";
                    return File(fileBytes4, System.Net.Mime.MediaTypeNames.Application.Octet, fileName4);
                case "fr-FR":
                    var path5 = Path.Combine(_environment.WebRootPath, "doc/French");
                    byte[] fileBytes5 = System.IO.File.ReadAllBytes(@path5 + "/" + "Annexe 1.6 Synopsis du Protocole.pdf");
                    string fileName5 = "Annexe 1.6 Synopsis du Protocole.pdf";
                    return File(fileBytes5, System.Net.Mime.MediaTypeNames.Application.Octet, fileName5);
            }
            return RedirectToAction("NotFound404", "Error");
        }

        [HttpGet]
        public IActionResult GetPrescriberGuide()
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            switch (cultureName)
            {
                case "nl-BE":
                    var path1 = Path.Combine(_environment.WebRootPath, "doc/Dutch");
                    byte[] fileBytes1 = System.IO.File.ReadAllBytes(@path1 + "/" + "Bijlage 1.2 Handleiding voor Voorschrijvers.pdf");
                    string fileName1 = "Bijlage 1.2 Handleiding voor Voorschrijvers.pdf";
                    return File(fileBytes1, System.Net.Mime.MediaTypeNames.Application.Octet, fileName1);
                case "de-DE":
                    var path2 = Path.Combine(_environment.WebRootPath, "doc/German");
                    byte[] fileBytes2 = System.IO.File.ReadAllBytes(@path2 + "/" + "Anhang 1.2 Vermittlerhandbuch.pdf");
                    string fileName2 = "Anhang 1.2 Vermittlerhandbuch.pdf";
                    return File(fileBytes2, System.Net.Mime.MediaTypeNames.Application.Octet, fileName2);
                case "en-GB":
                    var path3 = Path.Combine(_environment.WebRootPath, "doc/English");
                    byte[] fileBytes3 = System.IO.File.ReadAllBytes(@path3 + "/" + "Annex 1.2 Prescribers Guide.pdf");
                    string fileName3 = "Annex 1.2 Prescribers Guide.pdf";
                    return File(fileBytes3, System.Net.Mime.MediaTypeNames.Application.Octet, fileName3);
                case "es-ES":
                    var path4 = Path.Combine(_environment.WebRootPath, "doc/Spanish");
                    byte[] fileBytes4 = System.IO.File.ReadAllBytes(@path4 + "/" + "Anexo 1.2 Guía para Prescriptores.pdf");
                    string fileName4 = "Anexo 1.2 Guía para Prescriptores.pdf";
                    return File(fileBytes4, System.Net.Mime.MediaTypeNames.Application.Octet, fileName4);
                case "fr-FR":
                    var path5 = Path.Combine(_environment.WebRootPath, "doc/French");
                    byte[] fileBytes5 = System.IO.File.ReadAllBytes(@path5 + "/" + "Annexe 1.2 Guide des Prescripteurs.pdf");
                    string fileName5 = "Annexe 1.2 Guide des Prescripteurs.pdf";
                    return File(fileBytes5, System.Net.Mime.MediaTypeNames.Application.Octet, fileName5);
            }
            return RedirectToAction("NotFound404", "Error");
        }
    }
}
