using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmcureCERI.Business.Contract;
using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Business.Core.ServiceImplementations;
using EmcureCERI.Business.Models;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Classes;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Models;
using EmcureCERI.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmcureCERI.Web.Controllers
{
    //[Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ISMTPService _sMTPService;
        private readonly IEmailService _emailService;
        IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISpecializationService _specialists;
        private readonly IQAService _qa;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IMessageService _messageService;
        private readonly EmcureCERIDBContext _db;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IGlobalService _global;
        private readonly IMaster_ContinentService _region;
        private readonly IMaster_CountryService _country;
        //private readonly ITbl_Master_DepartmentService _dept;


        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IGlobalService global,
             IConfiguration configuration,
             IMessageService messageService,
             ISpecializationService specialists,
             IStringLocalizer<SharedResource> sharedLocalizer,
             IQAService qa,
              IMaster_ContinentService reg,
             IMaster_CountryService country,IConfiguration config, IHostingEnvironment env, IEmailService emailService, ISMTPService sMTPService
            /* ITbl_Master_DepartmentService dept*/)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _config = config;
            this._env = env;
            _emailService = emailService;
            _sMTPService = sMTPService;
            _logger = logger;
            _configuration = configuration;
            _messageService = messageService;
            _specialists = specialists;
            _qa = qa;
            _sharedLocalizer = sharedLocalizer;
            _global = global;
            _region = reg;
            _country = country;
            //_dept = dept;
            _db = new EmcureCERIDBContext();
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    // Resolve the user via their email
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    //Check Login Enable or not
                    if (user.IsEnabled == false)
                    {
                        await _signInManager.SignOutAsync();
                        _logger.LogInformation("User logged out.");
                        //Don't reveal that the user does not exist or is disabled by admin
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                    // Get the roles and name for the user
                    var roles = await _userManager.GetRolesAsync(user);
                    var log_user = _db.AspNetUsers.Where(o => o.Email == model.Email).FirstOrDefault();
                    var userDetails = _db.PrescriberDetails.Where(o => o.AspNetUserId == log_user.UserId).FirstOrDefault();

                    HttpContext.Session.SetString("CurrentUserId", userDetails.AspNetUserId.ToString());
                    HttpContext.Session.SetString("CurrentUserName", userDetails.FirstName + " " + userDetails.LastName);
                    HttpContext.Session.SetString("CurrentUserRole", roles.FirstOrDefault());
                    HttpContext.Session.SetString("CurrentUserCompanyID", userDetails.CompanyID.ToString());
                    if (userDetails.CompanyID==1)
                    {
                        HttpContext.Session.SetString("CurrentUserCompany", "Emcure");                        
                    }
                    else
                    {
                        HttpContext.Session.SetString("CurrentUserCompany", "Gennova");
                    }


                    _logger.LogInformation("User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        // [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public IActionResult Register(string returnUrl = null)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;

            List<SelectListItem> specializations = new List<SelectListItem>();

            specializations.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Role"].Value, Selected = true });

            var specialists = _db.AspNetRoles.ToList();

            // var specialists = _specialists.GetAllSpecialization().Results.Where(o => o.IsEnabled == true).ToList();
            foreach (var item in specialists)
            {

                specializations.Add(new SelectListItem() { Value = item.Name.ToString(), Text = item.Name });

               

            }
            // specializations.Add(new SelectListItem() { Value = "0", Text = _sharedLocalizer["Other"].Value });
            List<SelectListItem> Departments = new List<SelectListItem>();
            List<SelectListItem> Geographys = new List<SelectListItem>();
            List<SelectListItem> Countrys = new List<SelectListItem>();
           Departments.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Department"].Value, Selected = true });
            Geographys.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Geography"].Value, Selected = true });
            Countrys.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Country"].Value, Selected = true });
           var DepartmentList = _db.Tbl_Master_Department.ToList();
            var GeographyList = _region.GetAllRegion().Results.ToList();
            var CountryList = _country.GetAllCountry().Results.ToList();
            foreach (var item in DepartmentList)
            {
                Departments.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Department });

            }
            foreach (var item in GeographyList)
            {
                Geographys.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Continent });

            }
            foreach (var item in CountryList)
            {
                Countrys.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Country });

            }

            var questionList = _qa.GetAllQuestion().Results.ToList();

            List<SelectListItem> question1s = new List<SelectListItem>();
            List<SelectListItem> question2s = new List<SelectListItem>();
            List<SelectListItem> question3s = new List<SelectListItem>();
            question1s.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Question"].Value, Selected = true });
            question2s.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Question"].Value, Selected = true });
            question3s.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Question"].Value, Selected = true });
            foreach (var item in questionList)
            {
                switch (cultureName)
                {
                    case "nl-BE":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionBe });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionBe });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionBe });
                        break;
                    case "de-DE":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionDe });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionDe });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionDe });
                        break;
                    case "en-GB":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionGb });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionGb });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionGb });
                        break;
                    case "es-ES":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionEs });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionEs });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionEs });
                        break;
                    case "fr-FR":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionFr });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionFr });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionFr });
                        break;
                }
            }

            RegisterViewModel reg = new RegisterViewModel();
            reg.Question1s = question1s;
            reg.Question2s = question2s;
            reg.Question3s = question3s;
            reg.Specializations = specializations;
            reg.Departments = Departments;
            reg.Geographys = Geographys;
            reg.Countries = Countrys;
            // reg.IsDoctorPharmacist = true;
            ViewBag.Selected = null;
            ViewBag.SelectedQ1 = null;
            ViewBag.SelectedQ2 = null;
            ViewBag.SelectedQ3 = null;
            ViewBag.Department = null;
            ViewBag.Geography = null;
            ViewBag.Country = null;
            ViewData["ReturnUrl"] = returnUrl;
            return View(reg);
        }


        protected virtual string IncreamentUnique(string prefix, string unique)
        {
            if (unique != null && unique != "")
            {
                var suffixNum = unique.Split(prefix);
                int increment = Convert.ToInt32(suffixNum[1]) + 1;
                return string.Concat(prefix, increment.ToString("0000"));
            }
            else
            {
                return "";
            }
        }


        [HttpPost]
        // [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        [Obsolete]
        //public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            // ViewData["ReturnUrl"] = "~/Account/Register.cshtml";

            //if (model.IsDoctorPharmacist == false)
            //{
            //    ModelState.Remove("Specialization");
            //    ModelState.Remove("OtherSpecialization");
            //}
            //else
            //{
            //    if (model.Specialization != "0")
            //    {
            //        ModelState.Remove("OtherSpecialization");
            //    }
            //}
            #region Saveuser
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, IsEnabled = true, IsRejected = false, IsDeactivated = false, IsChangePassword = false, EmailConfirmed = true };
                // string password = GenerateRandomPassword();
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //Assign Role to user Here 
                    await this._userManager.AddToRoleAsync(user, model.Specialization);

                    //Ends Here
                    PrescriberDetails perDetail = new PrescriberDetails();
                    int userId = _db.AspNetUsers.Where(o => o.Email == model.Email).Select(o => o.UserId).FirstOrDefault();
                    perDetail.AspNetUserId = userId;
                    perDetail.FirstName = model.FirstName;
                    perDetail.LastName = model.LastName;
                    // perDetail.IsDoctorPharmacist = true;
                    // perDetail.IsDoctorPharmacist = model.IsDoctorPharmacist;
                    //  perDetail.SpecializationId = 1;
                    // perDetail.SpecializationId = Convert.ToInt32(model.Specialization);

                    // perDetail.OtherSpecialization = model.OtherSpecialization;
                    //  perDetail.GmcgpHcnumber = model.GmcgpHcnumber;
                    //  perDetail.HospitalAddress = model.HospitalAddress;
                    perDetail.DepartmentID = model.DepartmentID;
                    perDetail.GeographyId = model.GeographyId;
                    perDetail.CountryId = model.CountryId;
                    perDetail.ContactAddress = model.ContactAddress;
                    perDetail.TelephoneNumber = model.TelephoneNumber;
                    perDetail.CreatedOnUtc = DateTime.Now;
                    perDetail.UniqueId = IncreamentUnique("CERIHCP", _db.PrescriberDetails.Select(o => o.UniqueId).LastOrDefault());
                    _db.PrescriberDetails.Add(perDetail);
                    _db.SaveChanges();


                    AnswerDetails ansModel1 = new AnswerDetails();
                    ansModel1.QuestId = Convert.ToInt32(model.Question1);
                    ansModel1.PrescriberId = userId;
                    ansModel1.Answer = model.Answer1;
                    _qa.AddAnswerMap(ansModel1);


                    AnswerDetails ansModel2 = new AnswerDetails();
                    ansModel2.QuestId = Convert.ToInt32(model.Question2);
                    ansModel2.PrescriberId = userId;
                    ansModel2.Answer = model.Answer2;
                    _qa.AddAnswerMap(ansModel2);


                    AnswerDetails ansModel3 = new AnswerDetails();
                    ansModel3.QuestId = Convert.ToInt32(model.Question3);
                    ansModel3.PrescriberId = userId;
                    ansModel3.Answer = model.Answer3;
                    _qa.AddAnswerMap(ansModel3);


                    var prescriberFullName = model.FirstName + " " + model.LastName;

                    //Mail To Prescriber
                    //EmailHelper email = new EmailHelper();
                    //string url = _global.GetBaseUrl() + "/Account/Login";
                    //string emailbody = _messageService.GetAcknowledgement(prescriberFullName, user.Email, url);
                    //email.SendMail(user.Email, "", _sharedLocalizer["Welcome to"].Value + " Tillomed portal", emailbody);


                    //Mail To Admin
                    //var adminObj = _db.AspNetUsers.Where(o => o.UserId == 1).FirstOrDefault();
                    //var adminDetail = _db.PrescriberDetails.Where(o => o.AspNetUserId == adminObj.UserId).FirstOrDefault();
                    //var adminFullName = adminDetail.FirstName + " " + adminDetail.LastName;

                    //EmailHelper email1 = new EmailHelper();
                    //string emailbody1 = _messageService.GetAdminAcknowledgement(adminFullName, prescriberFullName, url);
                    //email1.SendMail(adminObj.Email, "", "New Registration Request for " + prescriberFullName, emailbody1);

                    //_logger.LogInformation("User created a new account with password.");

                    //Following mail code added by Yogesh on date 02/04/2020
                    MailDetails(String.Concat(model.FirstName, " ", model.LastName), model.Password, model.Email, "User Registered at Emcure Projects Notification");

                    TempData["Message"] = "Registered Successfully";
                    // ViewData["SuccessMessage"] = "Thank you for registering your interest in the PASS, you will be contacted via email shortly regarding your registration status";
                    ViewData["SuccessMessage"] = "User Registered Successfully";
                    // return View(model);
                    //return RedirectToLocal(returnUrl);  Rahul
                }
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName")
                    {
                        error.Description = "";
                    }
                }
                AddErrors(result);
            }
            #endregion
            //var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;

            //List<SelectListItem> specializations = new List<SelectListItem>();
            //specializations.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Role"].Value });

            //var specialists = _db.AspNetRoles.ToList();

            ////   var specialists = _specialists.GetAllSpecialization().Results.Where(o => o.IsEnabled == true).ToList();
            //foreach (var item in specialists)
            //{

            //    specializations.Add(new SelectListItem() { Value = item.Name.ToString(), Text = item.Name });


            //}
            ////specializations.Add(new SelectListItem() { Value = "0", Text = _sharedLocalizer["Other"].Value });
            //model.Specializations = specializations; 

            //var questionList = _qa.GetAllQuestion().Results.ToList();
            //List<SelectListItem> question1s = new List<SelectListItem>();
            //List<SelectListItem> question2s = new List<SelectListItem>();
            //List<SelectListItem> question3s = new List<SelectListItem>();
            //question1s.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Question"].Value, Selected = true });
            //question2s.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Question"].Value, Selected = true });
            //question3s.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Question"].Value, Selected = true });
            //foreach (var item in questionList)
            //{
            //    switch (cultureName)
            //    {
            //        case "nl-BE":
            //            question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionBe });
            //            question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionBe });
            //            question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionBe });
            //            break;
            //        case "de-DE":
            //            question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionDe });
            //            question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionDe });
            //            question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionDe });
            //            break;
            //        case "en-GB":
            //            question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionGb });
            //            question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionGb });
            //            question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionGb });
            //            break;
            //        case "es-ES":
            //            question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionEs });
            //            question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionEs });
            //            question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionEs });
            //            break;
            //        case "fr-FR":
            //            question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionFr });
            //            question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionFr });
            //            question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionFr });
            //            break;
            //    }
            //}


            //model.Question1s = question1s;
            //model.Question2s = question2s;
            //model.Question3s = question3s;

            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;

            List<SelectListItem> specializations = new List<SelectListItem>();

            specializations.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Role"].Value, Selected = true });

            var specialists = _db.AspNetRoles.ToList();

            // var specialists = _specialists.GetAllSpecialization().Results.Where(o => o.IsEnabled == true).ToList();
            foreach (var item in specialists)
            {

                specializations.Add(new SelectListItem() { Value = item.Name.ToString(), Text = item.Name });



            }
            // specializations.Add(new SelectListItem() { Value = "0", Text = _sharedLocalizer["Other"].Value });
            List<SelectListItem> Departments = new List<SelectListItem>();
            List<SelectListItem> Geographys = new List<SelectListItem>();
            List<SelectListItem> Countrys = new List<SelectListItem>();
            Departments.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Department"].Value, Selected = true });
            Geographys.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Geography"].Value, Selected = true });
            Countrys.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Country"].Value, Selected = true });
            var DepartmentList = _db.Tbl_Master_Department.ToList();
            var GeographyList = _region.GetAllRegion().Results.ToList();
            var CountryList = _country.GetAllCountry().Results.ToList();
            foreach (var item in DepartmentList)
            {
                Departments.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Department });

            }
            foreach (var item in GeographyList)
            {
                Geographys.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Continent });

            }
            foreach (var item in CountryList)
            {
                Countrys.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Country });

            }

            var questionList = _qa.GetAllQuestion().Results.ToList();

            List<SelectListItem> question1s = new List<SelectListItem>();
            List<SelectListItem> question2s = new List<SelectListItem>();
            List<SelectListItem> question3s = new List<SelectListItem>();
            question1s.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Question"].Value, Selected = true });
            question2s.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Question"].Value, Selected = true });
            question3s.Add(new SelectListItem() { Value = null, Text = _sharedLocalizer["Please select Question"].Value, Selected = true });
            foreach (var item in questionList)
            {
                switch (cultureName)
                {
                    case "nl-BE":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionBe });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionBe });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionBe });
                        break;
                    case "de-DE":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionDe });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionDe });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionDe });
                        break;
                    case "en-GB":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionGb });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionGb });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionGb });
                        break;
                    case "es-ES":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionEs });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionEs });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionEs });
                        break;
                    case "fr-FR":
                        question1s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionFr });
                        question2s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionFr });
                        question3s.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.QuestionFr });
                        break;
                }
            }

            RegisterViewModel reg = new RegisterViewModel();
            reg.Question1s = question1s;
            reg.Question2s = question2s;
            reg.Question3s = question3s;
            reg.Specializations = specializations;
            reg.Departments = Departments;
            reg.Geographys = Geographys;
            reg.Countries = Countrys;





            // If we got this far, something failed, redisplay form
            return View(reg);
        }

        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("User created a new account with password.");

        //            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
        //            await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            _logger.LogInformation("User created a new account with password.");
        //            return RedirectToLocal(returnUrl);
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout(int id = 0)
        {
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
                else
                {
                    // return RedirectToAction("SecurityQuestion", "Account", new { Email = user.Email });

                    // For more information on how to enable account confirmation and password reset please
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                    EmailHelper email = new EmailHelper();

                    string afn = "";
                    using (EmcureCERIDBContext _context = new EmcureCERIDBContext())
                    {
                        var user1 = _context.AspNetUsers.Where(o => o.Email == model.Email).FirstOrDefault();
                        afn = _context.PrescriberDetails.Where(o => o.AspNetUserId == user1.UserId).Select(o => o.FirstName).FirstOrDefault();
                    }

                    string emailBody = _messageService.GetForgotPasswordMessage(afn, callbackUrl);
                    email.SendMail(model.Email, "", _sharedLocalizer["Reset Password"].Value, emailBody);
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));

                }

                //// For more information on how to enable account confirmation and password reset please
                //// visit https://go.microsoft.com/fwlink/?LinkID=532713
                //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                //EmailHelper email = new EmailHelper();

                //string afn = "";
                //using (EmcureCERIDBContext _context = new EmcureCERIDBContext())
                //{
                //    var user1 = _context.AspNetUsers.Where(o => o.Email == model.Email).FirstOrDefault();
                //    afn = _context.PrescriberDetails.Where(o => o.AspNetUserId == user1.UserId).Select(o => o.Name).FirstOrDefault();
                //}

                //string emailBody = _messageService.GetForgotPasswordMessage(afn, callbackUrl);
                //email.SendMail(model.Email, "", "Reset Password", emailBody);
                //return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SecurityQuestion(string Email)
        {
            if (Email == null || Email == "")
            {
                return RedirectToAction(nameof(ForgotPassword));
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            int userId = _db.AspNetUsers.Where(o => o.Email == Email).Select(o => o.UserId).FirstOrDefault();

            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
            List<QuestionAnswer> model22 = _qa.GetAllQuesAnsByUserId(userId, cultureName);
            SecurityQuestionViewModel model = new SecurityQuestionViewModel();

            for (int i = 0; i < model22.Count(); i++)
            {
                switch (i)
                {
                    case 0:
                        model.Question1 = model22[i].Question;
                        break;
                    case 1:
                        model.Question2 = model22[i].Question;
                        break;
                    case 2:
                        model.Question3 = model22[i].Question;
                        break;
                }
            }
            model.Email = Email;
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SecurityQuestion(SecurityQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
                int userId = _db.AspNetUsers.Where(o => o.Email == model.Email).Select(o => o.UserId).FirstOrDefault();
                var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;
                List<QuestionAnswer> model22 = _qa.GetAllQuesAnsByUserId(userId, cultureName);

                bool IsAnswerCorrect = true;

                for (int i = 0; i < model22.Count(); i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (model.Answer1.ToLower() != model22[i].Answer.ToLower())
                            {
                                IsAnswerCorrect = false;
                            }
                            break;
                        case 1:
                            if (model.Answer2.ToLower() != model22[i].Answer.ToLower())
                            {
                                IsAnswerCorrect = false;
                            }
                            break;
                        case 2:
                            if (model.Answer3.ToLower() != model22[i].Answer.ToLower())
                            {
                                IsAnswerCorrect = false;
                            }
                            break;
                    }
                }

                if (IsAnswerCorrect)
                {
                    // For more information on how to enable account confirmation and password reset please
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                    EmailHelper email = new EmailHelper();

                    string afn = "";
                    using (EmcureCERIDBContext _context = new EmcureCERIDBContext())
                    {
                        var user1 = _context.AspNetUsers.Where(o => o.Email == model.Email).FirstOrDefault();
                        afn = _context.PrescriberDetails.Where(o => o.AspNetUserId == user1.UserId).Select(o => o.FirstName).FirstOrDefault();
                    }

                    string emailBody = _messageService.GetForgotPasswordMessage(afn, callbackUrl);
                    email.SendMail(model.Email, "", _sharedLocalizer["Reset Password"].Value, emailBody);
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
                else
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion


        [Authorize]
        public JsonResult GetDropdownForRegisterForm()
        {
            var CountryLists = _db.Master_Country
                .AsNoTracking()
                 .Where(x => x.IsActive == true)
                 .OrderBy(x => x.Country)
                .Select(x => new { x.Id, x.Country });
            return Json(new
            {
                data = new
                {
                    CountryLists = CountryLists
                    
                }
            });
        }

        [Authorize]
        public JsonResult GetRegionwiseCountry(int RegionID)
        {
            if(RegionID != 0 && RegionID != 8) //8 for all and 0 for please select option
            {
                var CountryLists = _db.Master_Country
                .AsNoTracking()
                 .Where(x => x.IsActive == true && x.ContinentID == RegionID)
                 .OrderBy(x => x.Country)
                .Select(x => new { x.Id, x.Country });
                return Json(new
                {
                    data = new
                    {
                        CountryLists = CountryLists
                    }
                });
            }
            else
            {
                var CountryLists = _db.Master_Country
               .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Country)
               .Select(x => new { x.Id, x.Country });
                return Json(new
                {
                    data = new
                    {
                        CountryLists = CountryLists
                    }
                });
            }
            
        }

        [Obsolete]
        private void MailDetails(string UserName,string UserPassword,string UserEmailID, string strSubject)
        {
            try
            {
                //send email notification code added by yogesh balapure on date 02/04/2021
                //get smtp details 
                SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();                
                SMTPDetailsVM sMTPDetailsVM = new SMTPDetailsVM();
                EmailDetailsVM emailDetailsVM = new EmailDetailsVM();

                if (sMTPDetailsModel != null)
                {
                    sMTPDetailsVM.AliasName = sMTPDetailsModel.AliasName;
                    sMTPDetailsVM.HostName = sMTPDetailsModel.HostName;
                    sMTPDetailsVM.FromMail = sMTPDetailsModel.FromMail;
                    sMTPDetailsVM.FromPassword = sMTPDetailsModel.FromPassword;
                    sMTPDetailsVM.IsEnableSSL = sMTPDetailsModel.IsEnableSSL;
                    sMTPDetailsVM.PortNumber = sMTPDetailsModel.PortNumber;
                    sMTPDetailsVM.IsMailStatus = sMTPDetailsModel.IsMailStatus;
                    sMTPDetailsVM.IsDefaultCredentials = sMTPDetailsModel.IsDefaultCredentials;
                    sMTPDetailsVM.IsWithoutPassword = sMTPDetailsModel.IsWithoutPassword;
                }                                            
                    
                if (!string.IsNullOrEmpty(UserEmailID))
                {
                    string strEmailMessage = UserName + "</br>" +
                                            "Thank you for using the Emcure Projects portal." + "</br></br>" +
                                            "UserName: " + UserEmailID + "</br>" +
                                            "Default Password : " + UserPassword + "</br></br>" +
                                            "Should you wish to change the system generated password, once you login, you can change the same, by visiting your Profile section." + "</br>" +
                                            "or After Login Visit";
                    //List<string> testBCC = new List<string>();
                    //List<string> testCC = new List<string>();
                    //testBCC.Add(ddata.Email.Trim());
                    //testCC.Add(ccdata.Trim());
                    emailDetailsVM.ToMail = UserEmailID.Trim();

                    //emailDetailsVM.CCMail = testCC;
                    //emailDetailsVM.BCCMail = testBCC;
                    emailDetailsVM.Subject = strSubject;

                    clsTemplate _clsTemplate = new clsTemplate(_config, _env);
                    string tempurl = _config.GetSection("ApplicationURL:ChangePasswordUrlLink").Value;
                    emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                    emailDetailsVM.DispalyName = "Emcure Project Management";

                    if (sMTPDetailsVM != null && emailDetailsVM != null)
                    {
                        EmailHelper emailHelper = new EmailHelper();
                        bool tempbln = emailHelper.SendMail1(sMTPDetailsVM, emailDetailsVM,_env.ContentRootPath,false);
                    }
                }                 
                

            }
            catch (Exception ex)
            {

            }

        }
    }
}