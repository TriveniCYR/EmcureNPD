using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmcureCERI.Business.Contract;
using EmcureCERI.Business.Core.ServiceImplementations;
using EmcureCERI.Business.Models;
using EmcureCERI.Business.Models.DataModel;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Models;
using EmcureCERI.Web.Models.AdminViewModels;
using EmcureCERI.Web.Models.PrescriberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace EmcureCERI.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmcureCERIDBContext _db;
        private readonly IConfiguration _configuration;
        private readonly IMessageService _messageService;
        private readonly ISpecializationService _specialists;
        private readonly IPrescriberService _prescriber;
        private readonly IPatientService _patient;
        private readonly IBaselineDataMaster _baselinedata;
        private readonly IAdminService _adminService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IGlobalService _global;

        public AdminController(IAdminService adminService,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IMessageService messageService,
            ISpecializationService specialists,
            IPrescriberService prescriber,
            IGlobalService global,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IPatientService patient, IBaselineDataMaster baselinedata)
        {
            this._userManager = userManager;
            this._db = new EmcureCERIDBContext();
            this._configuration = configuration;
            this._messageService = messageService;
            this._specialists = specialists;
            this._prescriber = prescriber;
            this._patient = patient;
            this._baselinedata = baselinedata;
            this._adminService = adminService;
            this._sharedLocalizer = sharedLocalizer;
            this._global = global;
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult PrescriberDetails(int id = 0)
        {
            var cultureName = System.Globalization.CultureInfo.CurrentCulture.Name;

            if (id == 0)
            {
                PrescriberViewModel pvm = new PrescriberViewModel();
                return View(pvm);
            }
            else
            {
                var _user = _db.PrescriberDetails.Where(o => o.AspNetUserId == id).FirstOrDefault();
                var _aspuser = _db.AspNetUsers.Where(o => o.UserId == id).FirstOrDefault();

                PrescriberViewModel pvm = new PrescriberViewModel();
                pvm.FirstName = _user.FirstName;
                pvm.LastName = _user.LastName;
                pvm.TelNo = _user.TelephoneNumber;
                pvm.Email = _aspuser.Email;
                pvm.UniqueId = _user.UniqueId;
                pvm.IsDoctorPharmacist = (_user.IsDoctorPharmacist == true) ? "Doctor" : "Pharmacist";
                pvm.IsHide = true;

                if (_user.IsDoctorPharmacist)
                {
                    Specialization spId = _specialists.GetAllSpecialization().Results.Where(o => o.Id == _user.SpecializationId).FirstOrDefault();
                    if (spId == null)
                    {
                        pvm.Specialization = _user.OtherSpecialization;
                    }
                    else
                    {
                        if (spId.Id != 0)
                        {
                            switch (cultureName)
                            {
                                case "nl-BE":
                                    pvm.Specialization = _specialists.GetAllSpecialization().Results.Where(o => o.Id == _user.SpecializationId).Select(o => o.NameBe).FirstOrDefault();
                                    break;
                                case "de-DE":
                                    pvm.Specialization = _specialists.GetAllSpecialization().Results.Where(o => o.Id == _user.SpecializationId).Select(o => o.NameDe).FirstOrDefault();
                                    break;
                                case "en-GB":
                                    pvm.Specialization = _specialists.GetAllSpecialization().Results.Where(o => o.Id == _user.SpecializationId).Select(o => o.NameGb).FirstOrDefault();
                                    break;
                                case "es-ES":
                                    pvm.Specialization = _specialists.GetAllSpecialization().Results.Where(o => o.Id == _user.SpecializationId).Select(o => o.NameEs).FirstOrDefault();
                                    break;
                                case "fr-FR":
                                    pvm.Specialization = _specialists.GetAllSpecialization().Results.Where(o => o.Id == _user.SpecializationId).Select(o => o.NameFr).FirstOrDefault();
                                    break;
                            }

                        }
                        else
                        {
                            pvm.Specialization = _user.OtherSpecialization;
                        }
                    }
                }
                else
                {
                    pvm.Specialization = "";
                }

                pvm.HospitalAddress = _user.HospitalAddress;
                pvm.ContactAddress = _user.ContactAddress;
                pvm.GMCGpHCNumber = _user.GmcgpHcnumber;
                return View(pvm);
            }
        }

        /// <summary>
        /// Add Prescriber User 
        /// </summary>
        /// <returns>View Page</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult AddPrescriber()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult PrescriberAddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                PrescriberViewModel pvm = new PrescriberViewModel();
                pvm.IsHide = true;
                return View(pvm);
            }
            else
            {
                var _user = _db.PrescriberDetails.Where(o => o.AspNetUserId == id).FirstOrDefault();
                var _aspuser = _db.AspNetUsers.Where(o => o.UserId == id).FirstOrDefault();

                PrescriberViewModel pvm = new PrescriberViewModel();
                pvm.Id = _user.Id;
                pvm.FirstName = _user.FirstName;
                pvm.LastName = _user.LastName;
                //pvm.Email = _aspuser.Email;
                //pvm.ContactNo = _user.ContactNo;
                pvm.IsHide = false;
                return View(pvm);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public JsonResult GetPrescriber()
        {
            var user = from s in _db.AspNetUsers
                       join r in _db.AspNetUserRoles on s.Id equals r.UserId
                       join t in _db.AspNetRoles on r.RoleId equals t.Id
                       where t.Name == "Prescriber"
                       select new
                       {
                           UserId = s.UserId,
                           Email = s.Email,
                           Role = t.Name,
                           IsEnable = s.IsEnabled
                       };
            List<PrescriberViewModel> userVMList = new List<PrescriberViewModel>();

            foreach (var item in user)
            {
                PrescriberDetails _user = _db.PrescriberDetails.Where(o => o.AspNetUserId == item.UserId).FirstOrDefault();
                if (_user != null)
                {
                    if (item.IsEnable)
                    {
                        PrescriberViewModel userVM = new PrescriberViewModel();
                        userVM.Id = item.UserId;
                        //userVM.Name = _user.FirstName;
                        //userVM.LastName = _user.LastName;
                        //userVM.Email = item.Email;
                        //userVM.ContactNo = _user.ContactNo;
                        userVM.IsEnable = item.IsEnable;
                        userVMList.Add(userVM);
                    }
                }

            }
            return Json(new { data = userVMList }, new JsonSerializerSettings());
        }

        public class Newmodel
        {
            public int UserId { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public bool IsEnable { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public JsonResult GetAllRequestedPrescriber(int id)
        {

            IEnumerable<Newmodel> user = new List<Newmodel>();
            if (id == 1)
            {

                user = from s in _db.AspNetUsers
                       join r in _db.AspNetUserRoles on s.Id equals r.UserId
                       join t in _db.AspNetRoles on r.RoleId equals t.Id
                       where t.Name == "Prescriber" && s.EmailConfirmed == false && s.IsEnabled == false && s.IsRejected == false
                       select new Newmodel
                       {
                           UserId = s.UserId,
                           Email = s.Email,
                           Role = t.Name,
                           IsEnable = s.IsEnabled
                       };
            }
            if (id == 2)
            {
                user = from s in _db.AspNetUsers
                       join r in _db.AspNetUserRoles on s.Id equals r.UserId
                       join t in _db.AspNetRoles on r.RoleId equals t.Id
                       where t.Name == "Prescriber" && s.EmailConfirmed == true && s.IsEnabled == true && s.IsRejected == false
                       select new Newmodel
                       {
                           UserId = s.UserId,
                           Email = s.Email,
                           Role = t.Name,
                           IsEnable = s.IsEnabled
                       };

            }
            if (id == 3)
            {
                user = from s in _db.AspNetUsers
                       join r in _db.AspNetUserRoles on s.Id equals r.UserId
                       join t in _db.AspNetRoles on r.RoleId equals t.Id
                       where t.Name == "Prescriber" && s.EmailConfirmed == true && s.IsEnabled == false && s.IsRejected == false
                       select new Newmodel
                       {
                           UserId = s.UserId,
                           Email = s.Email,
                           Role = t.Name,
                           IsEnable = s.IsEnabled
                       };
            }
            if (id == 4)
            {
                user = from s in _db.AspNetUsers
                       join r in _db.AspNetUserRoles on s.Id equals r.UserId
                       join t in _db.AspNetRoles on r.RoleId equals t.Id
                       where t.Name == "Prescriber" && s.EmailConfirmed == false && s.IsEnabled == false && s.IsRejected == true
                       select new Newmodel
                       {
                           UserId = s.UserId,
                           Email = s.Email,
                           Role = t.Name,
                           IsEnable = s.IsEnabled
                       };
            }

            List<PrescriberViewModel> userVMList = new List<PrescriberViewModel>();

            foreach (var item in user)
            {
                PrescriberDetails _user = _db.PrescriberDetails.Where(o => o.AspNetUserId == item.UserId).FirstOrDefault();
                if (_user != null)
                {
                    PrescriberViewModel userVM = new PrescriberViewModel();
                    userVM.Id = item.UserId;
                    userVM.UniqueId = _user.UniqueId;
                    userVM.FirstName = _user.FirstName;
                    userVM.LastName = _user.LastName;
                    userVM.TelNo = _user.TelephoneNumber;
                    userVM.Email = item.Email;
                    userVM.IsDoctorPharmacist = (_user.IsDoctorPharmacist == true) ? "Doctor" : "Pharmacist";
                    userVMList.Add(userVM);

                }

            }
            return Json(new { ids = id, data = userVMList }, new JsonSerializerSettings());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> PrescriberAddOrEdit(PrescriberViewModel model)
        {
            using (EmcureCERIDBContext db = new EmcureCERIDBContext())
            {
                if (model.Id == 0)
                {
                    if (ModelState.IsValid)
                    {

                        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, IsEnabled = true, EmailConfirmed = true };
                        var result = await _userManager.CreateAsync(user, model.Password);

                        if (result.Succeeded)
                        {
                            //Assign Role to user Here 
                            await this._userManager.AddToRoleAsync(user, "Prescriber");

                            //Ends Here
                            PrescriberDetails perDetail = new PrescriberDetails();
                            int userId = _db.AspNetUsers.Where(o => o.Email == model.Email).Select(o => o.UserId).FirstOrDefault();
                            perDetail.AspNetUserId = userId;
                            perDetail.FirstName = model.FirstName;
                            perDetail.LastName = model.LastName;
                            perDetail.CreatedOnUtc = DateTime.Now;
                            _db.PrescriberDetails.Add(perDetail);
                            _db.SaveChanges();

                            return Json(new { success = "success", message = _sharedLocalizer["Saved Successfully"].Value }, new JsonSerializerSettings());
                        }
                        return Json(new { success = "error", message = result.Errors.LastOrDefault() }, new JsonSerializerSettings());

                    }
                }
                else
                {
                    var uer = _db.PrescriberDetails.Where(o => o.AspNetUserId == model.Id).FirstOrDefault();
                    if (uer != null)
                    {
                        PrescriberDetails perDetail = new PrescriberDetails();
                        perDetail.Id = uer.Id;
                        perDetail.AspNetUserId = uer.AspNetUserId;
                        db.Entry(perDetail).State = EntityState.Modified;
                        db.SaveChanges();

                        return Json(new { success = "success", message = _sharedLocalizer["Updated Successfully"].Value }, new JsonSerializerSettings());
                    }


                }
            }
            return View(model);

        }

        /// <summary>
        /// All Patient
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult RegisteredPatients(int id = 0)
        {
            List<SelectListItem> allPrescriber = new List<SelectListItem>();
            allPrescriber.Add(new SelectListItem() { Value = "0", Text = _sharedLocalizer["All Prescriber"].Value });

            int adminId = Convert.ToInt32(_configuration["AdminId"].ToString());
            var prescribers = _prescriber.GetAllPrescribers().Results.ToList();
            foreach (var item in prescribers.Where(o => o.AspNetUserId != adminId))
            {
                if (item.AspNetUserId != 0)
                {
                    allPrescriber.Add(new SelectListItem() { Value = item.AspNetUserId.ToString(), Text = item.FirstName + " " + item.LastName });
                }
            }

            RegisteredPatientViewModel rpvm = new RegisteredPatientViewModel();
            rpvm.PrescriberList = allPrescriber;
            ViewBag.Selected = id;
            return View(rpvm);
        }
        protected virtual string GetStatus(string consent, string baseline)
        {
            if (consent == "Approved" && baseline == "Approved")
                return "Approved";
            if (consent == "Approved" && baseline == "Rejected" || consent == "Rejected" && baseline == "Approved" || consent == "Rejected" && baseline == "Pending")
                return "Partially Rejected";
            if (consent == "Rejected" && baseline == "Rejected")
                return "Rejected";
            if (consent == "Approved" && baseline == "Pending" || consent == "Pending" && baseline == "Approved")
                return "Partially Approved";
            if (consent == "Pending" && baseline == "Pending")
                return "Pending Approval";
            return null;
        }


        [HttpPost]
        public JsonResult GetPatientList(string prescriberId, DTParameters param)
        {

            int TotalCount = 0;
            var filtered = this.GetPatientFiltered(Convert.ToInt32(prescriberId), param.Search.Value, param.SortOrder, param.Start, param.Length, out TotalCount);

            var CustomerList = filtered.Select(o => new PatientViewModel()
            {
                UniqueId = o.UniqueId,
                FirstName = o.FirstName,
                LastName = o.LastName,
                FStatus = o.FStatus,
                CStatus = o.CStatus,
                BStatus = o.BStatus,
                Action = o.Action
            });

            DTResult<PatientViewModel> finalresult = new DTResult<PatientViewModel>
            {
                draw = param.Draw,
                data = CustomerList.ToList(),
                recordsFiltered = TotalCount,
                recordsTotal = filtered.Count

            };
            return Json(new { draw = finalresult.draw, data = finalresult.data, recordsFiltered = finalresult.recordsFiltered, recordsTotal = finalresult.recordsTotal }, new JsonSerializerSettings());
        }

        public List<PatientViewModel> GetPatientFiltered(int id, string search, string sortOrder, int start, int length, out int TotalCount)
        {
            List<PatientViewModel> patientList = new List<PatientViewModel>();
            ServiceResponseList<PatientDetails> allPatient = new ServiceResponseList<PatientDetails>();

            if (id != 0)
            {
                allPatient = _patient.GetAllPatientsByPrescriber(id);
            }
            else
            {
                allPatient = _patient.GetAllPatients();
            }

            foreach (var item in allPatient.Results.Where(o => o.PdfName != "" && o.PdfName != null))
            {
                PatientViewModel model = new PatientViewModel();
                model.Id = item.Id;
                model.UniqueId = item.UniqueId;
                model.FirstName = item.FirstName;
                model.LastName = item.LastName;
                model.IsConsentFcheckByAdmin = item.IsConsentFcheckByAdmin;

                if (item.IsStatus == null)
                {
                    model.CStatus = "Pending";
                }
                else
                {
                    if (item.IsStatus == true)
                    {
                        model.CStatus = "Approved";
                    }
                    else
                    {
                        model.CStatus = "Rejected";
                    }
                }

                var baseline = _baselinedata.GetBaselineDataByPatientId(item.Id);
                if (baseline != null)
                {
                    if (baseline.IsStatus == null)
                    {
                        model.BStatus = "Pending";
                    }
                    else
                    {
                        if (baseline.IsStatus == true)
                        {
                            model.BStatus = "Approved";
                        }
                        else
                        {
                            model.BStatus = "Rejected";
                        }
                    }

                    model.IsBaselineDataByAdmin = baseline.IsConfirmedByAdmin;
                }
                else
                {
                    model.BStatus = "Pending";
                    model.IsBaselineDataByAdmin = false;
                }

                model.FStatus = GetStatus(model.CStatus, model.BStatus);

                if (model.CStatus != "Pending")
                {
                    if (model.IsConsentFcheckByAdmin)
                    {
                        model.CStatus = "<a data-toggle='tooltip' title=" + _sharedLocalizer["Approved"].Value + " class='btn btn-success btn-sm'> <span class='glyphicon glyphicon-ok' aria-hidden='true'></span></a>";
                    }
                    else
                    {
                        model.CStatus = "<a title=" + _sharedLocalizer["Rejected"].Value + " class='btn btn-danger btn-sm'> <span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a>";
                    }
                }
                else
                {
                    model.CStatus = "";
                }

                if (model.BStatus != "Pending")
                {
                    if (model.IsBaselineDataByAdmin)
                    {
                        model.BStatus = "<a data-toggle='tooltip' title=" + _sharedLocalizer["Approved"].Value + " class='btn btn-success btn-sm'> <span class='glyphicon glyphicon-ok' aria-hidden='true'></span></a>";
                    }
                    else
                    {
                        model.BStatus = "<a title=" + _sharedLocalizer["Rejected"].Value + " class='btn btn-danger btn-sm'> <span class='glyphicon glyphicon-remove' aria-hidden='true'></span></a>";
                    }
                }
                else
                {
                    model.BStatus = "";
                }
                model.Action = "<a class='btn btn-default btn-sm' title='" + _sharedLocalizer["View Details"].Value + "' href='" + _global.GetBaseUrl() + "/Prescriber/InformedConsentForm/" + model.Id + "' ><span class='glyphicon glyphicon-list-alt'></span></a> <a class='btn btn-default btn-sm'  title='" + _sharedLocalizer["Download Details"].Value + "' onclick='callExcel(" + model.Id + ");' ><span class='glyphicon glyphicon-download-alt'></span></a >";
                patientList.Add(model);
            }


            var result = patientList.Where(p => (search == null
                || (p.UniqueId != null && p.UniqueId.ToLower().Contains(search.ToLower())
                || p.FirstName != null && p.FirstName.ToLower().Contains(search.ToLower())
                || p.LastName != null && p.LastName.ToString().ToLower().Contains(search.ToLower())
                || p.FStatus != null && p.FStatus.ToString().Contains(search)))
            ).ToList();

            TotalCount = result.Count;

            result = result.Skip(start).Take(length).ToList();

            switch (sortOrder)
            {

                case "UniqueId":
                    result = result.OrderBy(a => a.UniqueId).ToList();
                    break;
                case "FirstName":
                    result = result.OrderBy(a => a.FirstName).ToList();
                    break;
                case "LastName":
                    result = result.OrderBy(a => a.LastName).ToList();
                    break;
                case "FStatus":
                    result = result.OrderBy(a => a.FStatus).ToList();
                    break;
                default:
                    result = result.AsQueryable().ToList();
                    break;
            }
            return result.ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult PrescriberDelete(int id)
        {
            var user = _db.AspNetUsers.Where(o => o.UserId == id).FirstOrDefault();
            user.EmailConfirmed = false;
            user.IsEnabled = false;
            _db.SaveChanges();
            return Json(new { success = true, message = _sharedLocalizer["Deleted Successfully"].Value });

        }

        /// <summary>
        /// Prescriber
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Prescriber()
        {
            return View();
        }


        public void SendEmail(string _email, string _password, string action, string _message, int userId)
        {

            AdminObject adminObj = _adminService.GetAdmin();
            PrescriverObject preObj = _adminService.GetPrescriber(userId);

            EmailHelper email = new EmailHelper();
            string url = _global.GetBaseUrl() + "/Account/Login";
            string emailbody = "";
            switch (action)
            {
                case "ApprovalMessage":
                    emailbody = _messageService.GetApprovalMessage(preObj.FullName, _email, _password, url, adminObj.Email);
                    break;
                case "RejectedMessage":
                    emailbody = _messageService.GetRejectedMessage(preObj.FullName, _email, url, _message, adminObj.Email);
                    break;
                case "ActivatedMessage":
                    emailbody = _messageService.GetActivatedMessage(preObj.FullName, _email, url, adminObj.Email);
                    break;
                case "DeactivatedMessage":
                    emailbody = _messageService.GetDeactivatedMessage(preObj.FullName, _email, url, _message, adminObj.Email);
                    break;
                default:
                    emailbody = _messageService.GetApprovalMessage(preObj.FullName, _email, _password, url, adminObj.Email);
                    break;
            }
            email.SendMail(_email, "", _sharedLocalizer["Your credentials for the Tillomed portal"].Value, emailbody);

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> ApprovedPrescriber(int id)
        {

            //Enable Email and Actived Prescriber
            var user = _db.AspNetUsers.Where(o => o.UserId == id).FirstOrDefault();
            user.EmailConfirmed = true;
            user.IsEnabled = true;
            user.IsRejected = false;
            user.IsDeactivated = false;
            user.RejectionReason = null;
            user.DeactivationReason = null;
            _db.SaveChanges();

            //Send Generated Password
            string password = GenerateRandomPassword();

            //Reset Password 
            ApplicationUser user2 = await _userManager.FindByEmailAsync(user.Email);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user2);
            var result = await _userManager.ResetPasswordAsync(user2, code, password);
            if (result.Succeeded)
            {
                // Send Email to Prescriber
                SendEmail(user.Email, password, "ApprovalMessage", "", user.UserId);

            }

            //Send Success message to User
            return Json(new { success = true, message = _sharedLocalizer["Approved Successfully"].Value });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ActivatedPrescriber(int id)
        {

            //Enable Email and Actived Prescriber
            var user = _db.AspNetUsers.Where(o => o.UserId == id).FirstOrDefault();
            user.EmailConfirmed = true;
            user.IsEnabled = true;
            user.IsRejected = false;
            user.IsDeactivated = false;
            user.RejectionReason = null;
            user.DeactivationReason = null;
            _db.SaveChanges();

            SendEmail(user.Email, "", "ActivatedMessage", "", user.UserId);

            //Send Success message to User
            return Json(new { success = true, message = _sharedLocalizer["Activated Successfully"].Value });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult RejectedPrescriber(int id = 0)
        {
            ReasonViewModel pvm = new ReasonViewModel();
            pvm.Id = id;
            return View(pvm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RejectedPrescriber(ReasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.AspNetUsers.Where(o => o.UserId == model.Id).FirstOrDefault();
                user.EmailConfirmed = false;
                user.IsEnabled = false;
                user.IsRejected = true;
                user.IsDeactivated = false;
                user.RejectionReason = model.Reason;
                user.DeactivationReason = null;
                _db.SaveChanges();

                //Send Email to Prescriber
                SendEmail(user.Email, "", "RejectedMessage", model.Reason, user.UserId);

                return Json(new { success = "success", message = _sharedLocalizer["Rejected Successfully"].Value }, new JsonSerializerSettings());
            }
            return View(model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeactivatedPrescriber(int id = 0)
        {
            DReasonViewModel pvm = new DReasonViewModel();
            pvm.Id = id;
            return View(pvm);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeactivatedPrescriber(DReasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.AspNetUsers.Where(o => o.UserId == model.Id).FirstOrDefault();
                user.EmailConfirmed = true;
                user.IsEnabled = false;
                user.IsRejected = false;
                user.IsDeactivated = true;
                user.RejectionReason = null;
                user.DeactivationReason = model.Reason;
                _db.SaveChanges();

                //Send Email to Prescriber
                SendEmail(user.Email, "", "DeactivatedMessage", model.Reason, user.UserId);

                return Json(new { success = "success", message = _sharedLocalizer["Deactivated Successfully"].Value }, new JsonSerializerSettings());
            }
            return View(model);
        }

    }
}
