using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmcureCERI.Business.Contract.ServiceContracts;
using EmcureCERI.Data.DataAccess.Entities;
using EmcureCERI.Web.Classes;
using EmcureCERI.Web.Helper;
using EmcureCERI.Web.Hubs;
using EmcureCERI.Web.Models;
using EmcureCERI.Web.Models.DRFViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace EmcureCERI.Web.Controllers
{
    [Authorize]
    public class DRFInitializationController : Controller
    {
        //Test
        private readonly IConfiguration _config;
        private readonly IDRFInitialization _drfINI;
        private readonly EmcureCERIDBContext _db;
        private readonly IDRFManufacturing _drfMAN;
        private readonly IDRFSupplyChainManagement _drfSCM;
        private readonly IDRFRA _drfRA;
        private readonly IDRFIP _drfIP ;
        private readonly IDRFFinance _dRFFinance;
        private readonly IDRFFinal _dRFFinal;
        private readonly IDRFMedical _dRFMedical;
        private readonly ISMTPService _sMTPService;
        private readonly IEmailService _emailService;
        IHostingEnvironment _env;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        public DRFInitializationController(IHostingEnvironment env,IConfiguration config, IDRFInitialization INI,IDRFManufacturing MAN,IDRFSupplyChainManagement SCM,IDRFRA RA,IDRFIP IP,IDRFFinance Finance,IDRFFinal Final, IDRFMedical Medical, IHubContext<NotificationHub> notificationHubContext, IEmailService emailService, ISMTPService sMTPService)
        {
            _config = config;
            this._drfINI = INI;
            this._drfMAN = MAN;
            _db = new EmcureCERIDBContext();
            this._drfSCM = SCM;
            this._drfRA = RA;
            this._drfIP = IP;
            this._dRFFinance = Finance;
            this._dRFFinal = Final;
            this._dRFMedical = Medical;
            this._env = env;
            _notificationHubContext = notificationHubContext;
            _emailService = emailService;
            _sMTPService = sMTPService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DRFInitialization()
        {
            return View();
        }

        public IActionResult DRFInitializationApproval(int ID)
        {
           var data= (from i in _db.Tbl_DRF_Initialization
                      join c in _db.Master_Country on i.CountryID equals c.Id
                      join s in _db.Tbl_Master_PIDFStatus on i.StatusID equals s.PidfStatusID
                      join TMF in _db.Tbl_Master_Formulation on i.Form equals TMF.Id
                      join TMS in _db.Tbl_Master_Strength on i.Strength equals TMS.Id
                      join TMP in _db.Tbl_Master_PackSize on i.PackSize equals TMP.Id
                      join TMPSS in _db.Tbl_Master_PackStyle on i.PackStyle equals TMPSS.Id
                      //join TMPM in _db.Tbl_Master_ProductManufacture on i.Plant equals TMPM.Id
                      from TMPM in _db.Tbl_Master_ProductManufacture.Where(o => o.Id == i.Plant).DefaultIfEmpty()
                      join TMM in _db.Tbl_Master_ModeofFeesPayment on i.ModeOfFeesPayment equals TMM.Id
                      join TMMS in _db.Tbl_Master_Modeofshipment on i.ModeOfShipment equals TMMS.Id
                      //join TMDT in _db.Tbl_Master_DossierTemplate on i.DossierTemplateID equals TMDT.Id
                      from TMDT in _db.Tbl_Master_DossierTemplate.Where(n => n.Id == i.DossierTemplateID).DefaultIfEmpty()
                      join TMI in _db.Tbl_Master_Incoterms on i.Incoterms equals TMI.Id
                      where i.InitializationID== ID
             select new
             {
                   i.InitializationID
                  ,i.CountryID
                  ,c.Country
                  ,i.GenericName
                  ,i.BrandName
                  ,i.TreadmarkApprovedInternal
                  ,i.TreadmarkSuggestedInternal
                  ,i.TreadmarkOwnerInternal
                  ,//i.Form
                 Form = TMF.Formulation
                             ,
                 //i.Strength
                 Strength = TMS.Strength
                             ,
                 //i.PackSize
                 PackSize = TMP.PackSize
                             ,
                 //i.PackStyle
                 PackStyle = TMPSS.PackStyle
                             ,
                 //i.Plant
                 Plant = TMPM.ProductManufacture
                  ,
                 i.RegistrationFees
                  ,i.FeesToBePaidBy
                  ,//i.ModeOfFeesPayment
                 ModeOfFeesPayment = TMM.ModeofFeesPayment
                  ,
                 i.MAHolder
                  ,i.ProposedMarketingStatus
                  ,i.ShippingPort
                  ,//i.ModeOfShipment
                 ModeOfShipment = TMMS.Modeofshipment
                             ,
                 //i.Incoterms
                 Incoterms = TMI.Incoterms
                  ,
                 i.DossierSubmittedToMOHBy
                  ,i.OwnerOfRegistration
                  ,i.AvailabilityofCDA
                  ,i.MarketSize
                  ,i.ThreeYearCAGR
                  ,i.NumberOfCurrentPlayer
                  ,i.InnovatorBrand
                  ,i.FirstBrand
                  ,i.SecondBrand
                  ,i.ThirdBrand
                  ,i.ExpectedMarketValueGrowth
                  ,i.InnavotorName
                  ,i.MSFirstBrand
                  ,i.MSSecondBrand
                  ,i.MSThirdBrand
                  ,i.Partner
                  ,i.FirstYearForecastUnitsPacks
                  ,i.SecondYearForecastUnitsPacks
                  ,i.ThirdYearForecastUnitsPacks
                  ,i.FirstYearForecastPriceToPatient
                  ,i.SecondYearForecastPriceToPatient
                  ,i.ThirdYearForecastPriceToPatient
                  ,i.FirstYearForecastCIF
                  ,i.SecondYearForecastCIF
                  ,i.ThirdYearForecastCIF
                  ,i.FirstYearForecastValue
                  ,i.SecondYearForecastValue
                  ,i.ThirdYearForecastValue
                  ,i.OrderFrequency
                  ,i.NameDossierSend
                  ,i.AddressDossierSend
                  ,i.StrategyAlignment
                  ,i.ExceptionExplained
                  ,i.StatusID
                  ,s.PidfStatus
                   ,
                 i.DossierTemplateID
                    ,
                 TMDT.DossierTemplate,
                 i.Currency
                 ,i.TSExcecuted
                 ,i.DAExcecuted
                 ,i.EmailDossierSend
                 ,i.PhoneDossierSend
                 ,i.IsSamples_Required
                 ,i.Samples_Required
                 ,i.Remark
             }).FirstOrDefault();

            var AttachedPIDFID = (from i in _db.Tbl_DRF_Initialization
                                  join p in _db.Tbl_DRF_PIDF_Mapping on i.InitializationID equals p.DRFID
                                  where i.InitializationID == ID
                                  select new { 
                                  
                                      p.ID
                                  
                                  }).FirstOrDefault();

            if(AttachedPIDFID==null)
            {
                @ViewBag.IniAttachedPIDF = false;
            }
            else
            {
                @ViewBag.IniAttachedPIDF = true;
            }

            @ViewBag.IniInitializationID = data.InitializationID;
            @ViewBag.IniCountryID = data.CountryID;
            @ViewBag.IniCountry = data.Country;
            @ViewBag.IniGenericName = data.GenericName;
            @ViewBag.IniBrandName = data.BrandName;
            @ViewBag.IniTreadmarkApprovedInternal = data.TreadmarkApprovedInternal;
            @ViewBag.IniTreadmarkSuggestedInternal = data.TreadmarkSuggestedInternal;
            @ViewBag.IniTreadmarkOwnerInternal = data.TreadmarkOwnerInternal;
            @ViewBag.IniForm = data.Form;
            @ViewBag.IniStrength = data.Strength;
            @ViewBag.IniPackSize = data.PackSize;
            @ViewBag.IniPackStyle = data.PackStyle;
            @ViewBag.IniPlant = data.Plant;
            @ViewBag.IniCurrency = data.Currency;
            @ViewBag.IniRegistrationFees = data.RegistrationFees;
            @ViewBag.IniFeesToBePaidBy = data.FeesToBePaidBy;
            @ViewBag.IniModeOfFeesPayment = data.ModeOfFeesPayment;
            @ViewBag.IniMAHolder = data.MAHolder;
            @ViewBag.IniProposedMarketingStatus = data.ProposedMarketingStatus;
            @ViewBag.IniShippingPort = data.ShippingPort;
            @ViewBag.IniModeOfShipment = data.ModeOfShipment;
            @ViewBag.IniIncoterms = data.Incoterms;
            @ViewBag.IniDossierSubmittedToMOHBy = data.DossierSubmittedToMOHBy;
            @ViewBag.IniOwnerOfRegistration = data.OwnerOfRegistration;
            @ViewBag.IniAvailabilityofCDA = data.AvailabilityofCDA;
            @ViewBag.IniMarketSize = data.MarketSize;
            @ViewBag.IniThreeYearCAGR = data.ThreeYearCAGR;
            @ViewBag.IniNumberOfCurrentPlayer = data.NumberOfCurrentPlayer;
            @ViewBag.IniInnovatorBrand = data.InnovatorBrand;
            @ViewBag.IniFirstBrand = data.FirstBrand;
            @ViewBag.IniSecondBrand = data.SecondBrand;
            @ViewBag.IniThirdBrand = data.ThirdBrand;
            @ViewBag.IniExpectedMarketValueGrowth = data.ExpectedMarketValueGrowth;
            @ViewBag.IniInnavotorName = data.InnavotorName;
            @ViewBag.IniMSFirstBrand = data.MSFirstBrand;
            @ViewBag.IniMSSecondBrand = data.MSSecondBrand;
            @ViewBag.IniMSThirdBrand = data.MSThirdBrand;
            @ViewBag.IniPartner = data.Partner;
            @ViewBag.IniFirstYearForecastUnitsPacks = data.FirstYearForecastUnitsPacks;
            @ViewBag.IniSecondYearForecastUnitsPacks = data.SecondYearForecastUnitsPacks;
            @ViewBag.IniThirdYearForecastUnitsPacks = data.ThirdYearForecastUnitsPacks;
            @ViewBag.IniFirstYearForecastPriceToPatient = data.FirstYearForecastPriceToPatient;
            @ViewBag.IniSecondYearForecastPriceToPatient = data.SecondYearForecastPriceToPatient;
            @ViewBag.IniThirdYearForecastPriceToPatient = data.ThirdYearForecastPriceToPatient;
            @ViewBag.IniFirstYearForecastCIF = data.FirstYearForecastCIF;
            @ViewBag.IniSecondYearForecastCIF = data.SecondYearForecastCIF;
            @ViewBag.IniThirdYearForecastCIF = data.ThirdYearForecastCIF;
            @ViewBag.IniFirstYearForecastValue = data.FirstYearForecastValue;
            @ViewBag.IniSecondYearForecastValue = data.SecondYearForecastValue;
            @ViewBag.IniThirdYearForecastValue = data.ThirdYearForecastValue;
            @ViewBag.IniOrderFrequency = data.OrderFrequency;
            @ViewBag.IniNameDossierSend = data.NameDossierSend;
            @ViewBag.IniAddressDossierSend = data.AddressDossierSend;
            @ViewBag.IniStrategyAlignment = data.StrategyAlignment;
            @ViewBag.IniExceptionExplained = data.ExceptionExplained;
            @ViewBag.IniDossierTemplate = data.DossierTemplate;
            @ViewBag.IniTSExcecuted = data.TSExcecuted;
            @ViewBag.IniDAExcecuted = data.DAExcecuted;
            @ViewBag.IniEmailDossierSend = data.EmailDossierSend;
            @ViewBag.IniPhoneDossierSend = data.PhoneDossierSend;
            if(data.IsSamples_Required == true)
            {
                @ViewBag.IniSamplesRequired = data.Samples_Required;
            }
            else
            {
                @ViewBag.IniSamplesRequired = "No";
            }
            //@ViewBag.IniSamplesRequired = data.Samples_Required;
            @ViewBag.IniRemark = data.Remark;

            // @ViewBag.Ini = data.StatusID;
            // @ViewBag.Ini = data.PidfStatus; 

            return View();
        }
        public IActionResult DRFAddDetails(int ID)
        {
            HttpContext.Session.SetString("UpdationInitializationID", Convert.ToString(ID));
            var data = (from i in _db.Tbl_DRF_Initialization
                        join c in _db.Master_Country on i.CountryID equals c.Id
                        join s in _db.Tbl_Master_PIDFStatus on i.StatusID equals s.PidfStatusID
                        join TMF in _db.Tbl_Master_Formulation on i.Form equals TMF.Id
                        join TMS in _db.Tbl_Master_Strength on i.Strength equals TMS.Id
                        join TMP in _db.Tbl_Master_PackSize on i.PackSize equals TMP.Id
                        join TMPSS in _db.Tbl_Master_PackStyle on i.PackStyle equals TMPSS.Id
                        join TMPT in _db.Tbl_Master_ProductType on i.ProductTypeId equals TMPT.Id
                        //join TMPM in _db.Tbl_Master_ProductManufacture on i.Plant equals TMPM.Id
                        from TMPM in _db.Tbl_Master_ProductManufacture.Where(o => o.Id == i.Plant).DefaultIfEmpty()
                            //join TMM in _db.Tbl_Master_ModeofFeesPayment on i.ModeOfFeesPayment equals TMM.Id
                        from TMM in _db.Tbl_Master_ModeofFeesPayment.Where(x => x.Id == i.ModeOfFeesPayment).DefaultIfEmpty()
                            //join TMMS in _db.Tbl_Master_Modeofshipment on i.ModeOfShipment equals TMMS.Id
                        from TMMS in _db.Tbl_Master_Modeofshipment.Where(x => x.Id == i.ModeOfShipment).DefaultIfEmpty()
                            //join TMDT in _db.Tbl_Master_DossierTemplate on i.DossierTemplateID equals TMDT.Id
                        from TMDT in _db.Tbl_Master_DossierTemplate.Where(n => n.Id == i.DossierTemplateID).DefaultIfEmpty()
                            //join TMI in _db.Tbl_Master_Incoterms on i.Incoterms equals TMI.Id
                        from TMI in _db.Tbl_Master_Incoterms.Where(x => x.Id == i.Incoterms).DefaultIfEmpty()
                        where i.InitializationID == ID
                        select new
                        {
                            i.InitializationID
                             ,
                            i.CountryID
                             ,
                            c.Country
                             ,
                            i.GenericName
                             ,
                            i.BrandName
                             ,
                            i.TreadmarkApprovedInternal
                             ,
                            i.TreadmarkSuggestedInternal
                             ,
                            i.TreadmarkOwnerInternal
                             ,
                            //i.Form
                            Form = TMF.Formulation
                             ,
                            //i.Strength
                            Strength = TMS.Strength
                             ,
                            //i.PackSize
                            PackSize = TMP.PackSize
                             ,
                            //i.PackStyle
                            PackStyle = TMPSS.PackStyle
                             ,
                            //i.Plant
                            Plant = TMPM.ProductManufacture,
                            ProductTypeID = i.ProductTypeId,
                            ProductType = TMPT.ProductType
                             ,
                            i.RegistrationFees
                             ,
                            i.FeesToBePaidBy
                             ,
                            //i.ModeOfFeesPayment
                            ModeOfFeesPayment = TMM.ModeofFeesPayment
                             ,
                            i.MAHolder
                             ,
                            i.ProposedMarketingStatus
                             ,
                            i.ShippingPort
                             ,
                            //i.ModeOfShipment
                            ModeOfShipment = TMMS.Modeofshipment
                             ,
                            //i.Incoterms
                            Incoterms = TMI.Incoterms
                             ,
                            i.DossierSubmittedToMOHBy
                             ,
                            i.OwnerOfRegistration
                            ,
                            i.AvailabilityofCDA
                             ,
                            i.MarketSize
                             ,
                            i.ThreeYearCAGR
                             ,
                            i.NumberOfCurrentPlayer
                             ,
                            i.InnovatorBrand
                             ,
                            i.FirstBrand
                             ,
                            i.SecondBrand
                             ,
                            i.ThirdBrand
                             ,
                            i.ExpectedMarketValueGrowth
                             ,
                            i.InnavotorName
                             ,
                            i.MSFirstBrand
                             ,
                            i.MSSecondBrand
                             ,
                            i.MSThirdBrand
                             ,
                            i.Partner
                             ,
                            i.FirstYearForecastUnitsPacks
                             ,
                            i.SecondYearForecastUnitsPacks
                             ,
                            i.ThirdYearForecastUnitsPacks
                             ,
                            i.FirstYearForecastPriceToPatient
                             ,
                            i.SecondYearForecastPriceToPatient
                             ,
                            i.ThirdYearForecastPriceToPatient
                             ,
                            i.FirstYearAPIQuantity
                             ,
                            i.SecondYearAPIQuantity
                             ,
                            i.ThirdYearAPIQuantity
                             ,
                            i.FirstYearForecastCIF
                             ,
                            i.SecondYearForecastCIF
                             ,
                            i.ThirdYearForecastCIF
                             ,
                            i.FirstYearForecastValue
                             ,
                            i.SecondYearForecastValue
                             ,
                            i.ThirdYearForecastValue
                             ,
                            i.OrderFrequency
                             ,
                            i.NameDossierSend
                             ,
                            i.AddressDossierSend
                             ,
                            i.StrategyAlignment
                             ,
                            i.ExceptionExplained
                             ,
                            i.StatusID
                             ,
                            s.PidfStatus
                            ,
                            i.InitialApproveRejectComment
                             ,
                            i.DossierTemplateID,
                            i.IsSamples_Required,
                            i.Samples_Required,
                            i.Remark,
                            i.NoofShipmnets,
                            TMDT.DossierTemplate,
                            i.Currency, i.TSExcecuted, i.DAExcecuted, i.EmailDossierSend, i.PhoneDossierSend, IPProjectName = i.GenericName + " " + TMS.Strength
                        }).FirstOrDefault();

            var AttachedPIDFID = (from i in _db.Tbl_DRF_Initialization
                                  join p in _db.Tbl_DRF_PIDF_Mapping on i.InitializationID equals p.DRFID
                                  where i.InitializationID == ID
                                  select new
                                  {
                                      p.ID

                                  }).FirstOrDefault();

            if (AttachedPIDFID == null)
            {
                @ViewBag.DetAttachedPIDF = false;
            }
            else
            {
                @ViewBag.DetAttachedPIDF = true;
            }

            @ViewBag.DetInitializationID = data.InitializationID;
            @ViewBag.DetCountryID = data.CountryID;
            @ViewBag.DetCountry = data.Country;
            @ViewBag.DetGenericName = data.GenericName;
            @ViewBag.DetBrandName = data.BrandName;
            @ViewBag.DetGenericNameIP = data.IPProjectName;
            @ViewBag.DetBrandNameIP = data.BrandName;
            @ViewBag.DetTreadmarkApprovedInternal = data.TreadmarkApprovedInternal;
            @ViewBag.DetTreadmarkSuggestedInternal = data.TreadmarkSuggestedInternal;
            @ViewBag.DetTreadmarkOwnerInternal = data.TreadmarkOwnerInternal;
            @ViewBag.DetForm = data.Form;
            @ViewBag.DetStrength = data.Strength;
            @ViewBag.DetPackSize = data.PackSize;
            @ViewBag.DetPackStyle = data.PackStyle;
            @ViewBag.DetPlant = data.Plant;
            @ViewBag.DetProductType = data.ProductType;
            @ViewBag.DetCurrency = data.Currency;
            @ViewBag.DetRegistrationFees = data.RegistrationFees;
            @ViewBag.DetFeesToBePaidBy = data.FeesToBePaidBy;
            @ViewBag.DetModeOfFeesPayment = data.ModeOfFeesPayment;
            @ViewBag.DetMAHolder = data.MAHolder;
            @ViewBag.DetProposedMarketingStatus = data.ProposedMarketingStatus;
            @ViewBag.DetShippingPort = data.ShippingPort;
            @ViewBag.DetModeOfShipment = data.ModeOfShipment;
            @ViewBag.DetIncoterms = data.Incoterms;
            @ViewBag.DetDossierSubmittedToMOHBy = data.DossierSubmittedToMOHBy;
            @ViewBag.DetOwnerOfRegistration = data.OwnerOfRegistration;
            @ViewBag.DetAvailabilityofCDA = data.AvailabilityofCDA;
            @ViewBag.DetMarketSize = data.MarketSize;
            @ViewBag.DetThreeYearCAGR = data.ThreeYearCAGR;
            @ViewBag.DetNumberOfCurrentPlayer = data.NumberOfCurrentPlayer;
            @ViewBag.DetInnovatorBrand = data.InnovatorBrand;
            @ViewBag.DetFirstBrand = data.FirstBrand;
            @ViewBag.DetSecondBrand = data.SecondBrand;
            @ViewBag.DetThirdBrand = data.ThirdBrand;
            @ViewBag.DetExpectedMarketValueGrowth = data.ExpectedMarketValueGrowth;
            @ViewBag.DetInnavotorName = data.InnavotorName;
            @ViewBag.DetMSFirstBrand = data.MSFirstBrand;
            @ViewBag.DetMSSecondBrand = data.MSSecondBrand;
            @ViewBag.DetMSThirdBrand = data.MSThirdBrand;
            @ViewBag.DetPartner = data.Partner;
            @ViewBag.DetFirstYearForecastUnitsPacks = data.FirstYearForecastUnitsPacks;
            @ViewBag.DetSecondYearForecastUnitsPacks = data.SecondYearForecastUnitsPacks;
            @ViewBag.DetThirdYearForecastUnitsPacks = data.ThirdYearForecastUnitsPacks;
            @ViewBag.DetFirstYearForecastPriceToPatient = data.FirstYearForecastPriceToPatient;
            @ViewBag.DetSecondYearForecastPriceToPatient = data.SecondYearForecastPriceToPatient;
            @ViewBag.DetThirdYearForecastPriceToPatient = data.ThirdYearForecastPriceToPatient;
            @ViewBag.DetFirstYearAPIQuantity = data.FirstYearAPIQuantity;
            @ViewBag.DetSecondYearAPIQuantity = data.SecondYearAPIQuantity;
            @ViewBag.DetThirdYearAPIQuantity = data.ThirdYearAPIQuantity;
            @ViewBag.DetFirstYearForecastCIF = data.FirstYearForecastCIF;
            @ViewBag.DetSecondYearForecastCIF = data.SecondYearForecastCIF;
            @ViewBag.DetThirdYearForecastCIF = data.ThirdYearForecastCIF;
            @ViewBag.DetFirstYearForecastValue = data.FirstYearForecastValue;
            @ViewBag.DetSecondYearForecastValue = data.SecondYearForecastValue;
            @ViewBag.DetThirdYearForecastValue = data.ThirdYearForecastValue;
            @ViewBag.DetOrderFrequency = data.OrderFrequency;
            @ViewBag.DetNoofShipments = data.NoofShipmnets;
            @ViewBag.DetNameDossierSend = data.NameDossierSend;
            @ViewBag.DetAddressDossierSend = data.AddressDossierSend;
            @ViewBag.DetStrategyAlignment = data.StrategyAlignment;
            @ViewBag.DetExceptionExplained = data.ExceptionExplained;
            @ViewBag.DetInitialApproveRejectComment = data.InitialApproveRejectComment;
            @ViewBag.DetDossierTemplate = data.DossierTemplate;
            @ViewBag.DetTSExcecuted = data.TSExcecuted;
            @ViewBag.DetDAExcecuted = data.DAExcecuted;
            @ViewBag.DetEmailDossierSend = data.EmailDossierSend;
            @ViewBag.DetPhoneDossierSend = data.PhoneDossierSend;
            if(data.IsSamples_Required == true)
            {
                @ViewBag.DetSamplesRequired = data.Samples_Required;
            }
            else
            {
                @ViewBag.DetSamplesRequired = "No";
            }
            //@ViewBag.SamplesRequired = data.Samples_Required;
            @ViewBag.Remark = data.Remark;
            return View();
        }

        public ActionResult ProjectList()
        {
            return View();
        }
        public ActionResult ProjectShowDetails(int Id)
        {
            @ViewBag.ProjectInitializationID = Id;
            HttpContext.Session.SetString("Action", "DRF");
            HttpContext.Session.SetString("ListPageURL", "/DRFInitialization/ProjectList");
            HttpContext.Session.SetString("DetailsPageURL", "/DRFInitialization/ProjectShowDetails");
            HttpContext.Session.SetString("ButtonDetailPage", "Dossier Details");
            HttpContext.Session.SetString("ButtonListPage", "Dossier List");

            @ViewBag.CurrentUserRole = HttpContext.Session.GetString("CurrentUserRole");
            
            return View();
        }

        public ActionResult RejectionDetails(int Id)
        {
            @ViewBag.RejectProjectInitializationID = Id;
            //HttpContext.Session.SetString("Action", "DRF");
            //HttpContext.Session.SetString("ListPageURL", "/DRFInitialization/ProjectList");
            //HttpContext.Session.SetString("DetailsPageURL", "/DRFInitialization/ProjectShowDetails");
            //HttpContext.Session.SetString("ButtonDetailPage", "Dossier Details");
            //HttpContext.Session.SetString("ButtonListPage", "Dossier List");

            @ViewBag.RejectCurrentUserRole = HttpContext.Session.GetString("CurrentUserRole");

            return View();
        }

        public ActionResult DRFShowDetails(int Id)
        {
            @ViewBag.DRFShowDetailsInitializationID = Id;
            //HttpContext.Session.SetString("Action", "DRF");
            //HttpContext.Session.SetString("ListPageURL", "/DRFInitialization/ProjectList");
            //HttpContext.Session.SetString("DetailsPageURL", "/DRFInitialization/ProjectShowDetails");
            //HttpContext.Session.SetString("ButtonDetailPage", "Dossier Details");
            //HttpContext.Session.SetString("ButtonListPage", "Dossier List");

            return View();
        }

        [Authorize(Roles = "Prescriber,ManufacturingTeam,Manufacturing Manager")]
        [HttpPost]
        [ActionName("InsertDRFManufacturingDetails")]
        [Obsolete]
        public ActionResult InsertDRFManufacturingDetails(DRFManufacturingModel dRFManufacturingModel)
        {
            
            var alreadyExists = _db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == dRFManufacturingModel.InitializationID).Select(t => t.ManufacturingId).FirstOrDefault();

            alreadyExists = alreadyExists == null ? 0 : alreadyExists;

            if (alreadyExists > 0)
            {
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {

                if (ModelState.IsValid)
                {
                    int tempInitializationID = dRFManufacturingModel.InitializationID;
                    Tbl_DRF_Manufacturing tbl_DRF_Manufacturing = new Tbl_DRF_Manufacturing();
                    tbl_DRF_Manufacturing.InitializationId = dRFManufacturingModel.InitializationID;
                    tbl_DRF_Manufacturing.ManufacturingSiteId = dRFManufacturingModel.ManufacturingSiteId;
                    tbl_DRF_Manufacturing.ManufacturingSiteName = dRFManufacturingModel.ManufacturingSiteName;
                    tbl_DRF_Manufacturing.APIId = dRFManufacturingModel.APIId;
                    tbl_DRF_Manufacturing.APISiteName = dRFManufacturingModel.APISiteName;
                    tbl_DRF_Manufacturing.Batchsize = dRFManufacturingModel.Batchsize;
                    tbl_DRF_Manufacturing.Leadtime = dRFManufacturingModel.Leadtime;
                    tbl_DRF_Manufacturing.UnitEXW = dRFManufacturingModel.UnitEXW;
                    tbl_DRF_Manufacturing.ArtworkTypeId = dRFManufacturingModel.ArtworkTypeId;
                    //tbl_DRF_Manufacturing.TentativeSchedule = dRFManufacturingModel.TentativeSchedule;
                    tbl_DRF_Manufacturing.Tentative_Artwork_Lead_Time = dRFManufacturingModel.Tentative_Artwork_Lead_Time;
                    tbl_DRF_Manufacturing.PackorShipper = dRFManufacturingModel.PackorShipper;
                    tbl_DRF_Manufacturing.GrossWeight = dRFManufacturingModel.GrossWeight;
                    tbl_DRF_Manufacturing.Dimensions = dRFManufacturingModel.Dimensions;
                    tbl_DRF_Manufacturing.MWidth = dRFManufacturingModel.MWidth;
                    tbl_DRF_Manufacturing.MHeight = dRFManufacturingModel.MHeight;
                    tbl_DRF_Manufacturing.MLength = dRFManufacturingModel.MLength;

                    tbl_DRF_Manufacturing.Remark = dRFManufacturingModel.Remark;
                    tbl_DRF_Manufacturing.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_DRF_Manufacturing.CreatedDate = DateTime.Today;

                    var DRFCompanyID = _db.Tbl_DRF_Initialization
                                       .AsNoTracking()
                                       .Where(x => x.InitializationID == dRFManufacturingModel.InitializationID)
                                       .Select(x => x.CompanyID).FirstOrDefault();

                    int data = _drfMAN.insertDRFManufacturing(tbl_DRF_Manufacturing);

                    List<Tbl_DRF_Manufacturing_APISite> manufacturingAPISiteList = new List<Tbl_DRF_Manufacturing_APISite>();

                    if (dRFManufacturingModel.ManufacturingAPISiteList.Length > 0)
                    {
                        List<Tbl_DRF_Manufacturing_APISite> List = JsonConvert.DeserializeObject<List<Tbl_DRF_Manufacturing_APISite>>(dRFManufacturingModel.ManufacturingAPISiteList);

                        for (int i = 0; i < List.Count; i++)
                        {
                            Tbl_DRF_Manufacturing_APISite APISite = new Tbl_DRF_Manufacturing_APISite();
                            APISite.ManufacturingSiteId = data;
                            APISite.MAPIID = 0;
                            APISite.APIId = List[i].APIId;
                            APISite.APISiteName = List[i].APISiteName;
                            APISite.APIName = List[i].APIName;
                            APISite.IsActive = 1;
                            APISite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            APISite.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            manufacturingAPISiteList.Add(APISite);
                        }

                        int count = _drfMAN.insertDRFManufacturingAPISite(manufacturingAPISiteList);

                    }

                    HttpContext.Session.SetString("CurrentMoleculeCompanyID", DRFCompanyID.ToString());

                    if (data > 0)
                    {
                        ModelState.Clear();
                        SendEmailForDRFDetails(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), tempInitializationID,"Manufacturing Details", "Manufacturing Team");
                                               

                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }

                }

                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }
        }


       // [Authorize(Roles = "Prescriber")]
       [Authorize]
        [HttpPost]
        [ActionName("GetDropdownsForDRFAddDetails")]
        public ActionResult GetDropdownsForDRFAddDetails()
        {

            DRFAddDetailsModel dRFAddDetailsModel = new DRFAddDetailsModel();
          
            List<ManufacturingSiteList> manufacturingSiteLists = new List<ManufacturingSiteList>();

            var tempManufacturingSiteList= (from TMMS in _db.Tbl_Master_ManufacturingSite
                                            select new
                                            {
                                                TMMS.ManufacturingSiteID,
                                                TMMS.ManufacturingSite
                                            }).ToList();


            foreach (var data in tempManufacturingSiteList)
            {
                //ManufacturingSiteList temp = new ManufacturingSiteList();
                //temp.ManufacturingSiteID = data.ManufacturingSiteID;
                //temp.ManufacturingSite = data.ManufacturingSite;
                // manufacturingSiteLists.Add(temp);
                manufacturingSiteLists.Add(new ManufacturingSiteList {  ManufacturingSiteID=data.ManufacturingSiteID,ManufacturingSite=data.ManufacturingSite});

            }


            dRFAddDetailsModel.DRFManufacturingModel.ManufacturingSiteList = manufacturingSiteLists;

            List<APISiteList> aPISiteLists = new List<APISiteList>();

            var tempSPISiteList = (from TMA in _db.Tbl_Master_APISite
                                             select new
                                             {
                                                 TMA.APIID,
                                                 TMA.APISite
                                             }).ToList();


            foreach (var data in tempSPISiteList)
            {
                APISiteList temp = new APISiteList();
                temp.APIID = data.APIID;
                temp.APISite = data.APISite;
                aPISiteLists.Add(temp);
            }

            dRFAddDetailsModel.DRFManufacturingModel.APISiteList = aPISiteLists;

            List<ArtWorkTypeList> artWorkTypeLists = new List<ArtWorkTypeList>();

            var tempArtworkTypeList = (from TMAT in _db.Tbl_Master_ArtworkType
                                   select new
                                   {
                                       TMAT.ArtworkTypeId,
                                       TMAT.ArtworkTypeName
                                   }).ToList();


            foreach (var data in tempArtworkTypeList)
            {
                ArtWorkTypeList temp = new ArtWorkTypeList();
                temp.ArtworkTypeId = data.ArtworkTypeId;
                temp.ArtworkTypeName = data.ArtworkTypeName;
                artWorkTypeLists.Add(temp);
            }

            dRFAddDetailsModel.DRFManufacturingModel.ArtWorkTypeList = artWorkTypeLists;


            List<GMPAvailabilityList> gMPAvailabilityLists = new List<GMPAvailabilityList>();

            var tempGMPAvailabilityList = (from TMG in _db.Tbl_Master_GMPAvailability
                                             select new
                                             {
                                                 TMG.GMPAvailabilityID,
                                                 TMG.GMPAvailability
                                             }).ToList();


            foreach (var data in tempGMPAvailabilityList)
            {

                gMPAvailabilityLists.Add(new GMPAvailabilityList { GMPAvailabilityID = data.GMPAvailabilityID, GMPAvailability = data.GMPAvailability });

            }


            dRFAddDetailsModel.DRFRAModel.GMPAvailabilityList = gMPAvailabilityLists;


            List<CurrencyList> currencyLists = new List<CurrencyList>();

            var tempCurrencyList = (from TMC in _db.Tbl_Master_Currency
                                           select new
                                           {
                                               TMC.CurrencyID,
                                               TMC.Currency
                                           }).ToList();


            foreach (var data in tempCurrencyList)
            {

                currencyLists.Add(new CurrencyList { CurrencyID = data.CurrencyID, Currency = data.Currency });

            }

            dRFAddDetailsModel.DRFRAModel.CurrencyList = currencyLists;

            List<DossierTemplateList> dossierTemplateLists = new List<DossierTemplateList>();

            var tempDossierTemplateList = _db.Tbl_Master_DossierTemplate
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.DossierTemplate)
                .Select(x => new { x.Id, x.DossierTemplate });


            foreach (var data in tempDossierTemplateList)
            {

                dossierTemplateLists.Add(new DossierTemplateList { DossierTemplateID = data.Id, DossierTemplate = data.DossierTemplate });

            }

            dRFAddDetailsModel.DRFRAModel.DossierTemplateList = dossierTemplateLists;



            return Json(new { success = true, data = dRFAddDetailsModel });
            
        }


        [Authorize(Roles = "Prescriber,SupplyChainManagement,SCM Manager")]
        [HttpPost]
        [ActionName("InsertDRFSCMDetails")]
        [Obsolete]
        public ActionResult InsertDRFSCMDetails(DRFSCMModel dRFSCMModel)
        {
            var alreadyExists = _db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == dRFSCMModel.InitializationID).Select(t =>t.SCMId).FirstOrDefault();

           alreadyExists = alreadyExists == null ? 0: alreadyExists;

            if (alreadyExists > 0)
            {
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                if (ModelState.IsValid)
                {
                    int tempInitializationID = dRFSCMModel.InitializationID;
                    Tbl_DRF_SupplyChainMgmt tbl_DRF_SupplyChainMgmt = new Tbl_DRF_SupplyChainMgmt();
                    tbl_DRF_SupplyChainMgmt.InitializationId = dRFSCMModel.InitializationID;
                    tbl_DRF_SupplyChainMgmt.FreightCost = dRFSCMModel.FreightCost;
                    tbl_DRF_SupplyChainMgmt.TentativeShipmente = dRFSCMModel.TentativeShipmente;
                    tbl_DRF_SupplyChainMgmt.TentativeDestination = dRFSCMModel.TentativeDestination;
                    tbl_DRF_SupplyChainMgmt.Remark = dRFSCMModel.Remark;
                    tbl_DRF_SupplyChainMgmt.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_DRF_SupplyChainMgmt.CreatedDate = DateTime.Today;

                    int data = _drfSCM.insertDRFSCM(tbl_DRF_SupplyChainMgmt);

                    var DRFCompanyID = _db.Tbl_DRF_Initialization
                                      .AsNoTracking()
                                      .Where(x => x.InitializationID == dRFSCMModel.InitializationID)
                                      .Select(x => x.CompanyID).FirstOrDefault();

                    HttpContext.Session.SetString("CurrentMoleculeCompanyID", DRFCompanyID.ToString());

                    if (data == 1)
                    {
                        ModelState.Clear();
                        SendEmailForDRFDetails(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), tempInitializationID, "SCM Details", "SCM Team");
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }

                }

                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }
        }

        [Authorize(Roles = "Prescriber,RegulatoryTeam,Regulatory Manager")]
        [HttpPost]
        [ActionName("InsertDRFRADetails")]
        [Obsolete]
        public ActionResult InsertDRFRADetails(DRFRAModel dRFRAModel)
        {
            var alreadyExists = _db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == dRFRAModel.InitializationID).Select(t => t.RAInfoId).FirstOrDefault();

            alreadyExists = alreadyExists == null ? 0 : alreadyExists;

            if (alreadyExists > 0)
            {
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {

                if (ModelState.IsValid)
                {
                    int tempInitializationID = dRFRAModel.InitializationID;
                    Tbl_DRF_Requisite_RAInfo tbl_DRF_Requisite_RAInfo = new Tbl_DRF_Requisite_RAInfo();
                    tbl_DRF_Requisite_RAInfo.InitializationId = dRFRAModel.InitializationID;
                    tbl_DRF_Requisite_RAInfo.ACC = dRFRAModel.ACC;
                    tbl_DRF_Requisite_RAInfo.ZoneII = dRFRAModel.ZoneII;
                    tbl_DRF_Requisite_RAInfo.Ivbdata = dRFRAModel.Ivbdata;
                    tbl_DRF_Requisite_RAInfo.ProtocolAvailability = dRFRAModel.ProtocolAvailability;
                    tbl_DRF_Requisite_RAInfo.COPPAvailability = dRFRAModel.COPPAvailability;
                    tbl_DRF_Requisite_RAInfo.GMPAvailabilityId = dRFRAModel.GMPAvailabilityId;
                    tbl_DRF_Requisite_RAInfo.GMPAvailability = dRFRAModel.GMPAvailability;
                    tbl_DRF_Requisite_RAInfo.MfgLicense = dRFRAModel.MfgLicense;
                    tbl_DRF_Requisite_RAInfo.PlantInspection = dRFRAModel.PlantInspection;
                    tbl_DRF_Requisite_RAInfo.ValidationBatches = dRFRAModel.ValidationBatches;
                    tbl_DRF_Requisite_RAInfo.COAAvailability = dRFRAModel.COAAvailability;
                    tbl_DRF_Requisite_RAInfo.BEAvailability = dRFRAModel.BEAvailability;
                    tbl_DRF_Requisite_RAInfo.APIDMFstatus = dRFRAModel.APIDMFstatus;
                    tbl_DRF_Requisite_RAInfo.PlantApproval = dRFRAModel.PlantApproval;
                    tbl_DRF_Requisite_RAInfo.PlantApprovalIfYes = dRFRAModel.PlantApprovalIfYes;
                    tbl_DRF_Requisite_RAInfo.RegistrationValidity = dRFRAModel.RegistrationValidity;
                    tbl_DRF_Requisite_RAInfo.Timefordossierpreparation = dRFRAModel.Timefordossierpreparation;
                    tbl_DRF_Requisite_RAInfo.AMV = dRFRAModel.AMV;
                    tbl_DRF_Requisite_RAInfo.PDR = dRFRAModel.PDR;
                    tbl_DRF_Requisite_RAInfo.SamplesAvailability = dRFRAModel.SamplesAvailability;
                    tbl_DRF_Requisite_RAInfo.ImportPermit = dRFRAModel.ImportPermit;
                    tbl_DRF_Requisite_RAInfo.BrandNameApproval = dRFRAModel.BrandNameApproval;
                    tbl_DRF_Requisite_RAInfo.AvailabilityofCDA = dRFRAModel.AvailabilityofCDA;
                    tbl_DRF_Requisite_RAInfo.CurrencyID = dRFRAModel.CurrencyID;
                    tbl_DRF_Requisite_RAInfo.ProductRegistrationFee = dRFRAModel.ProductRegistrationFee;
                    tbl_DRF_Requisite_RAInfo.ComparativeDissolutionProfileData = dRFRAModel.ComparativeDissolutionProfileData;
                    tbl_DRF_Requisite_RAInfo.Remarks = dRFRAModel.Remarks;
                    tbl_DRF_Requisite_RAInfo.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_DRF_Requisite_RAInfo.CreatedDate = DateTime.Today;

                    tbl_DRF_Requisite_RAInfo.ConsultantCost = dRFRAModel.ConsultantCost;
                    tbl_DRF_Requisite_RAInfo.LegalizationCost = dRFRAModel.LegalizationCost;
                    tbl_DRF_Requisite_RAInfo.TranslationCost = dRFRAModel.TranslationCost;
                    tbl_DRF_Requisite_RAInfo.OtherCost = dRFRAModel.OtherCost;

                    int data = _drfRA.insertDRFRA(tbl_DRF_Requisite_RAInfo, Convert.ToInt32(dRFRAModel.DossierTemplateID));

                    var DRFCompanyID = _db.Tbl_DRF_Initialization
                                      .AsNoTracking()
                                      .Where(x => x.InitializationID == dRFRAModel.InitializationID)
                                      .Select(x => x.CompanyID).FirstOrDefault();

                    HttpContext.Session.SetString("CurrentMoleculeCompanyID", DRFCompanyID.ToString());

                    if (data == 1)
                    {
                        ModelState.Clear();
                        SendEmailForDRFDetails(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), tempInitializationID, "RA Requisite Details", "RA Team");
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }

                }

                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }
        }

        [Authorize(Roles = "Prescriber, IPTeam,IP Manager")]
        [HttpPost]
        [ActionName("InsertDRFIPDetails")]
        [Obsolete]
        public ActionResult InsertDRFIPDetails(DRFIPModel dRFIPModel)
        {
            var alreadyExists = _db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == dRFIPModel.InitializationID).Select(t => t.IPDetailsId).FirstOrDefault();

            alreadyExists = alreadyExists == null ? 0 : alreadyExists;

            if (alreadyExists > 0)
            {
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {

                var projectName = dRFIPModel.ProjectName.Trim().ToUpper();
                //  var chkduplicate = (from x in _db.Tbl_DRF_IP_Details where x.ProjectName.ToUpper() == projectName select x.ProjectName).ToList();
                int chkduplicate = 0;
                if (ModelState.IsValid)
                {
                   // if (chkduplicate.Count > 0)
                     if (chkduplicate > 0)
                     {
                        ViewBag.DuplicateProjectName = "Project Name " + dRFIPModel.ProjectName.Trim() + " is already exists in database.";

                     }
                    else
                    {
                        int tempInitializationID = dRFIPModel.InitializationID;
                        DRFIPHeaderAndDetails dRFIPHeaderAndDetails = new DRFIPHeaderAndDetails();
                        dRFIPHeaderAndDetails.InitializationId = dRFIPModel.InitializationID;
                        dRFIPHeaderAndDetails.ProjectName = dRFIPModel.ProjectName;
                        dRFIPHeaderAndDetails.Markets = dRFIPModel.Markets;
                        dRFIPHeaderAndDetails.NumbersOfApprovedANDA = dRFIPModel.NumbersOfApprovedANDA;
                        dRFIPHeaderAndDetails.PatentStatus = dRFIPModel.PatentStatus;
                        dRFIPHeaderAndDetails.LegalStatus = dRFIPModel.LegalStatus;
                        dRFIPHeaderAndDetails.IPDComments = dRFIPModel.IPDComments;
                        dRFIPHeaderAndDetails.NumbersOfApprovedGeneric = dRFIPModel.NumbersOfApprovedGeneric;
                        dRFIPHeaderAndDetails.TypeOfFiling = dRFIPModel.TypeOfFiling;
                        dRFIPHeaderAndDetails.CostofLitigation = dRFIPModel.CostofLitigation;
                        dRFIPHeaderAndDetails.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        dRFIPHeaderAndDetails.CreatedDate = DateTime.Today;

                        List<Tbl_DRF_Patent_Details> dRFIPModelDetailsList = new List<Tbl_DRF_Patent_Details>();

                        if (dRFIPModel.DRFIPModelDetailsList.Length > 0)
                        {
                            List<Tbl_DRF_Patent_Details> List = JsonConvert.DeserializeObject<List<Tbl_DRF_Patent_Details>>(dRFIPModel.DRFIPModelDetailsList);

                            for (int i = 0; i < List.Count; i++)
                            {
                                Tbl_DRF_Patent_Details dRFIPModelDetails = new Tbl_DRF_Patent_Details();
                                dRFIPModelDetails.PatentNumbers = List[i].PatentNumbers;
                                dRFIPModelDetails.OriginalExpiryDate = List[i].OriginalExpiryDate;
                                dRFIPModelDetails.Type = List[i].Type;
                                dRFIPModelDetails.ExtensionApplication = List[i].ExtensionApplication;
                                dRFIPModelDetails.ExtnExpiryDate = List[i].ExtnExpiryDate;
                                dRFIPModelDetails.Comment = List[i].Comment;
                                dRFIPModelDetails.Strategy = List[i].Strategy;
                                dRFIPModelDetailsList.Add(dRFIPModelDetails);
                            }
                        }
                        dRFIPHeaderAndDetails.tbl_DRF_Patent_Details = dRFIPModelDetailsList;

                        var DRFCompanyID = _db.Tbl_DRF_Initialization
                                      .AsNoTracking()
                                      .Where(x => x.InitializationID == dRFIPModel.InitializationID)
                                      .Select(x => x.CompanyID).FirstOrDefault();

                        HttpContext.Session.SetString("CurrentMoleculeCompanyID", DRFCompanyID.ToString());

                        int data = _drfIP.insertDRFIP(dRFIPHeaderAndDetails);

                        if (data == 1)
                        {
                            ModelState.Clear();
                            SendEmailForDRFDetails(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), tempInitializationID, "IP Details", "IP Team");
                            return Json(new { data = "success" }, new JsonSerializerSettings());
                        }
                        else
                        {
                            return Json(new { data = "fail" }, new JsonSerializerSettings());
                        }
                    }

                    return Json(new { data = "fail", message = ViewBag.DuplicateProjectName }, new JsonSerializerSettings());

                }

                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }
        }

        [Authorize(Roles = "Prescriber,MedicalTeam,Medical Manager")]
        [HttpPost]
        [ActionName("InsertDRFMedicalDetails")]
        [Obsolete]
        public ActionResult InsertDRFMedicalDetails(DRFMedicalModel dRFMedicalModel)
        {
            var alreadyExists = _db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == dRFMedicalModel.InitializationID).Select(t => t.MedicalId).FirstOrDefault();

            alreadyExists = alreadyExists == null ? 0 : alreadyExists;

            if (alreadyExists > 0)
            {
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {

                if (ModelState.IsValid)
                {
                    int tempInitializationID = dRFMedicalModel.InitializationID;
                    Tbl_DRF_Medical tbl_DRF_Medical = new Tbl_DRF_Medical();
                    tbl_DRF_Medical.InitializationId = dRFMedicalModel.InitializationID;
                    tbl_DRF_Medical.BeCtVitroAvailable = dRFMedicalModel.BeCtVitroAvailable;
                    tbl_DRF_Medical.BioWaiver = dRFMedicalModel.BioWaiver;
                    tbl_DRF_Medical.CTWaiver = dRFMedicalModel.CTWaiver;
                    tbl_DRF_Medical.Remark1 = dRFMedicalModel.Remark1;
                    tbl_DRF_Medical.Remark2 = dRFMedicalModel.Remark2;
                    tbl_DRF_Medical.Remark3 = dRFMedicalModel.Remark3;
                    tbl_DRF_Medical.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_DRF_Medical.CreatedDate = DateTime.Today;
                    tbl_DRF_Medical.BECost = Convert.ToDecimal(dRFMedicalModel.BECost);
                    tbl_DRF_Medical.BioCost = Convert.ToDecimal(dRFMedicalModel.BioCost);
                    tbl_DRF_Medical.CTCost = Convert.ToDecimal(dRFMedicalModel.CTCost);


                    int data = _dRFMedical.insertDRFMedical(tbl_DRF_Medical);

                    var DRFCompanyID = _db.Tbl_DRF_Initialization
                                      .AsNoTracking()
                                      .Where(x => x.InitializationID == dRFMedicalModel.InitializationID)
                                      .Select(x => x.CompanyID).FirstOrDefault();

                    HttpContext.Session.SetString("CurrentMoleculeCompanyID", DRFCompanyID.ToString());

                    if (data == 1)
                    {
                        ModelState.Clear();
                        SendEmailForDRFDetails(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), tempInitializationID, "Medical Details", "Medical Team");
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }
        }


        public ActionResult DRFApproval(int ID)
        {
            @ViewBag.DRFApprovalInitializationID = ID;            
            HttpContext.Session.SetString("UpdationInitializationID", Convert.ToString(ID));
            return View();
        }
        public IActionResult DRFAddFinanceDetails(int ID)
        {
            @ViewBag.FinanceInitializationID = ID;
            HttpContext.Session.SetString("UpdationInitializationID", Convert.ToString(ID));
            return View();
        }
        public IActionResult DRFFinalApproval(int ID)
        {
            @ViewBag.FinalInitializationID = ID;
            HttpContext.Session.SetString("UpdationInitializationID", Convert.ToString(ID));
            return View();
        }

        
       // [Authorize(Roles = "Prescriber,RegulatoryTeam,Regulatory Manager,SupplyChainManagement,SCM Manager,IPTeam,IP Manager,ManufacturingTeam,Manufacturing Manager,MedicalTeam,Medical Manager")]
        [Authorize]
        [HttpPost]
        [ActionName("InsertDRFInitializationDetails")]
        [Obsolete]
        public ActionResult InsertDRFInitializationDetails(DRFInitialization dRFInitialization)
        {          
            List<string> chkduplicate = null;
            if (ModelState.IsValid)
            {
                var projectName = dRFInitialization.GenericName.Trim().ToUpper();
                // var chkduplicate = (from x in _db.Tbl_DRF_Initialization where x.GenericName.ToUpper() == projectName && x.Strength == dRFInitialization.Strength select x.GenericName).ToList();
                int flag = 0;
                chkduplicate = (from x in _db.Tbl_DRF_Initialization where (x.StatusID == 10 || x.StatusID == 17 || x.StatusID == 18 || x.StatusID == 25) && x.GenericName.ToUpper() == projectName select x.GenericName).ToList();
                if (chkduplicate.Count > 0)
                {
                    flag = 1;                    
                }

                if(flag==0)
                {
                    chkduplicate = (from x in _db.Tbl_DRF_Initialization where x.GenericName.ToUpper() == projectName && x.Strength == dRFInitialization.Strength && x.PackSize == dRFInitialization.PackSize && x.CountryID == dRFInitialization.CountryID select x.GenericName).ToList();
                }
                
                var Strengthname = (from x in _db.Tbl_Master_Strength where x.Id == dRFInitialization.Strength select x.Strength).FirstOrDefault();
                var CountryName = (from x in _db.Master_Country where x.Id == dRFInitialization.CountryID select x.Country).FirstOrDefault();
                int intCountryID = dRFInitialization.CountryID;//use for only email
                if (chkduplicate.Count > 0 && flag==0)
                {
                    ViewBag.DuplicateGenericName = "Generic Name " + dRFInitialization.GenericName.Trim() +" & Strength "+ Strengthname + " is already exists in database.";

                }
                else
                {
                    Tbl_DRF_Initialization tbl_DRF_Initialization = new Tbl_DRF_Initialization();
                    //var uniqueID = _db.Tbl_DRF_Initialization.Select(o => o.DRFNo).LastOrDefault();

                    //if (uniqueID != null)
                    //{
                    //    if(DateTime.Now.Year == Convert.ToInt32(uniqueID.Split('-')[1]))
                    //    {
                    //        tbl_DRF_Initialization.DRFNo = IncreamentUnique("EM-" + DateTime.Now.Year + "-", uniqueID);
                    //    }
                    //    else
                    //    {
                    //        tbl_DRF_Initialization.DRFNo = "EM-" + DateTime.Now.Year + "-000001";
                    //    }

                    //}
                    //else
                    //{
                    //    tbl_DRF_Initialization.DRFNo = "EM-"+DateTime.Now.Year+"-000001";
                    //}
                    dRFInitialization.CurrencyID = 11;
                    dRFInitialization.OwnerOfRegistration= dRFInitialization.MAHolder;
                    tbl_DRF_Initialization.DRFNo = "EM-000000";
                    tbl_DRF_Initialization.CompanyID = dRFInitialization.CompanyID;
                    tbl_DRF_Initialization.CountryID = dRFInitialization.CountryID;
                    tbl_DRF_Initialization.GenericName = dRFInitialization.GenericName;
                    tbl_DRF_Initialization.BrandName = dRFInitialization.BrandName;
                    tbl_DRF_Initialization.TreadmarkApprovedInternal = dRFInitialization.TreadmarkApprovedInternal;
                    tbl_DRF_Initialization.TreadmarkSuggestedInternal = dRFInitialization.TreadmarkSuggestedInternal;
                    tbl_DRF_Initialization.TreadmarkOwnerInternal = dRFInitialization.TreadmarkOwnerInternal;
                    tbl_DRF_Initialization.Form = dRFInitialization.Form;
                    tbl_DRF_Initialization.Strength = dRFInitialization.Strength;
                    tbl_DRF_Initialization.PackSize = dRFInitialization.PackSize;
                    tbl_DRF_Initialization.PackStyle = dRFInitialization.PackStyle;
                    tbl_DRF_Initialization.Plant = dRFInitialization.Plant;
                    tbl_DRF_Initialization.ProductTypeId = dRFInitialization.ProductTypeID;
                    tbl_DRF_Initialization.DossierTemplateID = Convert.ToInt32(dRFInitialization.DossierTemplateID);
                    tbl_DRF_Initialization.CurrencyID = dRFInitialization.CurrencyID;
                    tbl_DRF_Initialization.RegistrationFees = dRFInitialization.RegistrationFees;
                    tbl_DRF_Initialization.FeesToBePaidByID = dRFInitialization.FeesToBePaidByID;
                    tbl_DRF_Initialization.FeesToBePaidBy = dRFInitialization.FeesToBePaidBy;

                    tbl_DRF_Initialization.ModeOfFeesPayment = dRFInitialization.ModeOfFeesPayment;
                    //tbl_DRF_Initialization.MAHolderID = dRFInitialization.MAHolder;
                    tbl_DRF_Initialization.MAHolder = dRFInitialization.MAHolder;
                    tbl_DRF_Initialization.ProposedMarketingStatusID = dRFInitialization.ProposedMarketingStatusID;
                    tbl_DRF_Initialization.ShippingPort = dRFInitialization.ShippingPort;
                    tbl_DRF_Initialization.ModeOfShipment = dRFInitialization.ModeOfShipment;
                    tbl_DRF_Initialization.Incoterms = dRFInitialization.Incoterms;
                    tbl_DRF_Initialization.DossierSubmittedToMOHBy = dRFInitialization.DossierSubmittedToMOHBy;
                    tbl_DRF_Initialization.OwnerOfRegistration = dRFInitialization.OwnerOfRegistration;
                    tbl_DRF_Initialization.AvailabilityofCDA = dRFInitialization.AvailabilityofCDA;
                    tbl_DRF_Initialization.MarketSize = dRFInitialization.MarketSize;
                    tbl_DRF_Initialization.ThreeYearCAGR = dRFInitialization.ThreeYearCAGR;
                    tbl_DRF_Initialization.NumberOfCurrentPlayer = dRFInitialization.NumberOfCurrentPlayer;
                    tbl_DRF_Initialization.InnovatorBrand = dRFInitialization.InnovatorBrand;
                    tbl_DRF_Initialization.FirstBrand = dRFInitialization.FirstBrand;
                    tbl_DRF_Initialization.SecondBrand = dRFInitialization.SecondBrand;
                    tbl_DRF_Initialization.ThirdBrand = dRFInitialization.ThirdBrand;
                    tbl_DRF_Initialization.ExpectedMarketValueGrowth = dRFInitialization.ExpectedMarketValueGrowth;
                    tbl_DRF_Initialization.InnavotorName = dRFInitialization.InnavotorName;
                    tbl_DRF_Initialization.MSFirstBrand = dRFInitialization.MSFirstBrand;
                    tbl_DRF_Initialization.MSSecondBrand = dRFInitialization.MSSecondBrand;
                    tbl_DRF_Initialization.MSThirdBrand = dRFInitialization.MSThirdBrand;
                    tbl_DRF_Initialization.Partner = dRFInitialization.Partner;
                    tbl_DRF_Initialization.FirstYearForecastUnitsPacks = dRFInitialization.FirstYearForecastUnitsPacks;
                    tbl_DRF_Initialization.SecondYearForecastUnitsPacks = dRFInitialization.SecondYearForecastUnitsPacks;
                    tbl_DRF_Initialization.ThirdYearForecastUnitsPacks = dRFInitialization.ThirdYearForecastUnitsPacks;
                    tbl_DRF_Initialization.FirstYearForecastPriceToPatient = dRFInitialization.FirstYearForecastPriceToPatient;
                    tbl_DRF_Initialization.SecondYearForecastPriceToPatient = dRFInitialization.SecondYearForecastPriceToPatient;
                    tbl_DRF_Initialization.ThirdYearForecastPriceToPatient = dRFInitialization.ThirdYearForecastPriceToPatient; 
                    tbl_DRF_Initialization.FirstYearAPIQuantity = dRFInitialization.FirstYearAPIQuantity;
                    tbl_DRF_Initialization.SecondYearAPIQuantity = dRFInitialization.SecondYearAPIQuantity;
                    tbl_DRF_Initialization.ThirdYearAPIQuantity = dRFInitialization.ThirdYearAPIQuantity; 
                    tbl_DRF_Initialization.FirstYearForecastCIF = dRFInitialization.FirstYearForecastCIF;
                    tbl_DRF_Initialization.SecondYearForecastCIF = dRFInitialization.SecondYearForecastCIF;
                    tbl_DRF_Initialization.ThirdYearForecastCIF = dRFInitialization.ThirdYearForecastCIF;
                    tbl_DRF_Initialization.FirstYearForecastValue = dRFInitialization.FirstYearForecastValue;
                    tbl_DRF_Initialization.SecondYearForecastValue = dRFInitialization.SecondYearForecastValue;
                    tbl_DRF_Initialization.ThirdYearForecastValue = dRFInitialization.ThirdYearForecastValue;
                    //tbl_DRF_Initialization.OrderFrequency = dRFInitialization.OrderFrequency;
                    tbl_DRF_Initialization.OrderFrequencyID = dRFInitialization.OrderFrequency;
                    tbl_DRF_Initialization.NameDossierSend = dRFInitialization.NameDossierSend;
                    tbl_DRF_Initialization.AddressDossierSend = dRFInitialization.AddressDossierSend;
                    tbl_DRF_Initialization.StrategyAlignment = dRFInitialization.StrategyAlignment;
                    tbl_DRF_Initialization.ExceptionExplained = dRFInitialization.ExceptionExplained; 
                    tbl_DRF_Initialization.Createdby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")); 
                    tbl_DRF_Initialization.CreatedDate = DateTime.Today;
                    tbl_DRF_Initialization.EmailDossierSend = dRFInitialization.EmailDossierSend;
   
                    tbl_DRF_Initialization.PhoneDossierSend = '+' + dRFInitialization.ConuntryMobileCode + ' ' + dRFInitialization.PhoneDossierSend;
                    tbl_DRF_Initialization.TSExcecuted = dRFInitialization.TSExcecuted;
                    tbl_DRF_Initialization.DAExcecuted = dRFInitialization.DAExcecuted;
                    tbl_DRF_Initialization.IsSamples_Required = dRFInitialization.IsSamples_Required;
                    tbl_DRF_Initialization.Samples_Required = dRFInitialization.Samples_Required;
                    tbl_DRF_Initialization.Remark = dRFInitialization.Remark;
                    tbl_DRF_Initialization.NoofShipmnets = dRFInitialization.NoofShipmnets;


                    int data = _drfINI.insertDRFInitialization(tbl_DRF_Initialization);

                    if (data !=0)
                    {
                        if (dRFInitialization.PIDFID != null && dRFInitialization.PIDFID != 0)
                        {
                            // var DRFID = _db.Tbl_DRF_Initialization.Select(o => o.InitializationID).LastOrDefault();

                            var DRFID = data;

                            Tbl_DRF_PIDF_Mapping tbl_DRF_PIDF_Mapping = new Tbl_DRF_PIDF_Mapping();
                            tbl_DRF_PIDF_Mapping.DRFID = Convert.ToInt32(DRFID);
                            tbl_DRF_PIDF_Mapping.PIDFID = dRFInitialization.PIDFID;
                            _drfINI.DRFPIDFMapping(tbl_DRF_PIDF_Mapping);                            
                        }
                        ModelState.Clear();

                        //Add email notification details
                        string userName = HttpContext.Session.GetString("CurrentUserName") + " has created following DRF : ";
                        var result = _db.Tbl_DRF_Initialization.AsNoTracking().Where(t => t.InitializationID == data).Select(t => t.DRFNo).FirstOrDefault();
                        string userMessage = "DRF Name : " + result;
                        string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago."; 
                      
                        //  string strEmailMessage = userName + "</br>" + "DRF Name : " + result;
                        string strEmailMessage = userName + "</br>" + "DRF No : " + result + "</br>" + "DRF Name: " + projectName + "</br>" + "Strength : " + Strengthname + "</br>" + "Country : " + CountryName;

                        HttpContext.Session.SetString("CurrentMoleculeCompanyID", dRFInitialization.CompanyID.ToString());

                        //send email notification code added by yogesh balapure on date 31/03/2021
                        if (Convert.ToBoolean(_config.GetSection("MailSend:IsDRFInitialization").Value) == true)
                        {
                            MailDetails(strEmailMessage, "Project Initial Approved", intCountryID, "DRF Creation");
                        }                                              
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }
                return Json(new { data = "fail", message = ViewBag.DuplicateGenericName }, new JsonSerializerSettings());        
          
            }
            return Json(new { data = "fail", message = "Please fill all mendatory information" }, new JsonSerializerSettings());
        }

        public IActionResult DRFInitializationUpdation(int ID)
        {
            //@ViewBag.UpdationInitializationID = ID;
            @ViewBag.UpdationInitializationID = Convert.ToInt32( HttpContext.Session.GetString("UpdationInitializationID"));
            return View();
        }

        // [Authorize(Roles = "Prescriber,Line Manager,RegulatoryTeam,Regulatory Manager,SupplyChainManagement,SCM Manager,IPTeam,IP Manager,ManufacturingTeam,Manufacturing Manager,MedicalTeam,Medical Manager")]
        [Authorize]
        [HttpPost]
        [ActionName("UpdateDRFInitializationDetails")]
        [Obsolete]
        public ActionResult UpdateDRFInitializationDetails(DRFInitialization dRFInitialization)
        {

            if (ModelState.IsValid)
            {
                Tbl_DRF_Initialization tbl_DRF_Initialization = new Tbl_DRF_Initialization();
                tbl_DRF_Initialization.InitializationID = Convert.ToInt32(dRFInitialization.InitializationID);
                tbl_DRF_Initialization.CompanyID = dRFInitialization.CompanyID;
                tbl_DRF_Initialization.CountryID = dRFInitialization.CountryID;
                tbl_DRF_Initialization.GenericName = dRFInitialization.GenericName;
                tbl_DRF_Initialization.BrandName = dRFInitialization.BrandName;
                tbl_DRF_Initialization.TreadmarkApprovedInternal = dRFInitialization.TreadmarkApprovedInternal;
                tbl_DRF_Initialization.TreadmarkSuggestedInternal = dRFInitialization.TreadmarkSuggestedInternal;
                tbl_DRF_Initialization.TreadmarkOwnerInternal = dRFInitialization.TreadmarkOwnerInternal;
                tbl_DRF_Initialization.Form = dRFInitialization.Form;
                tbl_DRF_Initialization.Strength = dRFInitialization.Strength;
                tbl_DRF_Initialization.PackSize = dRFInitialization.PackSize;
                tbl_DRF_Initialization.PackStyle = dRFInitialization.PackStyle;
                tbl_DRF_Initialization.Plant = dRFInitialization.Plant;
                tbl_DRF_Initialization.RegistrationFees = dRFInitialization.RegistrationFees;
                tbl_DRF_Initialization.FeesToBePaidByID = dRFInitialization.FeesToBePaidByID;
                tbl_DRF_Initialization.FeesToBePaidBy = dRFInitialization.FeesToBePaidBy;
                tbl_DRF_Initialization.ModeOfFeesPayment = dRFInitialization.ModeOfFeesPayment;
                //tbl_DRF_Initialization.MAHolderID = dRFInitialization.MAHolder;
                tbl_DRF_Initialization.MAHolder = dRFInitialization.MAHolder;
                tbl_DRF_Initialization.ProposedMarketingStatusID = dRFInitialization.ProposedMarketingStatusID;
                tbl_DRF_Initialization.ShippingPort = dRFInitialization.ShippingPort;
                tbl_DRF_Initialization.ModeOfShipment = dRFInitialization.ModeOfShipment;

                tbl_DRF_Initialization.Incoterms = dRFInitialization.Incoterms;
                tbl_DRF_Initialization.DossierSubmittedToMOHBy = dRFInitialization.DossierSubmittedToMOHBy;
                tbl_DRF_Initialization.OwnerOfRegistration = dRFInitialization.OwnerOfRegistration;
                tbl_DRF_Initialization.AvailabilityofCDA = dRFInitialization.AvailabilityofCDA;
                tbl_DRF_Initialization.TSExcecuted = dRFInitialization.TSExcecuted;
                tbl_DRF_Initialization.DAExcecuted = dRFInitialization.DAExcecuted;
                tbl_DRF_Initialization.MarketSize = dRFInitialization.MarketSize;
                tbl_DRF_Initialization.ThreeYearCAGR = dRFInitialization.ThreeYearCAGR;
                tbl_DRF_Initialization.NumberOfCurrentPlayer = dRFInitialization.NumberOfCurrentPlayer;
                tbl_DRF_Initialization.InnovatorBrand = dRFInitialization.InnovatorBrand;
                tbl_DRF_Initialization.FirstBrand = dRFInitialization.FirstBrand;
                tbl_DRF_Initialization.SecondBrand = dRFInitialization.SecondBrand;
                tbl_DRF_Initialization.ThirdBrand = dRFInitialization.ThirdBrand;
                tbl_DRF_Initialization.ExpectedMarketValueGrowth = dRFInitialization.ExpectedMarketValueGrowth;
                tbl_DRF_Initialization.InnavotorName = dRFInitialization.InnavotorName;
                tbl_DRF_Initialization.MSFirstBrand = dRFInitialization.MSFirstBrand;
                tbl_DRF_Initialization.MSSecondBrand = dRFInitialization.MSSecondBrand;
                tbl_DRF_Initialization.MSThirdBrand = dRFInitialization.MSThirdBrand;
                tbl_DRF_Initialization.Partner = dRFInitialization.Partner;
                tbl_DRF_Initialization.FirstYearForecastUnitsPacks = dRFInitialization.FirstYearForecastUnitsPacks;
                tbl_DRF_Initialization.SecondYearForecastUnitsPacks = dRFInitialization.SecondYearForecastUnitsPacks;
                tbl_DRF_Initialization.ThirdYearForecastUnitsPacks = dRFInitialization.ThirdYearForecastUnitsPacks;
                tbl_DRF_Initialization.FirstYearForecastPriceToPatient = dRFInitialization.FirstYearForecastPriceToPatient;
                tbl_DRF_Initialization.SecondYearForecastPriceToPatient = dRFInitialization.SecondYearForecastPriceToPatient;
                tbl_DRF_Initialization.ThirdYearForecastPriceToPatient = dRFInitialization.ThirdYearForecastPriceToPatient;
                tbl_DRF_Initialization.FirstYearAPIQuantity = dRFInitialization.FirstYearAPIQuantity;
                tbl_DRF_Initialization.SecondYearAPIQuantity = dRFInitialization.SecondYearAPIQuantity;
                tbl_DRF_Initialization.ThirdYearAPIQuantity = dRFInitialization.ThirdYearAPIQuantity;
                tbl_DRF_Initialization.FirstYearForecastCIF = dRFInitialization.FirstYearForecastCIF;
                tbl_DRF_Initialization.SecondYearForecastCIF = dRFInitialization.SecondYearForecastCIF;
                tbl_DRF_Initialization.ThirdYearForecastCIF = dRFInitialization.ThirdYearForecastCIF;
                tbl_DRF_Initialization.FirstYearForecastValue = dRFInitialization.FirstYearForecastValue;
                tbl_DRF_Initialization.SecondYearForecastValue = dRFInitialization.SecondYearForecastValue;
                tbl_DRF_Initialization.ThirdYearForecastValue = dRFInitialization.ThirdYearForecastValue;
                tbl_DRF_Initialization.OrderFrequencyID = dRFInitialization.OrderFrequency;
                tbl_DRF_Initialization.NameDossierSend = dRFInitialization.NameDossierSend;
                tbl_DRF_Initialization.EmailDossierSend = dRFInitialization.EmailDossierSend;
                tbl_DRF_Initialization.AddressDossierSend = dRFInitialization.AddressDossierSend;
                tbl_DRF_Initialization.PhoneDossierSend = '+' + dRFInitialization.ConuntryMobileCode + ' ' + dRFInitialization.PhoneDossierSend;
                
                tbl_DRF_Initialization.StrategyAlignment = dRFInitialization.StrategyAlignment;
                tbl_DRF_Initialization.ExceptionExplained = dRFInitialization.ExceptionExplained;
                tbl_DRF_Initialization.Modifiedby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_DRF_Initialization.ModifiedDate = DateTime.Today;
                tbl_DRF_Initialization.IsSamples_Required = dRFInitialization.IsSamples_Required;
                if(dRFInitialization.IsSamples_Required == true)
                {
                    tbl_DRF_Initialization.Samples_Required = dRFInitialization.Samples_Required;
                }
                
                tbl_DRF_Initialization.Remark = dRFInitialization.Remark;
                tbl_DRF_Initialization.NoofShipmnets = dRFInitialization.NoofShipmnets;
                tbl_DRF_Initialization.UpdateRemark = dRFInitialization.UpdateRemark;

                int data = _drfINI.updateDRFInitialization(tbl_DRF_Initialization);

                HttpContext.Session.SetString("CurrentMoleculeCompanyID", dRFInitialization.CompanyID.ToString());

                if (data>0)
                {
                    //send email to drf created
                    SendEmailForDRFUpdation(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), Convert.ToInt32(dRFInitialization.InitializationID), "DRF Updated.", dRFInitialization.UpdateRemark);
                }

                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,RegulatoryTeam,Regulatory Manager,SupplyChainManagement,SCM Manager,IPTeam,IP Manager,ManufacturingTeam,Manufacturing Manager,MedicalTeam,Medical Manager")]
        [HttpPost]
        [ActionName("DeleteDRFInitializationDetails")]
        public ActionResult DeleteDRFInitializationDetails(DRFInitialization dRFInitialization)
        {
                Tbl_DRF_Initialization tbl_DRF_Initialization = new Tbl_DRF_Initialization();
                tbl_DRF_Initialization.InitializationID = Convert.ToInt32(dRFInitialization.InitializationID);
                tbl_DRF_Initialization.Modifiedby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_DRF_Initialization.ModifiedDate = DateTime.Today;

                int data = _drfINI.deleteDRFInitialization(tbl_DRF_Initialization);

                ModelState.Clear();
            return Json(new { data = "success" });
           
        }

        [Authorize]
        [HttpPost]
        [ActionName("GetDRFInitializationList")]
        public ActionResult GetDRFInitializationList()
        {
            IList<Tbl_DRF_Initialization> list = new List<Tbl_DRF_Initialization>();
            int tempuserid = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            int tempUserCompanyID= Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID"));
            list = _drfINI.GetDRFInitializationLists(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), tempUserCompanyID);
            
            return Json(new { data = list});
        }

        //[Authorize(Roles = "Prescriber")]
        [Authorize]
        [HttpPost]
        [ActionName("GetDRFInitializationForEdit")]
        public ActionResult GetDRFInitializationForEdit(DRFInitialization dRFInitialization)
        {
            IList<Tbl_DRF_Initialization> list = new List<Tbl_DRF_Initialization>();
            list = _drfINI.GetDRFInitializationSingleRecord(Convert.ToInt32(dRFInitialization.InitializationID));
            return Json(new { data = list });
        }

        [Authorize]
        public JsonResult GetDropdownForInitializationForm()
        {

            var CompanyLists = _db.Tbl_Master_Company.AsNoTracking().Select(x=> new { x.CompanyID,x.CompanyName});
            
            var CountryLists = (from t1 in _db.Master_Country
                         from t2 in _db.Tbl_Master_User_Country_Mapping.Where(o => t1.Id == o.CountryID)
                         where t2.UserID == Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"))
                         select new
                         {
                             t1.Id,
                             t1.Country
                         }).AsNoTracking();

          //  var list = CountryLists.ToList();

            //var CountryLists1 = _db.Master_Country
            //    .AsNoTracking()
            //     .Where(x => x.IsActive == true)
            //     .OrderBy(x => x.Country)
            //    .Select(x => new { x.Id, x.Country });


            var CountryMobileCodeLists = _db.CountryDailingCode
                .AsNoTracking()
                .Where(x => x.iso3 != null && x.iso3 != "")
                .OrderBy(x => x.iso3)
               .Select(x => new {x.phonecode, Code = string.Format("{0} +{1}", x.iso3, x.phonecode) });

            var FormList = _db.Tbl_Master_Formulation
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Formulation)
                .Select(x => new { x.Id, x.Formulation });

            var PlantList = _db.Tbl_Master_ProductManufacture
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.ProductManufacture)
                .Select(x => new { x.Id, x.ProductManufacture  });

            var PackSizeList = _db.Tbl_Master_PackSize
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.PackSize)
                .Select(x => new { x.Id, x.PackSize });

            var PackStyleList = _db.Tbl_Master_PackStyle
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.PackStyle)
                .Select(x => new { x.Id, x.PackStyle });

            var StrengthList = _db.Tbl_Master_Strength
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Strength)
                .Select(x => new { x.Id, x.Strength });

            var ModeofFeesPaymentList = _db.Tbl_Master_ModeofFeesPayment
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.ModeofFeesPayment)
                .Select(x => new { x.Id, x.ModeofFeesPayment });

            var ModeOfShipmentList = _db.Tbl_Master_Modeofshipment
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Modeofshipment)
                .Select(x => new { x.Id, x.Modeofshipment });

            var IncotermsList = _db.Tbl_Master_Incoterms
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Incoterms)
                .Select(x => new { x.Id, x.Incoterms });

            var DossierTemplateList = _db.Tbl_Master_DossierTemplate
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.DossierTemplate)
                .Select(x => new { x.Id, x.DossierTemplate });

            var CurrencyList = _db.Tbl_Master_Currency
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.CurrencyID)
                .Select(x => new { x.CurrencyID, x.Currency });

            var MAHolderList=_db.Tbl_Master_MAHolder
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Id)
                .Select(x => new { x.Id, x.MAHolder });

            var OrderFrequencyList = _db.Tbl_Master_Orderfrequency
                .AsNoTracking()
               .Where(x => x.IsActive == true)
               .OrderBy(x => x.Id)
               .Select(x => new { x.Id, x.Orderfrequency });

            var MarketingStatusList = _db.Tbl_Master_MarketingStatus
               .AsNoTracking()
              .Where(x => x.IsActive == true)
              .OrderBy(x => x.Id)
              .Select(x => new { x.Id, x.MarketingStatus });

            var ProductTypeList = _db.Tbl_Master_ProductType
               .AsNoTracking()
              .Where(x => x.IsActive == true)
              .OrderBy(x => x.Id)
              .Select(x => new { x.Id, x.ProductType });

            //new code for product master added by yogesh on date 03/09/2021
            IList<ProductMasterDropdownData> allProductDataList = _drfINI.GetAllProductMasterNameList();
            //end

            return Json(new { data = new {
                CompanyLists = CompanyLists, CountryLists = CountryLists , FormList= FormList, PlantList= PlantList, PackSizeList= PackSizeList,
                PackStyleList= PackStyleList, StrengthList= StrengthList,ModeofFeesPaymentList = ModeofFeesPaymentList,
                ModeOfShipmentList = ModeOfShipmentList, IncotermsList = IncotermsList , DossierTemplateList= DossierTemplateList,
                CurrencyList= CurrencyList,
                MAHolderList= MAHolderList,
                OrderFrequencyList = OrderFrequencyList,
                CountryMobileCodeLists= CountryMobileCodeLists,
                MarketingStatusList = MarketingStatusList,
                ProductTypeList = ProductTypeList,
                allProductDataList = allProductDataList
            } });
        }

        [Authorize(Roles = "Prescriber,Initial Approval")]
        [HttpPost]
        [ActionName("UpdateDRFInitialApproval")]
        [Obsolete]
        public async Task<ActionResult> UpdateDRFInitialApproval(DRFInitialApproval dRFInitialApproval)
        {
            var alreadyExists=_db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == dRFInitialApproval.InitializationID).Select(t => t.Id).FirstOrDefault();

            if (alreadyExists > 0 )
            {
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                string userName = HttpContext.Session.GetString("CurrentUserName") + " has initial approval for following project : ";
                var result = _db.Tbl_DRF_Initialization.AsNoTracking().Where(t => t.InitializationID == dRFInitialApproval.InitializationID).Select(t => t.DRFNo).FirstOrDefault();
                string userMessage = "Project Name : " + result;
                string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago.";

                //string strEmailMessage = userName + "</br>" + "Project Name : " + result;

                var Strengthname = (from PD in _db.Tbl_DRF_Initialization
                                    join asp in _db.Tbl_Master_Strength on PD.Strength equals asp.Id
                                    where PD.InitializationID == dRFInitialApproval.InitializationID
                                    select new
                                    {
                                        asp.Strength
                                    }).FirstOrDefault();

                var CountryName = (from PD in _db.Tbl_DRF_Initialization
                                   join asp in _db.Master_Country on PD.CountryID equals asp.Id
                                   where PD.InitializationID == dRFInitialApproval.InitializationID
                                   select new
                                   {
                                       asp.Country
                                   }).FirstOrDefault();

                var projectName = _db.Tbl_DRF_Initialization
                                        .AsNoTracking()
                                        .Where(x => x.InitializationID == dRFInitialApproval.InitializationID)
                                        .Select(x => x.GenericName).FirstOrDefault();

                var DRFCompanyID = _db.Tbl_DRF_Initialization
                                       .AsNoTracking()
                                       .Where(x => x.InitializationID == dRFInitialApproval.InitializationID)
                                       .Select(x => x.CompanyID).FirstOrDefault();

                //string strEmailMessage = userName + "</br>" + "Project No : " + result + "</br>" + "Project Name : " + projectName + "</br>" + "Strength : " + Strengthname + "</br>" + "Country : " + CountryName;
                string strEmailMessage = userName + "</br>" + "Project No : " + result + "</br>" + "DRF Name : " + projectName + "</br>" + "Strength : " + Strengthname.ToString().Replace("{", "").Replace("}", "").Replace("Strength =", "") + "</br>" + "Country : " + CountryName.ToString().Replace("{", "").Replace("}", "").Replace("Country =", "");
                if (ModelState.IsValid)
                {
                    Tbl_DRF_Initialization tbl_DRF_Initialization = new Tbl_DRF_Initialization();

                    tbl_DRF_Initialization.InitializationID = Convert.ToInt32(dRFInitialApproval.InitializationID);

                    if (dRFInitialApproval.InitialApproval == true)
                    {
                        tbl_DRF_Initialization.StatusID = 12;
                        tbl_DRF_Initialization.Status = "Initial Approved";
                    }
                    else
                    {
                        tbl_DRF_Initialization.StatusID = 10;
                        tbl_DRF_Initialization.Status = "Rejected";
                    }

                    tbl_DRF_Initialization.InitialApproveRejectComment = dRFInitialApproval.Comment;

                    tbl_DRF_Initialization.Modifiedby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_DRF_Initialization.ModifiedDate = DateTime.Today;

                    int data = _drfINI.updateDRFInitialApproval(tbl_DRF_Initialization);

                    await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);
                    ModelState.Clear();


                    //send email notification code added by yogesh balapure on date 08/02/2020
                    //get smtp details 
                    SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
                    EmailDetailsModel emailDetailsModel = _emailService.EmailDetails("Project Initial Approved");
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

                    if (emailDetailsModel != null)
                    {
                        //get details
                        emailDetailsVM.ToMail = emailDetailsModel.ToList;
                        List<string> testCC = new List<string>();
                        if (!string.IsNullOrEmpty(emailDetailsModel.CCList))
                        {
                            if (emailDetailsModel.CCList.Contains(";"))
                            {
                                string[] splitcc = emailDetailsModel.CCList.Split(";");
                                foreach (var ccdata in splitcc)
                                {
                                    testCC.Add(ccdata.Trim());
                                }
                            }
                            else
                            {
                                testCC.Add(emailDetailsModel.CCList);
                            }
                        }
                        List<string> testBCC = new List<string>();
                        if (!string.IsNullOrEmpty(emailDetailsModel.BCCList))
                        {
                            if (emailDetailsModel.BCCList.Contains(";"))
                            {
                                string[] splitbcc = emailDetailsModel.BCCList.Split(";");
                                foreach (var ccdata in splitbcc)
                                {
                                    testBCC.Add(ccdata.Trim());
                                }
                            }
                            else
                            {
                                testBCC.Add(emailDetailsModel.BCCList);
                            }
                        }
                        emailDetailsVM.CCMail = testCC;
                        emailDetailsVM.BCCMail = testBCC;
                        emailDetailsVM.Subject = emailDetailsModel.MailSubject;

                        clsTemplate _clsTemplate = new clsTemplate(_config, _env);

                        //emailDetailsVM.Body = emailDetailsModel.MailBody;
                        string tempurl = _config.GetSection("ApplicationURL:DrfUrlLink").Value + dRFInitialApproval.InitializationID;
                       // emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID")));
                        emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(DRFCompanyID));
                        emailDetailsVM.DispalyName = "";
                    }

                    if (sMTPDetailsModel != null && emailDetailsModel != null)
                    {
                        EmailHelper emailHelper = new EmailHelper();
                        if (Convert.ToBoolean(_config.GetSection("MailSend:IsUpdateaDRFInitialApproval").Value) == true)
                        {
                            var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                        }
                            
                    }

                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }

                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }

            
        }


        //[Authorize(Roles = "Prescriber")]
        [Authorize]
        [HttpPost]  
        [ActionName("GetFilledIPMANSCMRADetails")]
        public ActionResult GetFilledIPMANSCMRADetails(GetDRFFilledDetailsByID getDRFFilledDetailsByID)
        {

            var InitializationDetails = (from i in _db.Tbl_DRF_Initialization
                                         join c in _db.Master_Country on i.CountryID equals c.Id
                                         join s in _db.Tbl_Master_PIDFStatus on i.StatusID equals s.PidfStatusID
                                         join TMF in _db.Tbl_Master_Formulation on i.Form equals TMF.Id
                                         join TMS in _db.Tbl_Master_Strength on i.Strength equals TMS.Id
                                         join TMP in _db.Tbl_Master_PackSize on i.PackSize equals TMP.Id
                                         join TMPSS in _db.Tbl_Master_PackStyle on i.PackStyle equals TMPSS.Id
                                         join TMPT in _db.Tbl_Master_ProductType on i.ProductTypeId equals TMPT.Id
                                         // from TMPM in _db.Tbl_Master_ProductManufacture  on i.Plant equals TMPM.Id
                                         from TMPM in _db.Tbl_Master_ProductManufacture.Where(o => o.Id == i.Plant).DefaultIfEmpty()
                                         from TMM in _db.Tbl_Master_ModeofFeesPayment.Where(x => x.Id == i.ModeOfFeesPayment).DefaultIfEmpty()
                                         from TMMS in _db.Tbl_Master_Modeofshipment.Where(x=> x.Id == i.ModeOfShipment).DefaultIfEmpty()
                                         from TMI in _db.Tbl_Master_Incoterms.Where(x=>x.Id == i.Incoterms).DefaultIfEmpty()
                                         from tblMMS in _db.Tbl_Master_MarketingStatus.Where(x=>x.Id == i.ProposedMarketingStatusID).DefaultIfEmpty()
                                         from TMO in _db.Tbl_Master_Orderfrequency.AsNoTracking().Where(x=>x.Id == i.OrderFrequencyID).DefaultIfEmpty()
                                         //join TMM in _db.Tbl_Master_ModeofFeesPayment  on i.ModeOfFeesPayment equals TMM.Id
                                         //join TMMS in _db.Tbl_Master_Modeofshipment  on i.ModeOfShipment equals TMMS.Id
                                         //join TMI in _db.Tbl_Master_Incoterms  on i.Incoterms equals TMI.Id
                                         ////join TMDT in _db.Tbl_Master_DossierTemplate on i.DossierTemplateID equals TMDT.Id
                                         from TMDT in _db.Tbl_Master_DossierTemplate.Where(n => n.Id == i.DossierTemplateID).DefaultIfEmpty()
                                         where i.InitializationID == getDRFFilledDetailsByID.InitializationID
                                         select new
                                         {
                                             i.InitializationID,
                                             i.DRFNo,
                                             i.CompanyID,
                                             i.CountryID,
                                             c.Country,
                                             i.GenericName,
                                             i.BrandName,
                                             i.TreadmarkApprovedInternal,
                                             i.TreadmarkSuggestedInternal,
                                             i.TreadmarkOwnerInternal,
                                             //i.Form
                                             FormulationID = i.Form,
                                             Form = TMF.Formulation,
                                             //i.Strength
                                             StrengthID = i.Strength,
                                             Strength = TMS.Strength,
                                             //i.PackSize
                                             PackSizeID = i.PackSize,
                                             PackSize = TMP.PackSize,
                                             //i.PackStyle
                                             PackStyleID =  i.PackStyle,
                                             PackStyle = TMPSS.PackStyle,
                                             //i.Plant
                                             PlantID = i.Plant,
                                             Plant = TMPM.ProductManufacture,
                                             ProductTypeID = i.ProductTypeId,
                                             ProductType = TMPT.ProductType,
                                             i.RegistrationFees,
                                             i.FeesToBePaidByID,
                                             i.FeesToBePaidBy,
                                             //i.ModeOfFeesPayment
                                             ModeOfFeesPaymentID = i.ModeOfFeesPayment,
                                             ModeOfFeesPayment = TMM.ModeofFeesPayment,
                                             i.MAHolder,
                                             i.ProposedMarketingStatusID,
                                             ProposedMarketingStatus = tblMMS.MarketingStatus,
                                             i.ShippingPort,
                                             //i.ModeOfShipment
                                             ModeOfShipmentID = i.ModeOfShipment,
                                             ModeOfShipment = TMMS.Modeofshipment,
                                             //i.Incoterms
                                             IncotermsID = i.Incoterms,
                                             Incoterms = TMI.Incoterms,
                                             i.DossierSubmittedToMOHBy,
                                             i.OwnerOfRegistration,
                                             i.AvailabilityofCDA,
                                             i.MarketSize,
                                             i.ThreeYearCAGR,
                                             i.NumberOfCurrentPlayer,
                                             i.InnovatorBrand,
                                             i.FirstBrand,
                                             i.SecondBrand,
                                             i.ThirdBrand,
                                             i.ExpectedMarketValueGrowth,
                                             i.InnavotorName,
                                             i.MSFirstBrand,
                                             i.MSSecondBrand,
                                             i.MSThirdBrand,
                                             i.Partner,
                                             i.FirstYearForecastUnitsPacks,
                                             i.SecondYearForecastUnitsPacks,
                                             i.ThirdYearForecastUnitsPacks,
                                             i.FirstYearForecastPriceToPatient,
                                             i.SecondYearForecastPriceToPatient,
                                             i.ThirdYearForecastPriceToPatient,
                                             i.FirstYearAPIQuantity,
                                             i.SecondYearAPIQuantity,
                                             i.ThirdYearAPIQuantity,
                                             i.FirstYearForecastCIF,
                                             i.SecondYearForecastCIF,
                                             i.ThirdYearForecastCIF,
                                             i.FirstYearForecastValue,
                                             i.SecondYearForecastValue,
                                             i.ThirdYearForecastValue,
                                             i.OrderFrequencyID,
                                             OrderFrequency = TMO.Orderfrequency,
                                             i.NameDossierSend,
                                             i.AddressDossierSend,
                                             i.StrategyAlignment,
                                             i.ExceptionExplained,
                                             i.StatusID,
                                             s.PidfStatus,
                                             i.InitialApproveRejectComment,
                                             i.DossierTemplateID,
                                             TMDT.DossierTemplate,
                                             i.CurrencyID,
                                             i.Currency,
                                             i.TSExcecuted,
                                             i.DAExcecuted,
                                             i.EmailDossierSend,
                                             i.PhoneDossierSend,
                                             i.IsSamples_Required,
                                             i.Samples_Required,
                                             i.Remark,
                                             i.NoofShipmnets,
                                             i.UpdateRemark
                                         }).FirstOrDefault();


            HttpContext.Session.SetString("DrfID", InitializationDetails.InitializationID.ToString());
            HttpContext.Session.SetString("ProductID", InitializationDetails.DRFNo.ToString());
            HttpContext.Session.SetString("Action", "DRF"); 

           //HttpContext.Session.SetString("DrfID", "21");
           //HttpContext.Session.SetString("ProductID", "EM-2021-000021");
           //HttpContext.Session.SetString("Action", "DRF"); 
            HttpContext.Session.SetString("ProjectManager", "Rahul Patil");//DRFList.ProjectManager.ToString()

            IList<Tbl_DRF_IP_Details> IPHeaderList = new List<Tbl_DRF_IP_Details>();
            IPHeaderList = _drfIP.GetIPHeaderFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));
            

            IList<Tbl_DRF_Patent_Details> IPDetailsList = new List<Tbl_DRF_Patent_Details>();
            IPDetailsList = _drfIP.GetIPHeaderDetailsFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_Manufacturing> ManufacturingList = new List<Tbl_DRF_Manufacturing>();
            ManufacturingList = _drfMAN.GetManufacturingFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_Manufacturing_APISite> ManufacturingAPIList = new List<Tbl_DRF_Manufacturing_APISite>();
            ManufacturingAPIList = _drfMAN.GetManufacturingAPIListDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_SupplyChainMgmt> SCMList = new List<Tbl_DRF_SupplyChainMgmt>();
            SCMList = _drfSCM.GetSCMFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_Requisite_RAInfo> RAList = new List<Tbl_DRF_Requisite_RAInfo>();
            RAList = _drfRA.GetRAFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_Medical> MedicalList = new List<Tbl_DRF_Medical>();
            MedicalList = _dRFMedical.GetMedicalFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            var FinanceDetails = (from i in _db.Tbl_DRFDataMaster
                                         join s in _db.Tbl_DRF_FinanceDetails on i.FinanceId equals s.Id
                                         where i.InitializationId == getDRFFilledDetailsByID.InitializationID
                                         select new
                                         {
                                            s.Overallbusinesscase,
                                            s.Exworks,
                                            s.GCminimum,
                                            s.ExworksYearTwo,
                                            s.ExworksYearThree,
                                            s.GCminimumYearTwo,
                                            s.GCminimumYearThree,
                                            s.Expenses,
                                            s.LitigationCost,
                                            s.FreightCost,
                                            s.RegistrationCost,
                                            s.ConsultantCost,
                                            s.LegalizationCost,
                                            s.TranslationCost,
                                            s.OtherCost,
                                            s.BECost,
                                            s.BioCost,
                                            s.CTCost,
                                            s.FilingCost,
                                            s.Freight,
                                            s.FreightYearTwo,
                                            s.FreightYearThree,
                                            s.TotalContribution,
                                            s.TotalPercentage,
                                            s.NetContribution,
                                            s.NetPercentage
                                         }).FirstOrDefault();

            var FinalApprovalDetails = (from i in _db.Tbl_DRFDataMaster
                                join f in _db.Tbl_DRF_FinalApprovelDetails on i.FinalId equals f.Id
                                where i.InitializationId == getDRFFilledDetailsByID.InitializationID
                                select new
                                {
                                    f.ApprovedReject,
                                    f.Comment
                                }).FirstOrDefault();

            var DRFApprovalDetails = (from TDFA in _db.Tbl_DRF_FormApprovals
                                       from DRF in _db.PrescriberDetails.Where(n => n.AspNetUserId == TDFA.DRFCreatedBy).DefaultIfEmpty()
                                       from CM in _db.PrescriberDetails.Where(n => n.AspNetUserId == TDFA.CMCreatedBy).DefaultIfEmpty()
                                       from LM in _db.PrescriberDetails.Where(n => n.AspNetUserId == TDFA.LMCreatedBy).DefaultIfEmpty()
                                       from HOD in _db.PrescriberDetails.Where(n => n.AspNetUserId == TDFA.HODCreatedBy).DefaultIfEmpty()
                                           // from master in _db.Tbl_DRFDataMaster.Where(n=>n.InitializationId==getDRFFilledDetailsByID.InitializationID).DefaultIfEmpty()
                                           //from finalentry in _db.Tbl_DRF_FinalApprovelDetails.Where(n=>n.Id == master.FinalId).DefaultIfEmpty()
                                           //from final in _db.PrescriberDetails.Where(n => n.AspNetUserId == finalentry.Createdby ).DefaultIfEmpty()
                                      from anu1 in _db.AspNetUsers.Where(x => x.UserId == DRF.AspNetUserId).DefaultIfEmpty()
                                      from anu2 in _db.AspNetUsers.Where(x => x.UserId == CM.AspNetUserId).DefaultIfEmpty()
                                      from anu3 in _db.AspNetUsers.Where(x => x.UserId == LM.AspNetUserId).DefaultIfEmpty()
                                      from anu4 in _db.AspNetUsers.Where(x => x.UserId == HOD.AspNetUserId).DefaultIfEmpty()
                                      let p = (from anur1 in _db.AspNetUserRoles where anu1.Id == anur1.UserId orderby anur1.RoleId select anur1).DefaultIfEmpty().First()
                                      let q = (from anur2 in _db.AspNetUserRoles where anu2.Id == anur2.UserId orderby anur2.RoleId select anur2).DefaultIfEmpty().First()
                                      let r = (from anur3 in _db.AspNetUserRoles where anu3.Id == anur3.UserId orderby anur3.RoleId select anur3).DefaultIfEmpty().First()
                                      let s = (from anur4 in _db.AspNetUserRoles where anu4.Id == anur4.UserId orderby anur4.RoleId select anur4).DefaultIfEmpty().First()                                      
                                      from anr1 in _db.AspNetRoles.Where(x => x.Id == p.RoleId).Take(1).DefaultIfEmpty()
                                      from anr2 in _db.AspNetRoles.Where(x => x.Id == q.RoleId).Take(1).DefaultIfEmpty()
                                      from anr3 in _db.AspNetRoles.Where(x => x.Id == r.RoleId).Take(1).DefaultIfEmpty()
                                      from anr4 in _db.AspNetRoles.Where(x => x.Id == s.RoleId).Take(1).DefaultIfEmpty()

                                      where TDFA.InitializationID == getDRFFilledDetailsByID.InitializationID
                                      select new
                                      {
                                          TDFA.ID,
                                         DRFCreatedName=DRF.FirstName +" " +DRF.LastName + (string.IsNullOrEmpty(anr1.Name) == true ? "" : "(" + anr1.Name + ")"),
                                          TDFA.DRFCreatedBy,
                                          TDFA.DRFCreatedDate,
                                          CMApprovalName = CM.FirstName + " " + CM.LastName + (string.IsNullOrEmpty(anr2.Name) == true ? "" : "(" + anr2.Name + ")"),
                                          TDFA.CMCreatedBy,
                                          TDFA.CMCreatedDate,
                                          LMApprovalName = LM.FirstName + " " + LM.LastName + (string.IsNullOrEmpty(anr3.Name) == true ? "" : "(" + anr3.Name + ")"),
                                          TDFA.LMCreatedBy,
                                          TDFA.LMCreatedDate,
                                          HODApprovalName = HOD.FirstName + " " + HOD.LastName + (string.IsNullOrEmpty(anr4.Name) == true ? "": "(" + anr4.Name + ")"),
                                          TDFA.HODCreatedBy,
                                          TDFA.HODCreatedDate
                                          //FinalApprovalName = final.FirstName + " " + final.LastName,
                                          //FinalCreatedBy = finalentry.Createdby,
                                          //FinalCreatedDate = finalentry.CreatedDate

                                      }).FirstOrDefault();

            FinalApprovalDetails FinalApprovalList = new FinalApprovalDetails();
            FinalApprovalList = _dRFFinal.GetFinalApprovalDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            return Json(new { data =new { InitializationDetails= InitializationDetails, IPHeaderList = IPHeaderList, IPDetailsList = IPDetailsList, ManufacturingList = ManufacturingList, ManufacturingAPIList = ManufacturingAPIList, SCMList = SCMList , RAList = RAList , MedicalList= MedicalList, FinanceDetails= FinanceDetails , FinalApprovalDetails = FinalApprovalDetails, DRFApprovalDetails =DRFApprovalDetails , FinalApprovalList= FinalApprovalList } });
        }

        [Authorize]
        [HttpPost]
        [ActionName("GetFilledIPMANSCMRADetailsForReject")]
        public ActionResult GetFilledIPMANSCMRADetailsForReject(GetDRFFilledDetailsByID getDRFFilledDetailsByID)
        {

            var InitializationDetails = (from i in _db.Tbl_DRF_Initialization
                                         join c in _db.Master_Country on i.CountryID equals c.Id
                                         join s in _db.Tbl_Master_PIDFStatus on i.StatusID equals s.PidfStatusID
                                         join TMF in _db.Tbl_Master_Formulation on i.Form equals TMF.Id
                                         join TMS in _db.Tbl_Master_Strength on i.Strength equals TMS.Id
                                         join TMP in _db.Tbl_Master_PackSize on i.PackSize equals TMP.Id
                                         join TMPSS in _db.Tbl_Master_PackStyle on i.PackStyle equals TMPSS.Id
                                         //join TMPM in _db.Tbl_Master_ProductManufacture on i.Plant equals TMPM.Id
                                         from TMPM in _db.Tbl_Master_ProductManufacture.Where(o => o.Id == i.Plant).DefaultIfEmpty()
                                             //join TMM in _db.Tbl_Master_ModeofFeesPayment on i.ModeOfFeesPayment equals TMM.Id
                                         from TMM in _db.Tbl_Master_ModeofFeesPayment.Where(x => x.Id == i.ModeOfFeesPayment).DefaultIfEmpty()
                                             //join TMMS in _db.Tbl_Master_Modeofshipment on i.ModeOfShipment equals TMMS.Id
                                         from TMMS in _db.Tbl_Master_Modeofshipment.Where(x => x.Id == i.ModeOfShipment).DefaultIfEmpty()
                                             //join TMI in _db.Tbl_Master_Incoterms on i.Incoterms equals TMI.Id
                                         from TMI in _db.Tbl_Master_Incoterms.Where(x => x.Id == i.Incoterms).DefaultIfEmpty()
                                             //join TMDT in _db.Tbl_Master_DossierTemplate on i.DossierTemplateID equals TMDT.Id
                                         from TMDT in _db.Tbl_Master_DossierTemplate.Where(n => n.Id == i.DossierTemplateID).DefaultIfEmpty()
                                         where i.InitializationID == getDRFFilledDetailsByID.InitializationID
                                         select new
                                         {
                                             i.InitializationID
                                              ,
                                             i.DRFNo
                                              ,
                                             i.CountryID
                                             ,
                                             c.Country
                                              ,
                                             i.GenericName
                                              ,
                                             i.BrandName
                                              ,
                                             i.TreadmarkApprovedInternal
                                              ,
                                             i.TreadmarkSuggestedInternal
                                              ,
                                             i.TreadmarkOwnerInternal
                                              ,
                                              i.InitialApproveRejectComment
                                              ,
                                             //i.Form
                                             Form = TMF.Formulation
                                              ,
                                             //i.Strength
                                             Strength = TMS.Strength
                                              ,
                                             //i.PackSize
                                             PackSize = TMP.PackSize
                                              ,
                                             //i.PackStyle
                                             PackStyle = TMPSS.PackStyle
                                              ,
                                             //i.Plant
                                             Plant = TMPM.ProductManufacture
                                              ,
                                             i.RegistrationFees
                                              ,
                                             i.FeesToBePaidBy
                                              ,
                                             //i.ModeOfFeesPayment
                                             ModeOfFeesPayment = TMM.ModeofFeesPayment
                                              ,
                                             i.MAHolder
                                              ,
                                             i.ProposedMarketingStatus
                                              ,
                                             i.ShippingPort
                                              ,
                                             //i.ModeOfShipment
                                             ModeOfShipment = TMMS.Modeofshipment
                                              ,
                                             //i.Incoterms
                                             Incoterms = TMI.Incoterms
                                              ,
                                             i.DossierSubmittedToMOHBy
                                              ,
                                             i.OwnerOfRegistration
                                             ,
                                             i.AvailabilityofCDA
                                              ,
                                             i.MarketSize
                                              ,
                                             i.ThreeYearCAGR
                                              ,
                                             i.NumberOfCurrentPlayer
                                              ,
                                             i.InnovatorBrand
                                              ,
                                             i.FirstBrand
                                              ,
                                             i.SecondBrand
                                              ,
                                             i.ThirdBrand
                                              ,
                                             i.ExpectedMarketValueGrowth
                                              ,
                                             i.InnavotorName
                                              ,
                                             i.MSFirstBrand
                                              ,
                                             i.MSSecondBrand
                                              ,
                                             i.MSThirdBrand
                                              ,
                                             i.Partner
                                              ,
                                             i.FirstYearForecastUnitsPacks
                                              ,
                                             i.SecondYearForecastUnitsPacks
                                              ,
                                             i.ThirdYearForecastUnitsPacks
                                              ,
                                             i.FirstYearForecastPriceToPatient
                                              ,
                                             i.SecondYearForecastPriceToPatient
                                              ,
                                             i.ThirdYearForecastPriceToPatient
                                              ,
                                             i.FirstYearAPIQuantity
                                              ,
                                             i.SecondYearAPIQuantity
                                              ,
                                             i.ThirdYearAPIQuantity
                                              ,
                                             i.FirstYearForecastCIF
                                              ,
                                             i.SecondYearForecastCIF
                                              ,
                                             i.ThirdYearForecastCIF
                                              ,
                                             i.FirstYearForecastValue
                                              ,
                                             i.SecondYearForecastValue
                                              ,
                                             i.ThirdYearForecastValue
                                              ,
                                             i.OrderFrequency
                                              ,
                                             i.NameDossierSend
                                              ,
                                             i.AddressDossierSend
                                              ,
                                             i.StrategyAlignment
                                              ,
                                             i.ExceptionExplained
                                              ,
                                             i.StatusID
                                              ,
                                             s.PidfStatus
                                             ,                                             
                                             i.DossierTemplateID
                                             ,
                                             TMDT.DossierTemplate
                                             ,
                                             i.Currency,
                                             i.TSExcecuted,
                                             i.DAExcecuted,
                                             i.EmailDossierSend,
                                             i.PhoneDossierSend,
                                             i.Samples_Required,
                                             i.Remark
                                         }).FirstOrDefault();


            HttpContext.Session.SetString("DrfID", InitializationDetails.InitializationID.ToString());
            HttpContext.Session.SetString("ProductID", InitializationDetails.DRFNo.ToString());
            HttpContext.Session.SetString("Action", "DRF");

            //HttpContext.Session.SetString("DrfID", "21");
            //HttpContext.Session.SetString("ProductID", "EM-2021-000021");
            //HttpContext.Session.SetString("Action", "DRF"); 
            HttpContext.Session.SetString("ProjectManager", "Rahul Patil");//DRFList.ProjectManager.ToString()

            IList<Tbl_DRF_IP_Details> IPHeaderList = new List<Tbl_DRF_IP_Details>();
            IPHeaderList = _drfIP.GetIPHeaderFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));


            IList<Tbl_DRF_Patent_Details> IPDetailsList = new List<Tbl_DRF_Patent_Details>();
            IPDetailsList = _drfIP.GetIPHeaderDetailsFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_Manufacturing> ManufacturingList = new List<Tbl_DRF_Manufacturing>();
            ManufacturingList = _drfMAN.GetManufacturingFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_Manufacturing_APISite> ManufacturingAPIList = new List<Tbl_DRF_Manufacturing_APISite>();
            ManufacturingAPIList = _drfMAN.GetManufacturingAPIListDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_SupplyChainMgmt> SCMList = new List<Tbl_DRF_SupplyChainMgmt>();
            SCMList = _drfSCM.GetSCMFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_Requisite_RAInfo> RAList = new List<Tbl_DRF_Requisite_RAInfo>();
            RAList = _drfRA.GetRAFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            IList<Tbl_DRF_Medical> MedicalList = new List<Tbl_DRF_Medical>();
            MedicalList = _dRFMedical.GetMedicalFilledDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            var FinanceDetails = (from i in _db.Tbl_DRFDataMaster
                                  join s in _db.Tbl_DRF_FinanceDetails on i.FinanceId equals s.Id
                                  join pre in _db.PrescriberDetails on s.Modifiedby equals pre.AspNetUserId
                                  where i.InitializationId == getDRFFilledDetailsByID.InitializationID
                                  select new
                                  {
                                      s.Overallbusinesscase,
                                      s.Exworks,
                                      s.GCminimum,
                                      s.ExworksYearTwo,
                                      s.ExworksYearThree,
                                      s.GCminimumYearTwo,
                                      s.GCminimumYearThree,
                                      s.Expenses,
                                      s.LitigationCost,
                                      s.FreightCost,
                                      s.RegistrationCost,
                                      s.ConsultantCost,
                                      s.LegalizationCost,
                                      s.TranslationCost,
                                      s.OtherCost,
                                      s.BECost,
                                      s.BioCost,
                                      s.CTCost,
                                      s.FilingCost,
                                      s.Freight,
                                      s.FreightYearTwo,
                                      s.FreightYearThree,
                                      s.TotalContribution,
                                      s.TotalPercentage,
                                      s.NetContribution,
                                      s.NetPercentage,
                                      s.ModifiedDate,
                                      Name = string.Format("{0} {1}", pre.FirstName + " ", pre.LastName)
                                    
                                  }).FirstOrDefault();

            var FinalApprovalDetails = (from i in _db.Tbl_DRFDataMaster
                                        join f in _db.Tbl_DRF_FinalApprovelDetails on i.FinalId equals f.Id
                                        where i.InitializationId == getDRFFilledDetailsByID.InitializationID
                                        select new
                                        {
                                            f.ApprovedReject,
                                            f.Comment
                                        }).FirstOrDefault();

            var DRFApprovalDetails = (from TDFA in _db.Tbl_DRF_FormApprovals
                                      from DRF in _db.PrescriberDetails.Where(n => n.AspNetUserId == TDFA.DRFCreatedBy).DefaultIfEmpty()
                                      from CM in _db.PrescriberDetails.Where(n1 => n1.AspNetUserId == TDFA.CMCreatedBy).DefaultIfEmpty()
                                      from LM in _db.PrescriberDetails.Where(n2 => n2.AspNetUserId == TDFA.LMCreatedBy).DefaultIfEmpty()
                                          //from HOD in _db.PrescriberDetails.Where(n => n.AspNetUserId == TDFA.HODCreatedBy).DefaultIfEmpty()
                                      let HOD=(from HOD1 in _db.PrescriberDetails where TDFA.HODCreatedBy == HOD1.AspNetUserId  orderby HOD1.AspNetUserId select HOD1).DefaultIfEmpty().FirstOrDefault()
                                          // from master in _db.Tbl_DRFDataMaster.Where(n=>n.InitializationId==getDRFFilledDetailsByID.InitializationID).DefaultIfEmpty()
                                          //from finalentry in _db.Tbl_DRF_FinalApprovelDetails.Where(n=>n.Id == master.FinalId).DefaultIfEmpty()
                                          //from final in _db.PrescriberDetails.Where(n => n.AspNetUserId == finalentry.Createdby ).DefaultIfEmpty()
                                      from anu1 in _db.AspNetUsers.Where(x => x.UserId == DRF.AspNetUserId).DefaultIfEmpty()
                                      from anu2 in _db.AspNetUsers.Where(x => x.UserId == CM.AspNetUserId).DefaultIfEmpty()
                                      from anu3 in _db.AspNetUsers.Where(x => x.UserId == LM.AspNetUserId).DefaultIfEmpty()
                                      from anu4 in _db.AspNetUsers.Where(x => x.UserId == HOD.AspNetUserId).DefaultIfEmpty()
                                      let p = (from anur1 in _db.AspNetUserRoles where anu1.Id == anur1.UserId orderby anur1.RoleId select anur1).DefaultIfEmpty().First()
                                      let q = (from anur2 in _db.AspNetUserRoles where anu2.Id == anur2.UserId orderby anur2.RoleId select anur2).DefaultIfEmpty().First()
                                      //let r = (from anur3 in _db.AspNetUserRoles where anu3.Id == anur3.UserId orderby anur3.RoleId select anur3).DefaultIfEmpty().First()
                                      let s = (from anur4 in _db.AspNetUserRoles where anu4.Id == anur4.UserId orderby anur4.RoleId select anur4).DefaultIfEmpty().FirstOrDefault()
                                      from anr1 in _db.AspNetRoles.Where(x => x.Id == p.RoleId).Take(1).DefaultIfEmpty()
                                      from anr2 in _db.AspNetRoles.Where(x1 => x1.Id == q.RoleId).Take(1).DefaultIfEmpty()
                                          //from anr3 in _db.AspNetRoles.Where(x => x.Id == r.RoleId).Take(1).DefaultIfEmpty()
                                      let anr3 = (from anur31 in _db.AspNetRoles where q.RoleId == anur31.Id orderby anur31.Id select anur31).DefaultIfEmpty().FirstOrDefault()
                                      //from anr4 in _db.AspNetRoles.Where(x => x.Id == s.RoleId).Take(1).DefaultIfEmpty()
                                      let anr4 = (from anur41 in _db.AspNetRoles where s.RoleId == anur41.Id orderby anur41.Id select anur41).DefaultIfEmpty().FirstOrDefault()
                                      where TDFA.InitializationID == getDRFFilledDetailsByID.InitializationID
                                      select new
                                      {
                                          TDFA.ID,
                                          DRFCreatedName = DRF.FirstName + " " + DRF.LastName + (string.IsNullOrEmpty(anr1.Name) == true ? "" : "(" + anr1.Name + ")"),
                                          TDFA.DRFCreatedBy,
                                          TDFA.DRFCreatedDate,
                                          CMApprovalName = CM.FirstName + " " + CM.LastName + (string.IsNullOrEmpty(anr2.Name) == true ? "" : "(" + anr2.Name + ")"),
                                          TDFA.CMCreatedBy,
                                          TDFA.CMCreatedDate,
                                          LMApprovalName = LM.FirstName + " " + LM.LastName + (string.IsNullOrEmpty(anr3.Name) == true ? "" : "(" + anr3.Name + ")"),
                                          TDFA.LMCreatedBy,
                                          TDFA.LMCreatedDate,
                                          HODApprovalName = HOD.FirstName + " " + HOD.LastName + (string.IsNullOrEmpty(anr4.Name) == true ? "" : "(" + anr4.Name + ")"),
                                          TDFA.HODCreatedBy,
                                          TDFA.HODCreatedDate
                                          //FinalApprovalName = final.FirstName + " " + final.LastName,
                                          //FinalCreatedBy = finalentry.Createdby,
                                          //FinalCreatedDate = finalentry.CreatedDate

                                      }).FirstOrDefault();

            FinalApprovalDetails FinalApprovalList = new FinalApprovalDetails();
            FinalApprovalList = _dRFFinal.GetFinalApprovalDetails(Convert.ToInt32(getDRFFilledDetailsByID.InitializationID));

            return Json(new { data = new { InitializationDetails = InitializationDetails, IPHeaderList = IPHeaderList, IPDetailsList = IPDetailsList, ManufacturingList = ManufacturingList, ManufacturingAPIList = ManufacturingAPIList, SCMList = SCMList, RAList = RAList, MedicalList = MedicalList, FinanceDetails = FinanceDetails, FinalApprovalDetails = FinalApprovalDetails, DRFApprovalDetails = DRFApprovalDetails, FinalApprovalList = FinalApprovalList } });
        }

        [Authorize(Roles = "Prescriber,FinanceTeam,Finance Manager")]
        [HttpPost]
        [ActionName("InsertDRFFinanceApproval")]
        public ActionResult InsertDRFFinanceApproval(DRFFinanceApproval dRFFinanceApproval)
        {
            var alreadyExists = _db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == dRFFinanceApproval.InitializationID).Select(t => t.FinanceId).FirstOrDefault();
           
            alreadyExists = alreadyExists == null ? 0 : alreadyExists;
            
            if (alreadyExists > 0)
            {
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Tbl_DRF_FinanceDetails tbl_DRF_FinanceDetails = new Tbl_DRF_FinanceDetails();

                    tbl_DRF_FinanceDetails.InitializationID = Convert.ToInt32(dRFFinanceApproval.InitializationID);
                    tbl_DRF_FinanceDetails.Overallbusinesscase = dRFFinanceApproval.IsOverallBusinessCaseFine;
                    tbl_DRF_FinanceDetails.Exworks = dRFFinanceApproval.Exworks;
                    tbl_DRF_FinanceDetails.GCminimum = dRFFinanceApproval.GCMinimum;

                    tbl_DRF_FinanceDetails.ExworksYearTwo = dRFFinanceApproval.ExworksYearTwo;
                    tbl_DRF_FinanceDetails.ExworksYearThree = dRFFinanceApproval.ExworksYearThree;

                    tbl_DRF_FinanceDetails.GCminimumYearTwo = dRFFinanceApproval.GCMinimumYearTwo;
                    tbl_DRF_FinanceDetails.GCminimumYearThree = dRFFinanceApproval.GCMinimumYearThree;

                    tbl_DRF_FinanceDetails.Expenses = dRFFinanceApproval.Expenses;
                    tbl_DRF_FinanceDetails.LitigationCost = dRFFinanceApproval.LitigationCost;
                    tbl_DRF_FinanceDetails.FreightCost = dRFFinanceApproval.FreightCost;
                    tbl_DRF_FinanceDetails.RegistrationCost = dRFFinanceApproval.RegistrationCost;
                    tbl_DRF_FinanceDetails.ConsultantCost = dRFFinanceApproval.ConsultantCost;
                    tbl_DRF_FinanceDetails.LegalizationCost = dRFFinanceApproval.LegalizationCost;
                    tbl_DRF_FinanceDetails.TranslationCost = dRFFinanceApproval.TranslationCost;
                    tbl_DRF_FinanceDetails.OtherCost = dRFFinanceApproval.OtherCost;
                    tbl_DRF_FinanceDetails.BECost = dRFFinanceApproval.BECost;
                    tbl_DRF_FinanceDetails.BioCost = dRFFinanceApproval.BioCost;
                    tbl_DRF_FinanceDetails.CTCost = dRFFinanceApproval.CTCost;


                    tbl_DRF_FinanceDetails.FilingCost = dRFFinanceApproval.FilingCost;

                    tbl_DRF_FinanceDetails.Freight = dRFFinanceApproval.Freight;
                    tbl_DRF_FinanceDetails.FreightYearTwo = dRFFinanceApproval.FreightYearTwo;
                    tbl_DRF_FinanceDetails.FreightYearThree = dRFFinanceApproval.FreightYearThree;

                    tbl_DRF_FinanceDetails.TotalContribution = dRFFinanceApproval.TotalContribution;
                    tbl_DRF_FinanceDetails.TotalPercentage = dRFFinanceApproval.TotalPercentage;
                    tbl_DRF_FinanceDetails.NetContribution = dRFFinanceApproval.NetContribution;
                    tbl_DRF_FinanceDetails.NetPercentage = dRFFinanceApproval.NetPercentage;

                    tbl_DRF_FinanceDetails.Createdby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_DRF_FinanceDetails.CreatedDate = DateTime.Today;

                    int data = _dRFFinance.insertDRFFinanceApprovel(tbl_DRF_FinanceDetails);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }

                }

                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }
        }

        string ProjectFolder = "";

        [Authorize(Roles = "Prescriber,HOD Of Dossier")]
        [HttpPost]
        [ActionName("InsertDRFFinalApproval")]
        [Obsolete]
        public async Task<ActionResult> InsertDRFFinalApproval(DRFFinalApproval dRFFinalApproval)
        {

            var alreadyExists = _db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == dRFFinalApproval.InitializationID).Select(t => t.FinalId).FirstOrDefault();
            alreadyExists = alreadyExists == null ? 0 : alreadyExists;
            if (alreadyExists > 0)
            {
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }
            else
            {

                string userName = HttpContext.Session.GetString("CurrentUserName") + " has approved the following DRF : ";
                var result = _db.Tbl_DRF_Initialization.AsNoTracking().Where(t => t.InitializationID == dRFFinalApproval.InitializationID).Select(t => new { t.DRFNo,t.CountryID }).FirstOrDefault();
                string userMessage = "DRF Name : " + result.DRFNo;// "Project Approved : " + result;
                string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago.";

                var Strengthname = (from PD in _db.Tbl_DRF_Initialization
                                    join asp in _db.Tbl_Master_Strength on PD.Strength equals asp.Id
                                    where PD.InitializationID == dRFFinalApproval.InitializationID
                                    select new
                                    {
                                        asp.Strength
                                    }).FirstOrDefault();

                var CountryName = (from PD in _db.Tbl_DRF_Initialization
                                   join asp in _db.Master_Country on PD.CountryID equals asp.Id
                                   where PD.InitializationID == dRFFinalApproval.InitializationID
                                   select new
                                   {
                                       asp.Country
                                   }).FirstOrDefault();

                var projectName = _db.Tbl_DRF_Initialization
                                        .AsNoTracking()
                                        .Where(x => x.InitializationID == dRFFinalApproval.InitializationID)
                                        .Select(x => x.GenericName).FirstOrDefault();

                var DRFCompanyID = _db.Tbl_DRF_Initialization
                                      .AsNoTracking()
                                      .Where(x => x.InitializationID == dRFFinalApproval.InitializationID)
                                      .Select(x => x.CompanyID).FirstOrDefault();

                //string strEmailMessage = userName + "</br>" + "DRF Name : " + result.DRFNo;

                string strEmailMessage = userName + "</br>" + "DRF No : " + result.DRFNo + "</br>" + "DRF Name : " + projectName + "</br>" + "Strength : " + Strengthname.ToString().Replace("{", "").Replace("}", "").Replace("Strength =", "") + "</br>" + "Country : " + CountryName.ToString().Replace("{", "").Replace("}", "").Replace("Country =", "");

                int intCountryID = result.CountryID.Value;
                string strDRFNumber = result.DRFNo;
                if(strDRFNumber.Contains("EM-OLD"))
                {
                    strDRFNumber = "EM-OLD-";
                }
                else
                {
                    strDRFNumber = "EM-";
                }
                string strCountryName = CountryName.Country;//added on date 13/10/2021
                if (ModelState.IsValid)
                {
                    Tbl_DRF_FinalApprovelDetails tbl_DRF_FinalApprovelDetails = new Tbl_DRF_FinalApprovelDetails();
                    tbl_DRF_FinalApprovelDetails.InitializationID = Convert.ToInt32(dRFFinalApproval.InitializationID);
                    tbl_DRF_FinalApprovelDetails.ApprovedReject = dRFFinalApproval.ApprovedReject;
                    tbl_DRF_FinalApprovelDetails.Comment = dRFFinalApproval.Comment;
                    tbl_DRF_FinalApprovelDetails.Createdby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    tbl_DRF_FinalApprovelDetails.CreatedDate = DateTime.Today;

                    int data = _dRFFinal.insertDRFFinalApprovel(tbl_DRF_FinalApprovelDetails);
                    string tempMessage = "";
                    if (data == 1)
                    {

                        if (dRFFinalApproval.ApprovedReject == true)
                        {
                            tempMessage = "Approved";
                            ProjectFolder = "";
                            ProjectFolder = _env.ContentRootPath + _config.GetSection("EmcureFolderPath").Value + strDRFNumber + DateTime.Now.Year + "-" + strCountryName + "-" + dRFFinalApproval.ProjectName.Trim() + "/" + strDRFNumber + DateTime.Now.Year + "-" + strCountryName + "-" + dRFFinalApproval.ProjectName.Trim() + "-000000";
                            //ProjectFolder = _env.ContentRootPath + _config.GetSection("EmcureFolderPath").Value + "EM-" + DateTime.Now.Year + "-" + strCountryName + "-" + dRFFinalApproval.ProjectName.Trim() + "/" + "EM-" + DateTime.Now.Year + "-" + strCountryName + "-" + dRFFinalApproval.ProjectName.Trim() + "-000000";
                            // ProjectFolder =  _config.GetSection("EmcureFolderPath").Value + "EM-" + DateTime.Now.Year + "-" + dRFFinalApproval.ProjectName.Trim() + "/" + "EM-" + DateTime.Now.Year + "-" + dRFFinalApproval.ProjectName.Trim() + "-000000";

                            if (!Directory.Exists(ProjectFolder))
                            {
                                Directory.CreateDirectory(ProjectFolder);
                            }

                            if (dRFFinalApproval.DossierTemplateID == 1 || dRFFinalApproval.DossierTemplateID == 4)//&& dRFViewModel.ContinentID == 3(ASIA) and 40 is manmar
                            {
                                CreateFolders();
                                //General CheckList 
                                _dRFFinal.InsertGeneralCheckList(Convert.ToInt32(dRFFinalApproval.InitializationID), Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today);
                            }

                            if (dRFFinalApproval.DossierTemplateID == 2 && dRFFinalApproval.CountryID != 40) //ACDT Folder Structure except myanmar country
                            {
                                CreateACDTFolders();
                                _dRFFinal.InsertGeneralCheckList(Convert.ToInt32(dRFFinalApproval.InitializationID), Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today);

                            }

                            if (dRFFinalApproval.DossierTemplateID == 3 || (dRFFinalApproval.DossierTemplateID == 2 && dRFFinalApproval.CountryID == 40))//3=National Format   //2=ACDT  //40=Manmar Country
                            {

                                //  CreateFolderStructure1(); 

                                IList<Tbl_Master_FolderStructure> tbl_Master_FolderStructures = new List<Tbl_Master_FolderStructure>();
                                tbl_Master_FolderStructures = _dRFFinal.GetFolderStructureCountryWise(Convert.ToInt32(dRFFinalApproval.DossierTemplateID), Convert.ToInt32(dRFFinalApproval.CountryID));


                                if (tbl_Master_FolderStructures.Count > 0)
                                {

                                    for (int i = 0; i < tbl_Master_FolderStructures.Count; i++)
                                    {
                                        if (tbl_Master_FolderStructures[i].ParentFolderID == 0)
                                        {
                                            if (dRFFinalApproval.DossierTemplateID == 3)
                                            {
                                                _dRFFinal.InsertNationalCheckList(Convert.ToInt32(dRFFinalApproval.InitializationID), tbl_Master_FolderStructures[i].FolderName, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today, Convert.ToInt32(0));
                                            }
                                            else if (dRFFinalApproval.DossierTemplateID == 2 && dRFFinalApproval.CountryID == 40)
                                            {
                                                _dRFFinal.InsertNationalCheckList(Convert.ToInt32(dRFFinalApproval.InitializationID), tbl_Master_FolderStructures[i].FolderName, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today, Convert.ToInt32(0));//this is general checklist for ACDT Myanmar 
                                            }
                                        }
                                        else
                                        {
                                            if (dRFFinalApproval.DossierTemplateID == 3)
                                            {
                                                var ParentFolderName = (from TMFS in _db.Tbl_Master_FolderStructure
                                                                        where TMFS.CountryID == Convert.ToInt32(dRFFinalApproval.CountryID) && TMFS.DossierTemplateID == 3 && TMFS.Id == tbl_Master_FolderStructures[i].ParentFolderID
                                                                        select new
                                                                        {
                                                                            TMFS.FolderName
                                                                        }).FirstOrDefault();

                                                var ParentID = (from TTC in _db.Tbl_Transaction_CheckList
                                                                where TTC.Name == ParentFolderName.FolderName && TTC.DRFID == Convert.ToInt32(dRFFinalApproval.InitializationID)
                                                                select new
                                                                {
                                                                    TTC.TransactionID
                                                                }).FirstOrDefault();

                                                _dRFFinal.InsertNationalCheckList(Convert.ToInt32(dRFFinalApproval.InitializationID), tbl_Master_FolderStructures[i].FolderName, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today, Convert.ToInt32(ParentID.TransactionID));
                                            }
                                            else if (dRFFinalApproval.DossierTemplateID == 2 && dRFFinalApproval.CountryID == 40)
                                            {
                                                var ParentFolderName = (from TMFS in _db.Tbl_Master_FolderStructure
                                                                        where TMFS.CountryID == Convert.ToInt32(dRFFinalApproval.CountryID) && TMFS.DossierTemplateID == 2 && TMFS.Id == tbl_Master_FolderStructures[i].ParentFolderID
                                                                        select new
                                                                        {
                                                                            TMFS.FolderName
                                                                        }).FirstOrDefault();

                                                var ParentID = (from TTC in _db.Tbl_Transaction_CheckList
                                                                where TTC.Name == ParentFolderName.FolderName && TTC.DRFID == Convert.ToInt32(dRFFinalApproval.InitializationID)
                                                                select new
                                                                {
                                                                    TTC.TransactionID
                                                                }).FirstOrDefault();

                                                _dRFFinal.InsertNationalCheckList(Convert.ToInt32(dRFFinalApproval.InitializationID), tbl_Master_FolderStructures[i].FolderName, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today, Convert.ToInt32(ParentID.TransactionID));
                                            }
                                        }

                                        if (dRFFinalApproval.DossierTemplateID == 2)
                                        {
                                            CreateFolderStructure(ProjectFolder, tbl_Master_FolderStructures[i].FolderPath);
                                        }
                                        //this is hide for only national format 
                                        // CreateFolderStructure(ProjectFolder, tbl_Master_FolderStructures[i].FolderPath);


                                        //if(tbl_Master_FolderStructures[i].ParentFolderID==0)
                                        //{
                                        //    CreateFolderStructure(ProjectFolder,tbl_Master_FolderStructures[i].FolderName);
                                        //}
                                        //else
                                        //{
                                        //    var mainFolderName = (from TMF in _db.Tbl_Master_FolderStructure
                                        //                          where TMF.Id == tbl_Master_FolderStructures[i].ParentFolderID
                                        //                          select new
                                        //                          {
                                        //                              TMF.FolderName
                                        //                          }
                                        //                        ).FirstOrDefault();

                                        //    var NewFolderPath = ProjectFolder + @"\'"+mainFolderName+"'";
                                        //    CreateFolderStructure(NewFolderPath,tbl_Master_FolderStructures[i].FolderName);

                                        //}

                                    }
                                }

                                //if(dRFFinalApproval.DossierTemplateID == 2 && dRFFinalApproval.CountryID == 40)
                                //{
                                //    _dRFFinal.InsertGeneralCheckList(Convert.ToInt32(dRFFinalApproval.InitializationID), Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today);
                                //}

                            }

                            IList<DRFTaskSubTaskOutput> drFTaskSubTaskOutputs = new List<DRFTaskSubTaskOutput>();
                            drFTaskSubTaskOutputs = _dRFFinal.GetMixedTaskSubTaskListForDRFInsertion();

                            for (int i = 0; i < drFTaskSubTaskOutputs.Count; i++)
                            {
                                TaskSubTaskInputs taskSubTaskInputs = new TaskSubTaskInputs();

                                taskSubTaskInputs.TaskOrder = drFTaskSubTaskOutputs[i].TaskOrder;
                                taskSubTaskInputs.TaskName = drFTaskSubTaskOutputs[i].TaskName;
                                //taskSubTaskInputs.SortOrder = drFTaskSubTaskOutputs[i].TaskOrder;
                                var tempList = (from TMST in _db.Tbl_Master_SubTask
                                                join TMT in _db.Tbl_Master_Task on TMST.TaskID equals TMT.TaskID
                                                where TMST.SubTaskName == drFTaskSubTaskOutputs[i].TaskName && TMST.SubTaskID == drFTaskSubTaskOutputs[i].SubTaskID
                                                select new
                                                {
                                                    TMST.SubTaskName,
                                                    TMT.TaskName
                                                }).ToList();

                                if (tempList.Count > 0)
                                {

                                    var tempID = (from TMPM in _db.Tbl_Master_ProjectTask_Mapping
                                                  where TMPM.TaskName == tempList[0].TaskName && TMPM.Drfid == Convert.ToInt32(dRFFinalApproval.InitializationID) && TMPM.Action == "DRF"
                                                  select new
                                                  {
                                                      TMPM.ProjectTaskMappingID
                                                  }).ToList();

                                    taskSubTaskInputs.ParentID = Convert.ToInt32(tempID[0].ProjectTaskMappingID);
                                    taskSubTaskInputs.Type = "task";

                                }
                                else
                                {
                                    taskSubTaskInputs.ParentID = 0;
                                    //taskSubTaskInputs.Type = "project";

                                    //CHECK ANY CHILD SUBTASK IS FOUND OR NOT
                                    var checkchild = (from TMST in _db.Tbl_Master_SubTask
                                                      join TMT in _db.Tbl_Master_Task on TMST.TaskID equals TMT.TaskID
                                                      where TMT.TaskName == drFTaskSubTaskOutputs[i].TaskName
                                                      select new { TMST.SubTaskName, TMT.TaskName }).ToList();
                                    if (checkchild.Count > 0)
                                    {
                                        taskSubTaskInputs.Type = "project";
                                    }
                                    else
                                    {
                                        taskSubTaskInputs.Type = "task";
                                    }

                                }


                                taskSubTaskInputs.DRFID = Convert.ToInt32(dRFFinalApproval.InitializationID);
                                taskSubTaskInputs.StartDate = DateTime.Today;
                                taskSubTaskInputs.EndDate = DateTime.Today.AddDays(1);
                                taskSubTaskInputs.PriorityID = 1;// Id of Normal Priority.
                                taskSubTaskInputs.Priority = "Normal";
                                taskSubTaskInputs.TaskStatusID = 8;//Initial Status
                                taskSubTaskInputs.TaskStatus = "Initial";
                                taskSubTaskInputs.TaskDuration = 1;
                                taskSubTaskInputs.TotalPercentage = 0;
                                taskSubTaskInputs.Action = "DRF";
                                taskSubTaskInputs.EmpID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                                // taskSubTaskInputs.Type = "";
                                taskSubTaskInputs.IsActive = true;
                                taskSubTaskInputs.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                                taskSubTaskInputs.CreatedDate = DateTime.Today;
                                taskSubTaskInputs.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                                taskSubTaskInputs.ModifiedDate = DateTime.Today;

                                int dataInserted = _dRFFinal.InsertTaskSubTaskDetails(taskSubTaskInputs);
                            }

                        }

                        await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);
                        
                        ModelState.Clear();
                        HttpContext.Session.SetString("CurrentMoleculeCompanyID", DRFCompanyID.ToString());
                        //send email notification code added by yogesh balapure on date 31/03/2021
                        if (Convert.ToBoolean(_config.GetSection("MailSend:IsInitialFinalApproval").Value) == true)
                        {
                            MailDetails(strEmailMessage, "Project Final Approved", intCountryID, "DRF " + tempMessage);
                        }                         


                            return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }

                }

                return Json(new { data = "fail" }, new JsonSerializerSettings());
            }
        }

        public void CreateFolders()
        {
            //Module 1
            string Module1 = ProjectFolder + @"\Module1";
            if (!Directory.Exists(Module1))
            {
                Directory.CreateDirectory(Module1);
            }

            string subModule12 = Module1 + @"\12-Cover";
            if (!Directory.Exists(subModule12))
            {
                Directory.CreateDirectory(subModule12);
            }

            string subModule13 = Module1 + @"\13-Form";
            if (!Directory.Exists(subModule13))
            {
                Directory.CreateDirectory(subModule13);
            }

            string subModule14 = Module1 + @"\14-PI";
            if (!Directory.Exists(subModule14))
            {
                Directory.CreateDirectory(subModule14);
            }

            string subModule141 = subModule14 + @"\141-SPC";
            if (!Directory.Exists(subModule141))
            {
                Directory.CreateDirectory(subModule141);
            }

            string subModule142 = subModule14 + @"\142-Labeling";
            if (!Directory.Exists(subModule142))
            {
                Directory.CreateDirectory(subModule142);
            }

            string subModule143 = subModule14 + @"\143-Leaflet";
            if (!Directory.Exists(subModule143))
            {
                Directory.CreateDirectory(subModule143);
            }

            string subModule144 = subModule14 + @"\144-Artwork";
            if (!Directory.Exists(subModule144))
            {
                Directory.CreateDirectory(subModule144);
            }

            string subModule145 = subModule14 + @"\145-Samples";
            if (!Directory.Exists(subModule145))
            {
                Directory.CreateDirectory(subModule145);
            }

            string subModule15 = Module1 + @"\15-Expert";
            if (!Directory.Exists(subModule15))
            {
                Directory.CreateDirectory(subModule15);
            }

            string subModule151 = subModule15 + @"\151-Quality";
            if (!Directory.Exists(subModule151))
            {
                Directory.CreateDirectory(subModule151);
            }

            string subModule152 = subModule15 + @"\152-Nonclinical";
            if (!Directory.Exists(subModule152))
            {
                Directory.CreateDirectory(subModule152);
            }

            string subModule153 = subModule15 + @"\153-Clinical";
            if (!Directory.Exists(subModule153))
            {
                Directory.CreateDirectory(subModule153);
            }

            string subModule16 = Module1 + @"\16-Environment Risk";
            if (!Directory.Exists(subModule16))
            {
                Directory.CreateDirectory(subModule16);
            }

            string subModule161 = subModule16 + @"\161-Non GMO";
            if (!Directory.Exists(subModule161))
            {
                Directory.CreateDirectory(subModule161);
            }

            string subModule17 = Module1 + @"\17-Pharmacovigilance";
            if (!Directory.Exists(subModule17))
            {
                Directory.CreateDirectory(subModule17);
            }

            string subModule171 = subModule17 + @"\171-Phvig-system";
            if (!Directory.Exists(subModule171))
            {
                Directory.CreateDirectory(subModule171);
            }

            string subModule172 = subModule17 + @"\172-Risk mgmt-system";
            if (!Directory.Exists(subModule172))
            {
                Directory.CreateDirectory(subModule172);
            }

            string subModule18 = Module1 + @"\18-Certificates";
            if (!Directory.Exists(subModule18))
            {
                Directory.CreateDirectory(subModule18);
            }

            string subModule181 = subModule18 + @"\181-GMP";
            if (!Directory.Exists(subModule181))
            {
                Directory.CreateDirectory(subModule181);
            }

            string subModule182 = subModule18 + @"\182-CPP";
            if (!Directory.Exists(subModule182))
            {
                Directory.CreateDirectory(subModule182);
            }

            string subModule183 = subModule18 + @"\183-Analysis Substance";
            if (!Directory.Exists(subModule183))
            {
                Directory.CreateDirectory(subModule183);
            }

            string subModule184 = subModule18 + @"\184-Analysis Excipients";
            if (!Directory.Exists(subModule184))
            {
                Directory.CreateDirectory(subModule184);
            }

            string subModule185 = subModule18 + @"\185-Alcohol Content";
            if (!Directory.Exists(subModule185))
            {
                Directory.CreateDirectory(subModule185);
            }

            string subModule186 = subModule18 + @"\186-Pork Content";
            if (!Directory.Exists(subModule186))
            {
                Directory.CreateDirectory(subModule186);
            }

            string subModule187 = subModule18 + @"\187-Certificate-TSE";
            if (!Directory.Exists(subModule187))
            {
                Directory.CreateDirectory(subModule187);
            }

            string subModule188 = subModule18 + @"\188-Diluent Coloring Agents";
            if (!Directory.Exists(subModule188))
            {
                Directory.CreateDirectory(subModule188);
            }

            string subModule189 = subModule18 + @"\189-Patent Information";
            if (!Directory.Exists(subModule189))
            {
                Directory.CreateDirectory(subModule189);
            }

            string subModule1810 = subModule18 + @"\1810-Letter Access- DMF";
            if (!Directory.Exists(subModule1810))
            {
                Directory.CreateDirectory(subModule1810);
            }

            string subModule19 = Module1 + @"\19-pricing";
            if (!Directory.Exists(subModule19))
            {
                Directory.CreateDirectory(subModule19);
            }

            string subModule191 = subModule19 + @"\191-price-list";
            if (!Directory.Exists(subModule191))
            {
                Directory.CreateDirectory(subModule191);
            }

            //Module 2

            string Module2 = ProjectFolder + @"\Module2";
            if (!Directory.Exists(Module2))
            {
                Directory.CreateDirectory(Module2);
            }

            string subModule22 = Module2 + @"\22-intro";
            if (!Directory.Exists(subModule22))
            {
                Directory.CreateDirectory(subModule22);
            }

            string subModule23 = Module2 + @"\23-qos";
            if (!Directory.Exists(subModule23))
            {
                Directory.CreateDirectory(subModule23);
            }

            string subModule24 = Module2 + @"\24-nonclin-over";
            if (!Directory.Exists(subModule24))
            {
                Directory.CreateDirectory(subModule24);
            }

            string subModule25 = Module2 + @"\25-clin-over";
            if (!Directory.Exists(subModule25))
            {
                Directory.CreateDirectory(subModule25);
            }

            string subModule26 = Module2 + @"\26-nonclin-sum";
            if (!Directory.Exists(subModule26))
            {
                Directory.CreateDirectory(subModule26);
            }

            string subModule27 = Module2 + @"\27-clin-sum";
            if (!Directory.Exists(subModule27))
            {
                Directory.CreateDirectory(subModule27);
            }

            //Module 3

            string Module3 = ProjectFolder + @"\Module3";
            if (!Directory.Exists(Module3))
            {
                Directory.CreateDirectory(Module3);
            }

            string subModule32 = Module3 + @"\32-body-data";
            if (!Directory.Exists(subModule32))
            {
                Directory.CreateDirectory(subModule32);
            }

            string subModule32a = subModule32 + @"\32a-app";
            if (!Directory.Exists(subModule32a))
            {
                Directory.CreateDirectory(subModule32a);
            }

            string subModule32a1 = subModule32a + @"\32a1-fac-equip";
            if (!Directory.Exists(subModule32a1))
            {
                Directory.CreateDirectory(subModule32a1);
            }

            string subModule32a2 = subModule32a + @"\32a2-advent-agent";
            if (!Directory.Exists(subModule32a2))
            {
                Directory.CreateDirectory(subModule32a2);
            }

            string subModule32a3 = subModule32a + @"\32a3-excip-name-1";
            if (!Directory.Exists(subModule32a3))
            {
                Directory.CreateDirectory(subModule32a3);
            }

            string subModule32p = subModule32 + @"\32p-drug-prod";
            if (!Directory.Exists(subModule32p))
            {
                Directory.CreateDirectory(subModule32p);
            }

            string product1 = subModule32p + @"\product-1";
            if (!Directory.Exists(product1))
            {
                Directory.CreateDirectory(product1);
            }

            string subModule32p1 = product1 + @"\32p1-desc-comp";
            if (!Directory.Exists(subModule32p1))
            {
                Directory.CreateDirectory(subModule32p1);
            }

            string subModule32p2 = product1 + @"\32p2-pharm-dev";
            if (!Directory.Exists(subModule32p2))
            {
                Directory.CreateDirectory(subModule32p2);
            }

            string subModule32p3 = product1 + @"\32p3-manuf";
            if (!Directory.Exists(subModule32p3))
            {
                Directory.CreateDirectory(subModule32p3);
            }

            string subModule32p4 = product1 + @"\32p4-contr-excip";
            if (!Directory.Exists(subModule32p4))
            {
                Directory.CreateDirectory(subModule32p4);
            }

            string excipient1 = subModule32p4 + @"\excipient 1";
            if (!Directory.Exists(excipient1))
            {
                Directory.CreateDirectory(excipient1);
            }

            string subModule32p5 = product1 + @"\32p5-contr-drug-prod";
            if (!Directory.Exists(subModule32p5))
            {
                Directory.CreateDirectory(subModule32p5);
            }

            string subModule32p51 = subModule32p5 + @"\32p51-spec";
            if (!Directory.Exists(subModule32p51))
            {
                Directory.CreateDirectory(subModule32p51);
            }

            string subModule32p52 = subModule32p5 + @"\32p52-analyt-proc";
            if (!Directory.Exists(subModule32p52))
            {
                Directory.CreateDirectory(subModule32p52);
            }

            string subModule32p53 = subModule32p5 + @"\32p53-val-analyt-proc";
            if (!Directory.Exists(subModule32p53))
            {
                Directory.CreateDirectory(subModule32p53);
            }

            string subModule32p54 = subModule32p5 + @"\32p54-batch-analys";
            if (!Directory.Exists(subModule32p54))
            {
                Directory.CreateDirectory(subModule32p54);
            }

            string subModule32p55 = subModule32p5 + @"\32p55-charac-imp";
            if (!Directory.Exists(subModule32p55))
            {
                Directory.CreateDirectory(subModule32p55);
            }

            string subModule32p56 = subModule32p5 + @"\32p56-justif-spec";
            if (!Directory.Exists(subModule32p56))
            {
                Directory.CreateDirectory(subModule32p56);
            }

            string subModule32p6 = product1 + @"\32p6-ref-stand";
            if (!Directory.Exists(subModule32p6))
            {
                Directory.CreateDirectory(subModule32p6);
            }
            string subModule32p7 = product1 + @"\32p7-cont-closure-sys";
            if (!Directory.Exists(subModule32p7))
            {
                Directory.CreateDirectory(subModule32p7);
            }
            string subModule32p8 = product1 + @"\32p8-stab";
            if (!Directory.Exists(subModule32p8))
            {
                Directory.CreateDirectory(subModule32p8);
            }

            string subModule32r = subModule32 + @"\32r-reg-info";
            if (!Directory.Exists(subModule32r))
            {
                Directory.CreateDirectory(subModule32r);
            }

            string subModule32s = subModule32 + @"\32s-drug-sub";
            if (!Directory.Exists(subModule32s))
            {
                Directory.CreateDirectory(subModule32s);
            }

            string substance1manufacturer1 = subModule32s + @"\substance-1-manufacturer-1";
            if (!Directory.Exists(substance1manufacturer1))
            {
                Directory.CreateDirectory(substance1manufacturer1);
            }

            string subModule32s1 = substance1manufacturer1 + @"\32s1-gen-info";
            if (!Directory.Exists(subModule32s1))
            {
                Directory.CreateDirectory(subModule32s1);
            }

            string subModule32s2 = substance1manufacturer1 + @"\32s2-manuf";
            if (!Directory.Exists(subModule32s2))
            {
                Directory.CreateDirectory(subModule32s2);
            }

            string subModule32s3 = substance1manufacturer1 + @"\32s3-charac";
            if (!Directory.Exists(subModule32s3))
            {
                Directory.CreateDirectory(subModule32s3);
            }

            string subModule32s4 = substance1manufacturer1 + @"\32s4-contr-drug-sub";
            if (!Directory.Exists(subModule32s4))
            {
                Directory.CreateDirectory(subModule32s4);
            }

            string subModule32s41 = subModule32s4 + @"\32s41-spec";
            if (!Directory.Exists(subModule32s41))
            {
                Directory.CreateDirectory(subModule32s41);
            }

            string subModule32s42 = subModule32s4 + @"\32s42- analyt-proc";
            if (!Directory.Exists(subModule32s42))
            {
                Directory.CreateDirectory(subModule32s42);
            }

            string subModule32s43 = subModule32s4 + @"\32s43-val-analyt-proc";
            if (!Directory.Exists(subModule32s43))
            {
                Directory.CreateDirectory(subModule32s43);
            }

            string subModule32s44 = subModule32s4 + @"\32s44-batch-analys";
            if (!Directory.Exists(subModule32s44))
            {
                Directory.CreateDirectory(subModule32s44);
            }

            string subModule32s45 = subModule32s4 + @"\32s45-justif-spec";
            if (!Directory.Exists(subModule32s45))
            {
                Directory.CreateDirectory(subModule32s45);
            }

            string subModule32s5 = substance1manufacturer1 + @"\32s5-ref-stand";
            if (!Directory.Exists(subModule32s5))
            {
                Directory.CreateDirectory(subModule32s5);
            }

            string subModule32s6 = substance1manufacturer1 + @"\32s6-cont-closure-sys";
            if (!Directory.Exists(subModule32s6))
            {
                Directory.CreateDirectory(subModule32s6);
            }

            string subModule32s7 = substance1manufacturer1 + @"\32s7-stab";
            if (!Directory.Exists(subModule32s7))
            {
                Directory.CreateDirectory(subModule32s7);
            }

            string subModule33 = Module3 + @"\33-lit-ref";
            if (!Directory.Exists(subModule33))
            {
                Directory.CreateDirectory(subModule33);
            }

            //Module 4

            string Module4 = ProjectFolder + @"\Module4";
            if (!Directory.Exists(Module4))
            {
                Directory.CreateDirectory(Module4);
            }

            string subModule42 = Module4 + @"\42-stud-rep";
            if (!Directory.Exists(subModule42))
            {
                Directory.CreateDirectory(subModule42);
            }

            string subModule421 = subModule42 + @"\421-pharmacol";
            if (!Directory.Exists(subModule421))
            {
                Directory.CreateDirectory(subModule421);
            }

            string subModule4211 = subModule421 + @"\4211-prim-pd";
            if (!Directory.Exists(subModule4211))
            {
                Directory.CreateDirectory(subModule4211);
            }

            string subModule4212 = subModule421 + @"\4212-sec-pd";
            if (!Directory.Exists(subModule4212))
            {
                Directory.CreateDirectory(subModule4212);
            }

            string subModule4213 = subModule421 + @"\4213-safety-pharmacol";
            if (!Directory.Exists(subModule4213))
            {
                Directory.CreateDirectory(subModule4213);
            }

            string subModule4214 = subModule421 + @"\4214-pd-drug-interact";
            if (!Directory.Exists(subModule4214))
            {
                Directory.CreateDirectory(subModule4214);
            }

            string subModule422 = subModule42 + @"\422-pk";
            if (!Directory.Exists(subModule422))
            {
                Directory.CreateDirectory(subModule422);
            }

            string subModule4221 = subModule422 + @"\4221-analyt-met-val";
            if (!Directory.Exists(subModule4221))
            {
                Directory.CreateDirectory(subModule4221);
            }

            string subModule4222 = subModule422 + @"\4222-absorp";
            if (!Directory.Exists(subModule4222))
            {
                Directory.CreateDirectory(subModule4222);
            }

            string subModule4223 = subModule422 + @"\4223-distrib";
            if (!Directory.Exists(subModule4223))
            {
                Directory.CreateDirectory(subModule4223);
            }

            string subModule4224 = subModule422 + @"\4224-metab";
            if (!Directory.Exists(subModule4224))
            {
                Directory.CreateDirectory(subModule4224);
            }

            string subModule4225 = subModule422 + @"\4225-excr";
            if (!Directory.Exists(subModule4225))
            {
                Directory.CreateDirectory(subModule4225);
            }

            string subModule4226 = subModule422 + @"\4226-pk-drug-interact";
            if (!Directory.Exists(subModule4226))
            {
                Directory.CreateDirectory(subModule4226);
            }

            string subModule4227 = subModule422 + @"\4227-other-pk-stud";
            if (!Directory.Exists(subModule4227))
            {
                Directory.CreateDirectory(subModule4227);
            }

            string subModule423 = subModule42 + @"\423-tox";
            if (!Directory.Exists(subModule423))
            {
                Directory.CreateDirectory(subModule423);
            }

            string subModule4231 = subModule423 + @"\4231-single-dose-tox";
            if (!Directory.Exists(subModule4231))
            {
                Directory.CreateDirectory(subModule4231);
            }

            string subModule4232 = subModule423 + @"\4232-repeat-dose-tox";
            if (!Directory.Exists(subModule4232))
            {
                Directory.CreateDirectory(subModule4232);
            }
            string subModule4233 = subModule423 + @"\4233-genotox";
            if (!Directory.Exists(subModule4233))
            {
                Directory.CreateDirectory(subModule4233);
            }
            string subModule42331 = subModule4233 + @"\42331-in-vitro";
            if (!Directory.Exists(subModule42331))
            {
                Directory.CreateDirectory(subModule42331);
            }
            string subModule42332 = subModule4233 + @"\42332-in-vivo";
            if (!Directory.Exists(subModule42331))
            {
                Directory.CreateDirectory(subModule42331);
            }

            string subModule4234 = subModule423 + @"\4234-carcigen";
            if (!Directory.Exists(subModule4234))
            {
                Directory.CreateDirectory(subModule4234);
            }

            string subModule42341 = subModule4234 + @"\42341-lt-stud";
            if (!Directory.Exists(subModule42341))
            {
                Directory.CreateDirectory(subModule42341);
            }

            string subModule42342 = subModule4234 + @"\42342-smt-stud";
            if (!Directory.Exists(subModule42342))
            {
                Directory.CreateDirectory(subModule42342);
            }

            string subModule42343 = subModule4234 + @"\42343-other-stud";
            if (!Directory.Exists(subModule42343))
            {
                Directory.CreateDirectory(subModule42343);
            }

            string subModule4235 = subModule423 + @"\4235-repro-dev-tox";
            if (!Directory.Exists(subModule4235))
            {
                Directory.CreateDirectory(subModule4235);
            }

            string subModule42351 = subModule4235 + @"\42351-fert-embryo-dev";
            if (!Directory.Exists(subModule42351))
            {
                Directory.CreateDirectory(subModule42351);
            }
            string subModule42352 = subModule4235 + @"\42352-embryo-fetal-dev";
            if (!Directory.Exists(subModule42352))
            {
                Directory.CreateDirectory(subModule42352);
            }
            string subModule42353 = subModule4235 + @"\42353-pre-postnatal-dev";
            if (!Directory.Exists(subModule42353))
            {
                Directory.CreateDirectory(subModule42353);
            }
            string subModule42354 = subModule4235 + @"\42354-juv";
            if (!Directory.Exists(subModule42354))
            {
                Directory.CreateDirectory(subModule42354);
            }

            string subModule4236 = subModule423 + @"\4236-loc-tol";
            if (!Directory.Exists(subModule4236))
            {
                Directory.CreateDirectory(subModule4236);
            }

            string subModule4237 = subModule423 + @"\4237-other-tox-stud";
            if (!Directory.Exists(subModule4237))
            {
                Directory.CreateDirectory(subModule4237);
            }

            string subModule42371 = subModule4237 + @"\42371-antigen";
            if (!Directory.Exists(subModule42371))
            {
                Directory.CreateDirectory(subModule42371);
            }
            string subModule42372 = subModule4237 + @"\42372-immunotox";
            if (!Directory.Exists(subModule42372))
            {
                Directory.CreateDirectory(subModule42372);
            }
            string subModule42373 = subModule4237 + @"\42373-mechan-stud";
            if (!Directory.Exists(subModule42373))
            {
                Directory.CreateDirectory(subModule42373);
            }
            string subModule42374 = subModule4237 + @"\42374-dep";
            if (!Directory.Exists(subModule42374))
            {
                Directory.CreateDirectory(subModule42374);
            }
            string subModule42375 = subModule4237 + @"\42375-metab";
            if (!Directory.Exists(subModule42375))
            {
                Directory.CreateDirectory(subModule42375);
            }
            string subModule42376 = subModule4237 + @"\42376-imp";
            if (!Directory.Exists(subModule42376))
            {
                Directory.CreateDirectory(subModule42376);
            }
            string subModule42377 = subModule4237 + @"\42377-other";
            if (!Directory.Exists(subModule42377))
            {
                Directory.CreateDirectory(subModule42377);
            }
            string subModule43 = Module4 + @"\43-lit-ref ";
            if (!Directory.Exists(subModule43))
            {
                Directory.CreateDirectory(subModule43);
            }

            //Module 5
            string Module5 = ProjectFolder + @"\Module5";
            if (!Directory.Exists(Module5))
            {
                Directory.CreateDirectory(Module5);
            }

            string subModule52 = Module5 + @"\52-tab-list";
            if (!Directory.Exists(subModule52))
            {
                Directory.CreateDirectory(subModule52);
            }

            string subModule53 = Module5 + @"\53-clin-stud-rep";
            if (!Directory.Exists(subModule53))
            {
                Directory.CreateDirectory(subModule53);
            }

            string subModule531 = subModule53 + @"\531-rep-biopharm-stud";
            if (!Directory.Exists(subModule531))
            {
                Directory.CreateDirectory(subModule531);
            }

            string subModule5311 = subModule531 + @"\5311-ba-stud-rep";
            if (!Directory.Exists(subModule5311))
            {
                Directory.CreateDirectory(subModule5311);
            }

            string subModule53111 = subModule5311 + @"\study-report-1";
            if (!Directory.Exists(subModule53111))
            {
                Directory.CreateDirectory(subModule53111);
            }

            string subModule53112 = subModule5311 + @"\study-report-2";
            if (!Directory.Exists(subModule53112))
            {
                Directory.CreateDirectory(subModule53112);
            }

            string subModule53113 = subModule5311 + @"\study-report-3";
            if (!Directory.Exists(subModule53113))
            {
                Directory.CreateDirectory(subModule53113);
            }

            string subModule5312 = subModule531 + @"\5312-compar-ba-be-stud-rep";
            if (!Directory.Exists(subModule5312))
            {
                Directory.CreateDirectory(subModule5312);
            }

            string subModule53121 = subModule5312 + @"\study-report-1";
            if (!Directory.Exists(subModule53121))
            {
                Directory.CreateDirectory(subModule53121);
            }

            string subModule53122 = subModule5312 + @"\study-report-2";
            if (!Directory.Exists(subModule53122))
            {
                Directory.CreateDirectory(subModule53122);
            }

            string subModule53123 = subModule5312 + @"\study-report-3";
            if (!Directory.Exists(subModule53123))
            {
                Directory.CreateDirectory(subModule53123);
            }

            string subModule5313 = subModule531 + @"\5313-in-vitro-in-vivo-corr-stud-rep";
            if (!Directory.Exists(subModule5313))
            {
                Directory.CreateDirectory(subModule5313);
            }

            string subModule53131 = subModule5313 + @"\study-report-1";
            if (!Directory.Exists(subModule53131))
            {
                Directory.CreateDirectory(subModule53131);
            }

            string subModule53132 = subModule5313 + @"\study-report-2";
            if (!Directory.Exists(subModule53132))
            {
                Directory.CreateDirectory(subModule53132);
            }

            string subModule53133 = subModule5313 + @"\study-report-3";
            if (!Directory.Exists(subModule53133))
            {
                Directory.CreateDirectory(subModule53133);
            }

            string subModule5314 = subModule531 + @"\5314-bioanalyt-analyt-met";
            if (!Directory.Exists(subModule5314))
            {
                Directory.CreateDirectory(subModule5314);
            }

            string subModule53141 = subModule5314 + @"\study-report-1";
            if (!Directory.Exists(subModule53141))
            {
                Directory.CreateDirectory(subModule53141);
            }

            string subModule53142 = subModule5314 + @"\study-report-2";
            if (!Directory.Exists(subModule53142))
            {
                Directory.CreateDirectory(subModule53142);
            }

            string subModule53143 = subModule5314 + @"\study-report-3";
            if (!Directory.Exists(subModule53143))
            {
                Directory.CreateDirectory(subModule53143);
            }

            string subModule532 = subModule53 + @"\532-rep-stud-pk-human-biomat";
            if (!Directory.Exists(subModule532))
            {
                Directory.CreateDirectory(subModule532);
            }

            string subModule5321 = subModule532 + @"\5321-plasma-prot-bind-stud-rep";
            if (!Directory.Exists(subModule5321))
            {
                Directory.CreateDirectory(subModule5321);
            }

            string subModule53211 = subModule5321 + @"\study-report-1";
            if (!Directory.Exists(subModule53211))
            {
                Directory.CreateDirectory(subModule53211);
            }

            string subModule53212 = subModule5321 + @"\study-report-2";
            if (!Directory.Exists(subModule53212))
            {
                Directory.CreateDirectory(subModule53212);
            }

            string subModule53213 = subModule5321 + @"\study-report-3";
            if (!Directory.Exists(subModule53213))
            {
                Directory.CreateDirectory(subModule53213);
            }

            string subModule5322 = subModule532 + @"\5322-rep-hep-metab-interact-stud";
            if (!Directory.Exists(subModule5322))
            {
                Directory.CreateDirectory(subModule5322);
            }

            string subModule53221 = subModule5322 + @"\study-report-1";
            if (!Directory.Exists(subModule53221))
            {
                Directory.CreateDirectory(subModule53221);
            }

            string subModule53222 = subModule5322 + @"\study-report-2";
            if (!Directory.Exists(subModule53222))
            {
                Directory.CreateDirectory(subModule53222);
            }

            string subModule53223 = subModule5322 + @"\study-report-3";
            if (!Directory.Exists(subModule53223))
            {
                Directory.CreateDirectory(subModule53223);
            }

            string subModule5323 = subModule532 + @"\5322-rep-hep-metab-interact-stud";
            if (!Directory.Exists(subModule5323))
            {
                Directory.CreateDirectory(subModule5323);
            }

            string subModule53231 = subModule5323 + @"\study-report-1";
            if (!Directory.Exists(subModule53231))
            {
                Directory.CreateDirectory(subModule53231);
            }

            string subModule53232 = subModule5323 + @"\study-report-2";
            if (!Directory.Exists(subModule53232))
            {
                Directory.CreateDirectory(subModule53232);
            }

            string subModule53233 = subModule5323 + @"\study-report-3";
            if (!Directory.Exists(subModule53233))
            {
                Directory.CreateDirectory(subModule53233);
            }

            string subModule533 = subModule53 + @"\533-rep-human-pk-stud";
            if (!Directory.Exists(subModule533))
            {
                Directory.CreateDirectory(subModule533);
            }

            string subModule5331 = subModule533 + @"\5331-healthy-subj-pk-init-tol-stud-rep";
            if (!Directory.Exists(subModule5331))
            {
                Directory.CreateDirectory(subModule5331);
            }

            string subModule53311 = subModule5331 + @"\study-report-1";
            if (!Directory.Exists(subModule53311))
            {
                Directory.CreateDirectory(subModule53311);
            }

            string subModule53312 = subModule5331 + @"\study-report-2";
            if (!Directory.Exists(subModule53312))
            {
                Directory.CreateDirectory(subModule53312);
            }

            string subModule53313 = subModule5331 + @"\study-report-3";
            if (!Directory.Exists(subModule53313))
            {
                Directory.CreateDirectory(subModule53313);
            }

            string subModule5332 = subModule533 + @"\5332-patient-pk-init-tol-stud-rep";
            if (!Directory.Exists(subModule5332))
            {
                Directory.CreateDirectory(subModule5332);
            }

            string subModule53321 = subModule5332 + @"\study-report-1";
            if (!Directory.Exists(subModule53321))
            {
                Directory.CreateDirectory(subModule53321);
            }

            string subModule53322 = subModule5332 + @"\study-report-2";
            if (!Directory.Exists(subModule53322))
            {
                Directory.CreateDirectory(subModule53322);
            }

            string subModule53323 = subModule5332 + @"\study-report-3";
            if (!Directory.Exists(subModule53323))
            {
                Directory.CreateDirectory(subModule53323);
            }

            string subModule5333 = subModule533 + @"\5333-intrin-factor-pk-stud-rep";
            if (!Directory.Exists(subModule5333))
            {
                Directory.CreateDirectory(subModule5333);
            }

            string subModule53331 = subModule5333 + @"\study-report-1";
            if (!Directory.Exists(subModule53331))
            {
                Directory.CreateDirectory(subModule53331);
            }
            string subModule53332 = subModule5333 + @"\study-report-2";
            if (!Directory.Exists(subModule53332))
            {
                Directory.CreateDirectory(subModule53332);
            }
            string subModule53333 = subModule5333 + @"\study-report-3";
            if (!Directory.Exists(subModule53333))
            {
                Directory.CreateDirectory(subModule53333);
            }
            string subModule5334 = subModule533 + @"\5334-extrin-factor-pk-stud-rep";
            if (!Directory.Exists(subModule5334))
            {
                Directory.CreateDirectory(subModule5334);
            }
            string subModule53341 = subModule5334 + @"\study-report-1";
            if (!Directory.Exists(subModule53341))
            {
                Directory.CreateDirectory(subModule53341);
            }
            string subModule53342 = subModule5334 + @"\study-report-2";
            if (!Directory.Exists(subModule53342))
            {
                Directory.CreateDirectory(subModule53342);
            }
            string subModule53343 = subModule5334 + @"\study-report-3";
            if (!Directory.Exists(subModule53343))
            {
                Directory.CreateDirectory(subModule53343);
            }
            string subModule5335 = subModule533 + @"\5335-popul-pk-stud-rep";
            if (!Directory.Exists(subModule5335))
            {
                Directory.CreateDirectory(subModule5335);
            }
            string subModule53351 = subModule5335 + @"\study-report-1";
            if (!Directory.Exists(subModule53351))
            {
                Directory.CreateDirectory(subModule53351);
            }
            string subModule53352 = subModule5335 + @"\study-report-2";
            if (!Directory.Exists(subModule53352))
            {
                Directory.CreateDirectory(subModule53352);
            }
            string subModule53353 = subModule5335 + @"\study-report-3";
            if (!Directory.Exists(subModule53353))
            {
                Directory.CreateDirectory(subModule53353);
            }

            string subModule534 = subModule53 + @"\534-rep-human-pd-stud";
            if (!Directory.Exists(subModule534))
            {
                Directory.CreateDirectory(subModule534);
            }

            string subModule5341 = subModule534 + @"\5341-healthy-subj-pd-stud-rep";
            if (!Directory.Exists(subModule5341))
            {
                Directory.CreateDirectory(subModule5341);
            }

            string subModule53411 = subModule5341 + @"\study-report-1";
            if (!Directory.Exists(subModule53411))
            {
                Directory.CreateDirectory(subModule53411);
            }

            string subModule53412 = subModule5341 + @"\study-report-2";
            if (!Directory.Exists(subModule53412))
            {
                Directory.CreateDirectory(subModule53412);
            }

            string subModule53413 = subModule5341 + @"\study-report-3";
            if (!Directory.Exists(subModule53413))
            {
                Directory.CreateDirectory(subModule53413);
            }

            string subModule5342 = subModule534 + @"\5342-patient-pd-stud-rep";
            if (!Directory.Exists(subModule5342))
            {
                Directory.CreateDirectory(subModule5342);
            }

            string subModule53421 = subModule5342 + @"\study-report-1";
            if (!Directory.Exists(subModule53421))
            {
                Directory.CreateDirectory(subModule53421);
            }

            string subModule53422 = subModule5342 + @"\study-report-2";
            if (!Directory.Exists(subModule53422))
            {
                Directory.CreateDirectory(subModule53422);
            }

            string subModule53423 = subModule5342 + @"\study-report-3";
            if (!Directory.Exists(subModule53423))
            {
                Directory.CreateDirectory(subModule53423);
            }

            string subModule535 = subModule53 + @"\535-rep-effic-safety-stud";
            if (!Directory.Exists(subModule535))
            {
                Directory.CreateDirectory(subModule535);
            }

            string subModule5351 = subModule535 + @"\5351-indication-1";
            if (!Directory.Exists(subModule5351))
            {
                Directory.CreateDirectory(subModule5351);
            }

            string subModule53511 = subModule5351 + @"\53511-stud-rep-contr";
            if (!Directory.Exists(subModule53511))
            {
                Directory.CreateDirectory(subModule53511);
            }

            string subModule535111 = subModule53511 + @"\study-report-1";
            if (!Directory.Exists(subModule535111))
            {
                Directory.CreateDirectory(subModule535111);
            }

            string subModule535112 = subModule53511 + @"\study-report-2";
            if (!Directory.Exists(subModule535112))
            {
                Directory.CreateDirectory(subModule535112);
            }

            string subModule535113 = subModule53511 + @"\study-report-3";
            if (!Directory.Exists(subModule535113))
            {
                Directory.CreateDirectory(subModule535113);
            }

            string subModule53512 = subModule5351 + @"\53512-stud-rep-uncontr";
            if (!Directory.Exists(subModule53512))
            {
                Directory.CreateDirectory(subModule53512);
            }

            string subModule535121 = subModule53512 + @"\study-report-1";
            if (!Directory.Exists(subModule535121))
            {
                Directory.CreateDirectory(subModule535121);
            }

            string subModule535122 = subModule53512 + @"\study-report-2";
            if (!Directory.Exists(subModule535122))
            {
                Directory.CreateDirectory(subModule535122);
            }

            string subModule535123 = subModule53512 + @"\study-report-3";
            if (!Directory.Exists(subModule535123))
            {
                Directory.CreateDirectory(subModule535123);
            }

            string subModule53513 = subModule5351 + @"\53513-rep-analys-data-more-one-stud";
            if (!Directory.Exists(subModule53513))
            {
                Directory.CreateDirectory(subModule53513);
            }

            string subModule535131 = subModule53513 + @"\study-report-1";
            if (!Directory.Exists(subModule535131))
            {
                Directory.CreateDirectory(subModule535131);
            }

            string subModule535132 = subModule53513 + @"\study-report-2";
            if (!Directory.Exists(subModule535132))
            {
                Directory.CreateDirectory(subModule535132);
            }

            string subModule535133 = subModule53513 + @"\study-report-3";
            if (!Directory.Exists(subModule535133))
            {
                Directory.CreateDirectory(subModule535133);
            }

            string subModule53514 = subModule5351 + @"\53514-other-stud-rep";
            if (!Directory.Exists(subModule53514))
            {
                Directory.CreateDirectory(subModule53514);
            }

            string subModule535141 = subModule53514 + @"\study-report-1";
            if (!Directory.Exists(subModule535141))
            {
                Directory.CreateDirectory(subModule535141);
            }

            string subModule535142 = subModule53514 + @"\study-report-2";
            if (!Directory.Exists(subModule535142))
            {
                Directory.CreateDirectory(subModule535142);
            }

            string subModule535143 = subModule53514 + @"\study-report-3";
            if (!Directory.Exists(subModule535143))
            {
                Directory.CreateDirectory(subModule535143);
            }

            string subModule536 = subModule53 + @"\536-postmark-exp";
            if (!Directory.Exists(subModule536))
            {
                Directory.CreateDirectory(subModule536);
            }

            string subModule537 = subModule53 + @"\537-crf-ipl";
            if (!Directory.Exists(subModule537))
            {
                Directory.CreateDirectory(subModule537);
            }

            string subModule5371 = subModule537 + @"\study-report-1";
            if (!Directory.Exists(subModule5371))
            {
                Directory.CreateDirectory(subModule5371);
            }

            string subModule5372 = subModule537 + @"\study-report-2";
            if (!Directory.Exists(subModule5372))
            {
                Directory.CreateDirectory(subModule5372);
            }

            string subModule5373 = subModule537 + @"\study-report-3";
            if (!Directory.Exists(subModule5373))
            {
                Directory.CreateDirectory(subModule5373);
            }

            string subModule54 = Module5 + @"\54-lit-ref";
            if (!Directory.Exists(subModule54))
            {
                Directory.CreateDirectory(subModule54);
            }

        }


        protected virtual string IncreamentUnique(string prefix, string unique)
        {
            if (unique != null && unique != "")
            {
                var suffixNum = unique.Split(prefix);
                int increment = Convert.ToInt32(suffixNum[1]) + 1;
                return string.Concat(prefix, increment.ToString("000000"));
            }
            else
            {
                return "";
            }
        }

        //[Authorize(Roles = "Prescriber")]
        [Authorize]
        [HttpPost]
        [ActionName("GetPIDFListForAttached")]
        public ActionResult GetPIDFListForAttached(PIDFByCountryInDRF pIDFByCountryInDRF)
        {
            var PIDFLists = (from pidf in _db.Tbl_PIDF_HeaderNew
                             join pidfc in _db.Tbl_PIDF_CountryDetailsNew on pidf.PidfID equals pidfc.PidfID
                             join cc in _db.Master_Continent on pidfc.ContinentID equals cc.Id
                             join ccc in _db.Master_Country on pidfc.CountryID equals ccc.Id
                             join pidfs in _db.Tbl_Pidf_Strength on pidfc.StrengthID equals pidfs.Id
                             join mu in _db.Tbl_Master_Unit on pidfs.UnitID equals mu.Id
                             where pidfc.CountryID == pIDFByCountryInDRF.CountryID && pidf.PidfStatusID == 16 //16=filanl approved//5=approved pidf
                             select new
                             {
                                 Id = pidf.PidfID,
                                 ProductId = pidf.PIDFNo,
                                 ProductName = pidf.ProjectorProductName,
                                 Region = cc.Continent,
                                 Country = ccc.Country,
                                 Strength =pidfs.PidfStrength +' '+ mu.UnitName,
                                 PackSize=pidfc.PackSizeName,
                                 Packing=pidfc.PackingName
                             }).ToList();


            return Json(new { success = true, Data = PIDFLists });

        }

       // [Authorize(Roles = "Prescriber")]
       [Authorize]
        [HttpPost]
        [ActionName("AttachedPIDFDetails")]
        public ActionResult AttachedPIDFDetails(GetDRFAttachedPIDF getDRFAttachedPIDF)
        {
            IList<PIDFDetailsNew> AttachedPIDFList = new List<PIDFDetailsNew>();
            AttachedPIDFList = _drfINI.GetAttachedPIDFList(getDRFAttachedPIDF.DRFID, getDRFAttachedPIDF.CountryID);
            return Json(new { data = AttachedPIDFList });
        }

        [Authorize]
        [HttpGet]
        [ActionName("GetFinalApprovedProjectList")]
        public ActionResult GetFinalApprovedProjectList()
        {
            HttpContext.Session.SetString("Action", "DRF");
            HttpContext.Session.SetString("ListPageURL", "/DRFInitialization/ProjectList");
            HttpContext.Session.SetString("DetailsPageURL", "/DRFInitialization/ProjectShowDetails");
            HttpContext.Session.SetString("ButtonDetailPage", "Dossier Details");
            HttpContext.Session.SetString("ButtonListPage", "Dossier List");
            HttpContext.Session.SetString("ButtonSummaryPage", "Dossier List");
            HttpContext.Session.SetString("SummaryPageURL", "/DRFInitialization/ProjectList");
            HttpContext.Session.SetString("GanttSummaryPageTitle", "Dossier Summary");

            //IList<Tbl_DRF_Initialization> list = new List<Tbl_DRF_Initialization>();
            //list = _drfINI.GetFinalApprovedProjectList();
            //return Json(new { data = list });
            int tempUserCompanyID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID"));
            var result = _drfINI.GetFinalApprovedDRFList(Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), tempUserCompanyID);
            return Json( new { data = result });
        }

        // [Authorize(Roles = "Prescriber,Initial Approval")]
        [Authorize(Roles = "Prescriber,HOD Of Dossier")]
        [HttpPost]
        [ActionName("UpdateMultipleDRFInitialApproval")]
        [Obsolete]
        public async Task<ActionResult> UpdateMultipleDRFInitialApproval(MultipleDRFInitialApproval multipleDRFInitialApproval)
        {
            
                if (ModelState.IsValid)
                {
                    if (multipleDRFInitialApproval.InitializationIDList.Length > 0)
                    {
                        var IDList = JsonConvert.DeserializeObject<List<string>>(multipleDRFInitialApproval.InitializationIDList);

                        for (int i = 0; i < IDList.Count; i++)
                        {
                            var alreadyExists = _db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == Convert.ToInt32(IDList[i])).Select(t => t.Id).FirstOrDefault();
                        
                            if (alreadyExists > 0)
                            {
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                Tbl_DRF_Initialization tbl_DRF_Initialization = new Tbl_DRF_Initialization();

                                tbl_DRF_Initialization.InitializationID = Convert.ToInt32(IDList[i]);

                            string tempMessage = " ";
                                if (multipleDRFInitialApproval.InitialApproval == true)
                                {
                                    tbl_DRF_Initialization.StatusID = 12;
                                    tbl_DRF_Initialization.Status = "Initial Approved";
                                tempMessage = "Approved";
                                    //tbl_DRF_Initialization.StatusID = 24;
                                    //tbl_DRF_Initialization.Status = "HOD of Dossier Approved";
                            }
                                else
                                {
                                    tbl_DRF_Initialization.StatusID = 10;
                                    tbl_DRF_Initialization.Status = "Rejected";
                                tempMessage = "Rejected";
                                }

                                tbl_DRF_Initialization.InitialApproveRejectComment = multipleDRFInitialApproval.Comment;

                                tbl_DRF_Initialization.Modifiedby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                                tbl_DRF_Initialization.ModifiedDate = DateTime.Today;

                                //int data = _drfINI.updateDRFInitialApproval(tbl_DRF_Initialization);

                                Tbl_DRF_FormApprovals tbl_DRF_FormApprovals = new Tbl_DRF_FormApprovals();

                                tbl_DRF_FormApprovals.InitializationID = Convert.ToInt32(IDList[i]);
                                tbl_DRF_FormApprovals.HODofDossierApproval = multipleDRFInitialApproval.InitialApproval;
                                tbl_DRF_FormApprovals.HODCreatedBy= Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                                tbl_DRF_FormApprovals.HODCreatedDate = DateTime.Now;
                                tbl_DRF_FormApprovals.Comment = multipleDRFInitialApproval.Comment;
                                int data1 = _drfINI.UpdateDRFApprovals(tbl_DRF_FormApprovals, HttpContext.Session.GetString("CurrentUserRole"), 23); //23 ==line manager approved
                           

                                //Add realtime notification method
                                string userName = HttpContext.Session.GetString("CurrentUserName");
                                var result = _db.Tbl_DRF_Initialization.AsNoTracking().Where(t => t.InitializationID == tbl_DRF_Initialization.InitializationID).Select(t => new { t.DRFNo, t.CountryID }).FirstOrDefault();
                            string userMessage = "DRF " + tempMessage + " : " + result.DRFNo;
                            //string userMessage = "DRF Approved : " + result.DRFNo;

                            var Strengthname = (from PD in _db.Tbl_DRF_Initialization
                                                join asp in _db.Tbl_Master_Strength on PD.Strength equals asp.Id
                                                where PD.InitializationID == Convert.ToInt32(IDList[i])
                                                select new
                                                {
                                                    asp.Strength
                                                }).FirstOrDefault();

                            var CountryName = (from PD in _db.Tbl_DRF_Initialization
                                               join asp in _db.Master_Country on PD.CountryID equals asp.Id
                                               where PD.InitializationID == Convert.ToInt32(IDList[i])
                                               select new
                                               {
                                                   asp.Country
                                               }).FirstOrDefault();

                            var projectName = _db.Tbl_DRF_Initialization
                                                    .AsNoTracking()
                                                    .Where(x => x.InitializationID == Convert.ToInt32(IDList[i]))
                                                    .Select(x => x.GenericName).FirstOrDefault();

                            var DRFCompanyID = _db.Tbl_DRF_Initialization
                                      .AsNoTracking()
                                      .Where(x => x.InitializationID == Convert.ToInt32(IDList[i]))
                                      .Select(x => x.CompanyID).FirstOrDefault();

                           

                            string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago.";
                                if (multipleDRFInitialApproval.InitialApproval == true)
                                {
                                    await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);
                                }

                                int intCountryID = result.CountryID.Value;//use for only email
                                string userMessage1 = "DRF Name : " + result.DRFNo;
                               // string strEmailMessage = userName + " (HOD of Dossier)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF Name : " + result.DRFNo;
                            string strEmailMessage = userName + " (HOD of Dossier)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF No : " + result.DRFNo + "</br>" + "DRF Name : " + projectName + "</br>" + "Strength : " + Strengthname.ToString().Replace("{", "").Replace("}", "").Replace("Strength =", "") + "</br>" + "Country : " + CountryName.ToString().Replace("{", "").Replace("}", "").Replace("Country =", "");

                            HttpContext.Session.SetString("CurrentMoleculeCompanyID", DRFCompanyID.ToString());

                            //send email notification code added by yogesh balapure on date 31/03/2021
                            if (Convert.ToBoolean(_config.GetSection("MailSend:IsMultipleDRFInitialApproval").Value) == true)
                                {
                                    MailDetails(strEmailMessage, "Project Initial Approved", intCountryID, "DRF Approved");
                                }                               

                            }
                        }
                    }

                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }

                return Json(new { data = "fail" }, new JsonSerializerSettings());
            
        }


        [Authorize(Roles = "Prescriber,HOD Of Dossier")]
        [HttpPost]
        [ActionName("UpdateMultipleDRFFinalApproval")]
        public ActionResult UpdateMultipleDRFFinalApproval(MultipleDRFFinalApproval multipleDRFFinalApproval)
        {

            if (ModelState.IsValid)
            {
                if (multipleDRFFinalApproval.InitializationIDList.Length > 0)
                {
                    var IDList = JsonConvert.DeserializeObject<List<string>>(multipleDRFFinalApproval.InitializationIDList);

                    for (int i = 0; i < IDList.Count; i++)
                    {
                        var alreadyExists = _db.Tbl_DRFDataMaster.AsNoTracking().Where(t => t.InitializationId == Convert.ToInt32(IDList[i])).Select(t => t.FinalId).FirstOrDefault();

                        alreadyExists = alreadyExists == null ? 0 : alreadyExists;
                        if (alreadyExists > 0)
                        {
                            return Json(new { data = "success" }, new JsonSerializerSettings());
                        }
                        else
                        {
                         
                            Tbl_DRF_FinalApprovelDetails tbl_DRF_FinalApprovelDetails = new Tbl_DRF_FinalApprovelDetails();

                            tbl_DRF_FinalApprovelDetails.InitializationID = Convert.ToInt32(IDList[i]);

                            //if (multipleDRFFinalApproval.ApprovedReject == true)
                            //{
                            //    tbl_DRF_FinalApprovelDetails.StatusID = 16;
                            //    tbl_DRF_FinalApprovelDetails.Status = "Final Approved";
                            //}
                            //else
                            //{
                            //    tbl_DRF_FinalApprovelDetails.StatusID = 17;
                            //    tbl_DRF_FinalApprovelDetails.Status = "Final Rejected";
                            //}

                            tbl_DRF_FinalApprovelDetails.ApprovedReject = multipleDRFFinalApproval.ApprovedReject;
                            tbl_DRF_FinalApprovelDetails.Comment = multipleDRFFinalApproval.Comment;
                            tbl_DRF_FinalApprovelDetails.Createdby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FinalApprovelDetails.CreatedDate = DateTime.Today;

                            int data = _dRFFinal.insertDRFFinalApprovel(tbl_DRF_FinalApprovelDetails);

                            if (data == 1)
                            {

                                if (multipleDRFFinalApproval.ApprovedReject == true)
                                {
                                    var CountryName = (from PD in _db.Tbl_DRF_Initialization
                                                       join asp in _db.Master_Country on PD.CountryID equals asp.Id
                                                       where PD.InitializationID == Convert.ToInt32(IDList[i])
                                                       select new
                                                       {
                                                           asp.Country,
                                                           PD.DRFNo
                                                       }).FirstOrDefault();

                                    var Details = (from a in _db.Tbl_DRF_Initialization
                                                   join b in _db.Tbl_DRFDataMaster on a.InitializationID equals b.InitializationId
                                                   join c in _db.Tbl_DRF_IP_Details on b.IPDetailsId equals c.Id
                                                   where a.InitializationID == Convert.ToInt32(IDList[i])
                                                   select new
                                                   {
                                                       a.DossierTemplateID,
                                                       c.ProjectName,
                                                       a.CountryID
                                                   }).FirstOrDefault();

                                    string strCountryName = CountryName.Country;
                                    string strDRFNumber = CountryName.DRFNo;
                                    if (strDRFNumber.Contains("EM-OLD"))
                                    {
                                        strDRFNumber = "EM-OLD-";
                                    }
                                    else
                                    {
                                        strDRFNumber = "EM-";
                                    }
                                    ProjectFolder = "";
                                    //ProjectFolder = _config.GetSection("EmcureFolderPath").Value + "EM-" + DateTime.Now.Year + "-" + Details.ProjectName.Trim() + "/" + "EM-" + DateTime.Now.Year + "-" + Details.ProjectName.Trim() + "-000000";
                                    //ProjectFolder = _env.ContentRootPath + _config.GetSection("EmcureFolderPath").Value + "EM-" + DateTime.Now.Year + "-" + strCountryName + "-"  + Details.ProjectName.Trim() + "/" + "EM-" + DateTime.Now.Year + "-" + strCountryName + "-" + Details.ProjectName.Trim() + "-000000";
                                    ProjectFolder = _env.ContentRootPath + _config.GetSection("EmcureFolderPath").Value + strDRFNumber + DateTime.Now.Year + "-" + strCountryName + "-" + Details.ProjectName.Trim() + "/" + strDRFNumber + DateTime.Now.Year + "-" + strCountryName + "-" + Details.ProjectName.Trim() + "-000000";

                                    if (!Directory.Exists(ProjectFolder))
                                    {
                                        Directory.CreateDirectory(ProjectFolder);
                                    }

                                    if (Details.DossierTemplateID == 1 || Details.DossierTemplateID == 4)//&& dRFViewModel.ContinentID == 3(ASIA) and 40 is manmar
                                    {
                                        CreateFolders();
                                        //General CheckList
                                        _dRFFinal.InsertGeneralCheckList(Convert.ToInt32(IDList[i]), Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today);
                                    }

                                    if (Details.DossierTemplateID == 2 && Details.CountryID != 40) //ACDT Folder Structure except myanmar country
                                    {
                                        CreateACDTFolders();
                                        _dRFFinal.InsertGeneralCheckList(Convert.ToInt32(IDList[i]), Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today);

                                    }

                                    if (Details.DossierTemplateID == 3 || (Details.DossierTemplateID == 2 && Details.CountryID == 40))//3=National Format   //2=ACDT  //40=Manmar Country
                                    {
                                        IList<Tbl_Master_FolderStructure> tbl_Master_FolderStructures = new List<Tbl_Master_FolderStructure>();
                                        tbl_Master_FolderStructures = _dRFFinal.GetFolderStructureCountryWise(Convert.ToInt32(Details.DossierTemplateID), Convert.ToInt32(Details.CountryID));

                                        if (tbl_Master_FolderStructures.Count > 0)
                                        {
                                            for (int k = 0; k < tbl_Master_FolderStructures.Count; k++)
                                            {
                                                // _dRFFinal.InsertNationalCheckList(Convert.ToInt32(IDList[i]), tbl_Master_FolderStructures[i].FolderName, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today);
                                                if (tbl_Master_FolderStructures[k].ParentFolderID == 0)
                                                {
                                                    if (Details.DossierTemplateID == 3)
                                                    {
                                                        _dRFFinal.InsertNationalCheckList(Convert.ToInt32(IDList[i]), tbl_Master_FolderStructures[k].FolderName, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today, Convert.ToInt32(0));
                                                    }
                                                    else if (Details.DossierTemplateID == 2 && Details.CountryID == 40)
                                                    {
                                                        _dRFFinal.InsertNationalCheckList(Convert.ToInt32(IDList[i]), tbl_Master_FolderStructures[k].FolderName, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today, Convert.ToInt32(0));//this is general checklist for ACDT Myanmar 
                                                    }
                                                }
                                                else
                                                {
                                                    if (Details.DossierTemplateID == 3)
                                                    {
                                                        var ParentFolderName = (from TMFS in _db.Tbl_Master_FolderStructure
                                                                                where TMFS.CountryID == Convert.ToInt32(Details.CountryID) && TMFS.DossierTemplateID == 3 && TMFS.Id == tbl_Master_FolderStructures[k].ParentFolderID
                                                                                select new
                                                                                {
                                                                                    TMFS.FolderName
                                                                                }).FirstOrDefault();

                                                        var ParentID = (from TTC in _db.Tbl_Transaction_CheckList
                                                                        where TTC.Name == ParentFolderName.FolderName && TTC.DRFID == Convert.ToInt32(IDList[i])
                                                                        select new
                                                                        {
                                                                            TTC.TransactionID
                                                                        }).FirstOrDefault();


                                                        _dRFFinal.InsertNationalCheckList(Convert.ToInt32(IDList[i]), tbl_Master_FolderStructures[k].FolderName, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today, Convert.ToInt32(ParentID.TransactionID));
                                                    }
                                                    else if (Details.DossierTemplateID == 2 && Details.CountryID == 40)
                                                    {
                                                        var ParentFolderName = (from TMFS in _db.Tbl_Master_FolderStructure
                                                                                where TMFS.CountryID == Convert.ToInt32(Details.CountryID) && TMFS.DossierTemplateID == 2 && TMFS.Id == tbl_Master_FolderStructures[k].ParentFolderID
                                                                                select new
                                                                                {
                                                                                    TMFS.FolderName
                                                                                }).FirstOrDefault();

                                                        var ParentID = (from TTC in _db.Tbl_Transaction_CheckList
                                                                        where TTC.Name == ParentFolderName.FolderName && TTC.DRFID == Convert.ToInt32(IDList[i])
                                                                        select new
                                                                        {
                                                                            TTC.TransactionID
                                                                        }).FirstOrDefault();

                                                        _dRFFinal.InsertNationalCheckList(Convert.ToInt32(IDList[i]), tbl_Master_FolderStructures[k].FolderName, Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today, Convert.ToInt32(ParentID.TransactionID));
                                                    }
                                                }
                                                if (Details.DossierTemplateID == 2)
                                                {
                                                    CreateFolderStructure(ProjectFolder, tbl_Master_FolderStructures[k].FolderPath);
                                                }

                                                //this is hide for only national format 
                                                // CreateFolderStructure(ProjectFolder, tbl_Master_FolderStructures[i].FolderPath);
                                            }
                                        }
                                        //if (Details.DossierTemplateID == 2 && Details.CountryID == 40)
                                        //{
                                        //    _dRFFinal.InsertGeneralCheckList(Convert.ToInt32(IDList[i]), Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId")), DateTime.Today);
                                        //}
                                    }

                                    IList<DRFTaskSubTaskOutput> drFTaskSubTaskOutputs = new List<DRFTaskSubTaskOutput>();
                                    drFTaskSubTaskOutputs = _dRFFinal.GetMixedTaskSubTaskListForDRFInsertion();

                                    for (int j = 0; j < drFTaskSubTaskOutputs.Count; j++)
                                    {
                                        TaskSubTaskInputs taskSubTaskInputs = new TaskSubTaskInputs();

                                        taskSubTaskInputs.TaskOrder = drFTaskSubTaskOutputs[j].TaskOrder;
                                        taskSubTaskInputs.TaskName = drFTaskSubTaskOutputs[j].TaskName;
                                        //taskSubTaskInputs.SortOrder = drFTaskSubTaskOutputs[i].TaskOrder;
                                        var tempList = (from TMST in _db.Tbl_Master_SubTask
                                                        join TMT in _db.Tbl_Master_Task on TMST.TaskID equals TMT.TaskID
                                                        where TMST.SubTaskName == drFTaskSubTaskOutputs[j].TaskName && TMST.SubTaskID == drFTaskSubTaskOutputs[j].SubTaskID
                                                        select new
                                                        {
                                                            TMST.SubTaskName,
                                                            TMT.TaskName
                                                        }).ToList();

                                        if (tempList.Count > 0)
                                        {

                                            var tempID = (from TMPM in _db.Tbl_Master_ProjectTask_Mapping
                                                          where TMPM.TaskName == tempList[0].TaskName && TMPM.Drfid == Convert.ToInt32(IDList[i]) && TMPM.Action == "DRF"
                                                          select new
                                                          {
                                                              TMPM.ProjectTaskMappingID
                                                          }).ToList();

                                            taskSubTaskInputs.ParentID = Convert.ToInt32(tempID[0].ProjectTaskMappingID);
                                            taskSubTaskInputs.Type = "task";

                                        }
                                        else
                                        {
                                            taskSubTaskInputs.ParentID = 0;
                                            // taskSubTaskInputs.Type = "project";
                                            //CHECK ANY CHILD SUBTASK IS FOUND OR NOT
                                            var checkchild = (from TMST in _db.Tbl_Master_SubTask
                                                              join TMT in _db.Tbl_Master_Task on TMST.TaskID equals TMT.TaskID
                                                              where TMT.TaskName == drFTaskSubTaskOutputs[j].TaskName
                                                              select new { TMST.SubTaskName, TMT.TaskName }).ToList();
                                            if (checkchild.Count > 0)
                                            {
                                                taskSubTaskInputs.Type = "project";
                                            }
                                            else
                                            {
                                                taskSubTaskInputs.Type = "task";
                                            }
                                        }


                                        taskSubTaskInputs.DRFID = Convert.ToInt32(IDList[i]);
                                        taskSubTaskInputs.StartDate = DateTime.Today;
                                        taskSubTaskInputs.EndDate = DateTime.Today.AddDays(1);
                                        taskSubTaskInputs.PriorityID = 1;// Id of Normal Priority.
                                        taskSubTaskInputs.Priority = "Normal";
                                        taskSubTaskInputs.TaskStatusID = 8;//Initial Status
                                        taskSubTaskInputs.TaskStatus = "Initial";
                                        taskSubTaskInputs.TaskDuration = 1;
                                        taskSubTaskInputs.TotalPercentage = 0;
                                        taskSubTaskInputs.Action = "DRF";
                                        taskSubTaskInputs.EmpID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                                        // taskSubTaskInputs.Type = "";
                                        taskSubTaskInputs.IsActive = true;
                                        taskSubTaskInputs.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                                        taskSubTaskInputs.CreatedDate = DateTime.Today;
                                        taskSubTaskInputs.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                                        taskSubTaskInputs.ModifiedDate = DateTime.Today;

                                        int dataInserted = _dRFFinal.InsertTaskSubTaskDetails(taskSubTaskInputs);
                                    }

                                }


                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                    }
                }

                ModelState.Clear();
                return Json(new { data = "success" }, new JsonSerializerSettings());
            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        public IActionResult DRFUpdateDetails(int ID)
        {
            @ViewBag.UpdateInitializationID = ID;
            HttpContext.Session.SetString("UpdationInitializationID", Convert.ToString(ID));
            return View();
        }

        [Authorize(Roles = "Prescriber,Regulatory Manager")]
        [HttpPost]
        [ActionName("UpdateDRFRADetails")]
        public ActionResult UpdateDRFRADetails(DRFRAModel dRFRAModel)
        {

            if (ModelState.IsValid)
            {
                Tbl_DRF_Requisite_RAInfo tbl_DRF_Requisite_RAInfo = new Tbl_DRF_Requisite_RAInfo();
                tbl_DRF_Requisite_RAInfo.InitializationId = dRFRAModel.InitializationID;
                tbl_DRF_Requisite_RAInfo.ACC = dRFRAModel.ACC;
                tbl_DRF_Requisite_RAInfo.ZoneII = dRFRAModel.ZoneII;
                tbl_DRF_Requisite_RAInfo.Ivbdata = dRFRAModel.Ivbdata;
                tbl_DRF_Requisite_RAInfo.ProtocolAvailability = dRFRAModel.ProtocolAvailability;
                tbl_DRF_Requisite_RAInfo.COPPAvailability = dRFRAModel.COPPAvailability;
                tbl_DRF_Requisite_RAInfo.GMPAvailabilityId = dRFRAModel.GMPAvailabilityId;
                tbl_DRF_Requisite_RAInfo.GMPAvailability = dRFRAModel.GMPAvailability;
                tbl_DRF_Requisite_RAInfo.MfgLicense = dRFRAModel.MfgLicense;
                tbl_DRF_Requisite_RAInfo.PlantInspection = dRFRAModel.PlantInspection;
                tbl_DRF_Requisite_RAInfo.ValidationBatches = dRFRAModel.ValidationBatches;
                tbl_DRF_Requisite_RAInfo.COAAvailability = dRFRAModel.COAAvailability;
                tbl_DRF_Requisite_RAInfo.BEAvailability = dRFRAModel.BEAvailability;
                tbl_DRF_Requisite_RAInfo.APIDMFstatus = dRFRAModel.APIDMFstatus;
                tbl_DRF_Requisite_RAInfo.PlantApproval = dRFRAModel.PlantApproval;
                tbl_DRF_Requisite_RAInfo.PlantApprovalIfYes = dRFRAModel.PlantApprovalIfYes;
                tbl_DRF_Requisite_RAInfo.RegistrationValidity = dRFRAModel.RegistrationValidity;
                tbl_DRF_Requisite_RAInfo.Timefordossierpreparation = dRFRAModel.Timefordossierpreparation;
                tbl_DRF_Requisite_RAInfo.AMV = dRFRAModel.AMV;
                tbl_DRF_Requisite_RAInfo.PDR = dRFRAModel.PDR;
                tbl_DRF_Requisite_RAInfo.SamplesAvailability = dRFRAModel.SamplesAvailability;
                tbl_DRF_Requisite_RAInfo.ImportPermit = dRFRAModel.ImportPermit;
                tbl_DRF_Requisite_RAInfo.BrandNameApproval = dRFRAModel.BrandNameApproval;
                tbl_DRF_Requisite_RAInfo.AvailabilityofCDA = dRFRAModel.AvailabilityofCDA;
                tbl_DRF_Requisite_RAInfo.CurrencyID = dRFRAModel.CurrencyID;
                tbl_DRF_Requisite_RAInfo.ProductRegistrationFee = dRFRAModel.ProductRegistrationFee;
                tbl_DRF_Requisite_RAInfo.ComparativeDissolutionProfileData = dRFRAModel.ComparativeDissolutionProfileData;
                tbl_DRF_Requisite_RAInfo.Remarks = dRFRAModel.Remarks;
                tbl_DRF_Requisite_RAInfo.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_DRF_Requisite_RAInfo.CreatedDate = DateTime.Today;
                tbl_DRF_Requisite_RAInfo.ConsultantCost = dRFRAModel.ConsultantCost;
                tbl_DRF_Requisite_RAInfo.LegalizationCost = dRFRAModel.LegalizationCost;
                tbl_DRF_Requisite_RAInfo.TranslationCost = dRFRAModel.TranslationCost;
                tbl_DRF_Requisite_RAInfo.OtherCost = dRFRAModel.OtherCost;

                int data = _drfRA.updateDRFRA(tbl_DRF_Requisite_RAInfo, Convert.ToInt32(dRFRAModel.DossierTemplateID));

                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Medical Manager")]
        [HttpPost]
        [ActionName("UpdateDRFMedicalDetails")]
        public ActionResult UpdateDRFMedicalDetails(DRFMedicalModel dRFMedicalModel)
        {

            if (ModelState.IsValid)
            {
                Tbl_DRF_Medical tbl_DRF_Medical = new Tbl_DRF_Medical();
                tbl_DRF_Medical.InitializationId = dRFMedicalModel.InitializationID;
                tbl_DRF_Medical.BeCtVitroAvailable = dRFMedicalModel.BeCtVitroAvailable;
                tbl_DRF_Medical.BioWaiver = dRFMedicalModel.BioWaiver;
                tbl_DRF_Medical.CTWaiver = dRFMedicalModel.CTWaiver;
                tbl_DRF_Medical.Remark1 = dRFMedicalModel.Remark1;
                tbl_DRF_Medical.Remark2 = dRFMedicalModel.Remark2;
                tbl_DRF_Medical.Remark3 = dRFMedicalModel.Remark3;
                tbl_DRF_Medical.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_DRF_Medical.CreatedDate = DateTime.Today;
                tbl_DRF_Medical.BECost = Convert.ToDecimal(dRFMedicalModel.BECost);
                tbl_DRF_Medical.BioCost = Convert.ToDecimal(dRFMedicalModel.BioCost);
                tbl_DRF_Medical.CTCost = Convert.ToDecimal(dRFMedicalModel.CTCost);


                int data = _dRFMedical.updateDRFMedical(tbl_DRF_Medical);

                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,SCM Manager")]
        [HttpPost]
        [ActionName("UpdateDRFSCMDetails")]
        public ActionResult UpdateDRFSCMDetails(DRFSCMModel dRFSCMModel)
        {

            if (ModelState.IsValid)
            {
                Tbl_DRF_SupplyChainMgmt tbl_DRF_SupplyChainMgmt = new Tbl_DRF_SupplyChainMgmt();
                tbl_DRF_SupplyChainMgmt.InitializationId = dRFSCMModel.InitializationID;
                tbl_DRF_SupplyChainMgmt.FreightCost = dRFSCMModel.FreightCost;
                tbl_DRF_SupplyChainMgmt.TentativeShipmente = dRFSCMModel.TentativeShipmente;
                tbl_DRF_SupplyChainMgmt.TentativeDestination = dRFSCMModel.TentativeDestination;
                tbl_DRF_SupplyChainMgmt.Remark = dRFSCMModel.Remark;
                tbl_DRF_SupplyChainMgmt.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_DRF_SupplyChainMgmt.CreatedDate = DateTime.Today;

                int data = _drfSCM.updateDRFSCM(tbl_DRF_SupplyChainMgmt);

                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        //[Authorize(Roles = "Prescriber,Manufacturing Manager")]
        //[HttpPost]
        //[ActionName("UpdateDRFManufacturingDetails")]
        //public ActionResult UpdateDRFManufacturingDetails(DRFManufacturingModel dRFManufacturingModel)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        Tbl_DRF_Manufacturing tbl_DRF_Manufacturing = new Tbl_DRF_Manufacturing();
        //        tbl_DRF_Manufacturing.InitializationId = dRFManufacturingModel.InitializationID;
        //        tbl_DRF_Manufacturing.ManufacturingSiteId = dRFManufacturingModel.ManufacturingSiteId;
        //        tbl_DRF_Manufacturing.ManufacturingSiteName = dRFManufacturingModel.ManufacturingSiteName;
        //        tbl_DRF_Manufacturing.APIId = dRFManufacturingModel.APIId;
        //        tbl_DRF_Manufacturing.APISiteName = dRFManufacturingModel.APISiteName;
        //        tbl_DRF_Manufacturing.Batchsize = dRFManufacturingModel.Batchsize;
        //        tbl_DRF_Manufacturing.Leadtime = dRFManufacturingModel.Leadtime;
        //        tbl_DRF_Manufacturing.UnitEXW = dRFManufacturingModel.UnitEXW;
        //        tbl_DRF_Manufacturing.ArtworkTypeId = dRFManufacturingModel.ArtworkTypeId;
        //        // tbl_DRF_Manufacturing.TentativeSchedule = dRFManufacturingModel.TentativeSchedule;
        //        tbl_DRF_Manufacturing.Tentative_Artwork_Lead_Time = dRFManufacturingModel.Tentative_Artwork_Lead_Time;
        //        tbl_DRF_Manufacturing.PackorShipper = dRFManufacturingModel.PackorShipper;
        //        tbl_DRF_Manufacturing.GrossWeight = dRFManufacturingModel.GrossWeight;
        //        tbl_DRF_Manufacturing.Dimensions = dRFManufacturingModel.Dimensions;
        //        tbl_DRF_Manufacturing.Remark = dRFManufacturingModel.Remark;
        //        tbl_DRF_Manufacturing.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
        //        tbl_DRF_Manufacturing.CreatedDate = DateTime.Today;

        //        //int data = _drfMAN.updateDRFManufacturing(tbl_DRF_Manufacturing);
        //        List<Tbl_DRF_Manufacturing_APISite> manufacturingAPISiteList = new List<Tbl_DRF_Manufacturing_APISite>();

        //        if (dRFManufacturingModel.ManufacturingAPISiteList.Length > 0)
        //        {
        //            List<Tbl_DRF_Manufacturing_APISite> List = JsonConvert.DeserializeObject<List<Tbl_DRF_Manufacturing_APISite>>(dRFManufacturingModel.ManufacturingAPISiteList);

        //            for (int i = 0; i < List.Count; i++)
        //            {
        //                Tbl_DRF_Manufacturing_APISite APISite = new Tbl_DRF_Manufacturing_APISite();
        //                APISite.ManufacturingSiteId = dRFManufacturingModel.Id.Value;
        //                APISite.MAPIID = List[i].MAPIID;
        //                APISite.APIId = List[i].APIId;
        //                APISite.APISiteName = List[i].APISiteName;
        //                APISite.APIName = List[0].APIName;
        //                APISite.IsActive = 1;
        //                APISite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
        //                APISite.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
        //                manufacturingAPISiteList.Add(APISite);
        //            }

        //            int count = _drfMAN.insertDRFManufacturingAPISite(manufacturingAPISiteList);

        //        }

        //        if (data > 0)
        //        {
        //            ModelState.Clear();
        //            return Json(new { data = "success" }, new JsonSerializerSettings());
        //        }
        //        else
        //        {
        //            return Json(new { data = "fail" }, new JsonSerializerSettings());
        //        }

        //    }

        //    return Json(new { data = "fail" }, new JsonSerializerSettings());
        //}
        //CODE BY SONALI GORE  ABOVE OLD COSE COMMITED BELOW NEW CODE 
        [Authorize(Roles = "Prescriber,Manufacturing Manager")]
        [HttpPost]
        [ActionName("UpdateDRFManufacturingDetails")]
        public ActionResult UpdateDRFManufacturingDetails(DRFManufacturingModel dRFManufacturingModel)
        {

            if (ModelState.IsValid)
            {
                DRFManufHeaderAndDetails drFManufHeaderAndDetails = new DRFManufHeaderAndDetails();
                //Tbl_DRF_Manufacturing tbl_DRF_Manufacturing = new Tbl_DRF_Manufacturing();
                drFManufHeaderAndDetails.InitializationId = dRFManufacturingModel.InitializationID;
                drFManufHeaderAndDetails.ManufacturingSiteId = dRFManufacturingModel.ManufacturingSiteId;
                drFManufHeaderAndDetails.ManufacturingSiteName = dRFManufacturingModel.ManufacturingSiteName;
                drFManufHeaderAndDetails.APIId = dRFManufacturingModel.APIId;
                drFManufHeaderAndDetails.APISiteName = dRFManufacturingModel.APISiteName;
                drFManufHeaderAndDetails.Batchsize = dRFManufacturingModel.Batchsize;
                drFManufHeaderAndDetails.Leadtime = dRFManufacturingModel.Leadtime;
                drFManufHeaderAndDetails.UnitEXW = dRFManufacturingModel.UnitEXW;
                drFManufHeaderAndDetails.ArtworkTypeId = dRFManufacturingModel.ArtworkTypeId;
                // tbl_DRF_Manufacturing.TentativeSchedule = dRFManufacturingModel.TentativeSchedule;
                drFManufHeaderAndDetails.Tentative_Artwork_Lead_Time = dRFManufacturingModel.Tentative_Artwork_Lead_Time;
                drFManufHeaderAndDetails.PackorShipper = dRFManufacturingModel.PackorShipper;
                drFManufHeaderAndDetails.GrossWeight = dRFManufacturingModel.GrossWeight;
                drFManufHeaderAndDetails.Dimensions = dRFManufacturingModel.Dimensions;
                drFManufHeaderAndDetails.MWidth = dRFManufacturingModel.MWidth;
                drFManufHeaderAndDetails.MHeight = dRFManufacturingModel.MHeight;
                drFManufHeaderAndDetails.MLength = dRFManufacturingModel.MLength;
                drFManufHeaderAndDetails.Remark = dRFManufacturingModel.Remark;
                drFManufHeaderAndDetails.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                drFManufHeaderAndDetails.CreatedDate = DateTime.Today;

                //int data = _drfMAN.updateDRFManufacturing(drFManufHeaderAndDetails);
                List<Tbl_DRF_Manufacturing_APISite> manufacturingAPISiteList = new List<Tbl_DRF_Manufacturing_APISite>();

                if (dRFManufacturingModel.ManufacturingAPISiteList.Length > 0)
                {
                    List<Tbl_DRF_Manufacturing_APISite> List = JsonConvert.DeserializeObject<List<Tbl_DRF_Manufacturing_APISite>>(dRFManufacturingModel.ManufacturingAPISiteList);

                    for (int i = 0; i < List.Count; i++)
                    {
                        Tbl_DRF_Manufacturing_APISite APISite = new Tbl_DRF_Manufacturing_APISite();
                        APISite.ManufacturingSiteId = dRFManufacturingModel.Id.Value;
                        APISite.MAPIID = List[i].MAPIID;
                        APISite.APIId = List[i].APIId;
                        APISite.APISiteName = List[i].APISiteName;
                        APISite.APIName = List[i].APIName;
                        APISite.IsActive = 1;
                        APISite.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        APISite.ModifiedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        manufacturingAPISiteList.Add(APISite);
                    }
                }
                drFManufHeaderAndDetails.Tbl_DRF_Manufacturing_APISite = manufacturingAPISiteList;
                int data = _drfMAN.updateDRFManufacturing(drFManufHeaderAndDetails);



                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,IP Manager")]
        [HttpPost]
        [ActionName("UpdateDRFIPDetails")]
        public ActionResult UpdateDRFIPDetails(DRFIPModel dRFIPModel)
        {
            var DuplicateProjectName = "";

           
            if (ModelState.IsValid)
            {
                var projectName = dRFIPModel.ProjectName.Trim().ToUpper();
                var chkduplicate = (from x in _db.Tbl_DRF_IP_Details where x.ProjectName.ToUpper() == projectName select new { x.Id, x.ProjectName }).ToList();

                if (chkduplicate.Count > 0)
                {
                    if (dRFIPModel.IPID == chkduplicate[0].Id)
                    {
                        if (projectName == chkduplicate[0].ProjectName.ToUpper())
                        {
                            DRFIPHeaderAndDetails dRFIPHeaderAndDetails = new DRFIPHeaderAndDetails();
                            dRFIPHeaderAndDetails.InitializationId = dRFIPModel.InitializationID;
                            dRFIPHeaderAndDetails.ProjectName = dRFIPModel.ProjectName;
                            dRFIPHeaderAndDetails.Markets = dRFIPModel.Markets;
                            dRFIPHeaderAndDetails.NumbersOfApprovedANDA = dRFIPModel.NumbersOfApprovedANDA;
                            dRFIPHeaderAndDetails.PatentStatus = dRFIPModel.PatentStatus;
                            dRFIPHeaderAndDetails.LegalStatus = dRFIPModel.LegalStatus;
                            dRFIPHeaderAndDetails.IPDComments = dRFIPModel.IPDComments;
                            dRFIPHeaderAndDetails.NumbersOfApprovedGeneric = dRFIPModel.NumbersOfApprovedGeneric;
                            dRFIPHeaderAndDetails.TypeOfFiling = dRFIPModel.TypeOfFiling;
                            dRFIPHeaderAndDetails.CostofLitigation = dRFIPModel.CostofLitigation;
                            dRFIPHeaderAndDetails.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            dRFIPHeaderAndDetails.CreatedDate = DateTime.Today;

                            List<Tbl_DRF_Patent_Details> dRFIPModelDetailsList = new List<Tbl_DRF_Patent_Details>();

                            if (dRFIPModel.DRFIPModelDetailsList.Length > 0)
                            {
                                List<Tbl_DRF_Patent_Details> List = JsonConvert.DeserializeObject<List<Tbl_DRF_Patent_Details>>(dRFIPModel.DRFIPModelDetailsList);

                                for (int i = 0; i < List.Count; i++)
                                {
                                    Tbl_DRF_Patent_Details dRFIPModelDetails = new Tbl_DRF_Patent_Details();
                                    dRFIPModelDetails.PatentNumbers = List[i].PatentNumbers;
                                    dRFIPModelDetails.OriginalExpiryDate = List[i].OriginalExpiryDate;
                                    dRFIPModelDetails.Type = List[i].Type;
                                    dRFIPModelDetails.ExtensionApplication = List[i].ExtensionApplication;
                                    dRFIPModelDetails.ExtnExpiryDate = List[i].ExtnExpiryDate;
                                    dRFIPModelDetails.Comment = List[i].Comment;
                                    dRFIPModelDetails.Strategy = List[i].Strategy;
                                    dRFIPModelDetailsList.Add(dRFIPModelDetails);
                                }
                            }
                            dRFIPHeaderAndDetails.tbl_DRF_Patent_Details = dRFIPModelDetailsList;


                            int data = _drfIP.updateDRFIP(dRFIPHeaderAndDetails);

                            if (data == 1)
                            {
                                ModelState.Clear();
                                return Json(new { data = "success" }, new JsonSerializerSettings());
                            }
                            else
                            {
                                return Json(new { data = "fail" }, new JsonSerializerSettings());
                            }
                        }
                        else
                        {
                            DuplicateProjectName = "Project Name " + dRFIPModel.ProjectName.Trim() + " is already exists in database.";
                            return Json(new { data = "fail", message = DuplicateProjectName }, new JsonSerializerSettings());
                        }
                    }
                    else
                    {
                        DuplicateProjectName = "Project Name " + dRFIPModel.ProjectName.Trim() + " is already exists in database.";
                        return Json(new { data = "fail", message = DuplicateProjectName }, new JsonSerializerSettings());
                    }
                }
                else
                {
                    DRFIPHeaderAndDetails dRFIPHeaderAndDetails = new DRFIPHeaderAndDetails();
                    dRFIPHeaderAndDetails.InitializationId = dRFIPModel.InitializationID;
                    dRFIPHeaderAndDetails.ProjectName = dRFIPModel.ProjectName;
                    dRFIPHeaderAndDetails.Markets = dRFIPModel.Markets;
                    dRFIPHeaderAndDetails.NumbersOfApprovedANDA = dRFIPModel.NumbersOfApprovedANDA;
                    dRFIPHeaderAndDetails.PatentStatus = dRFIPModel.PatentStatus;
                    dRFIPHeaderAndDetails.LegalStatus = dRFIPModel.LegalStatus;
                    dRFIPHeaderAndDetails.IPDComments = dRFIPModel.IPDComments;
                    dRFIPHeaderAndDetails.NumbersOfApprovedGeneric = dRFIPModel.NumbersOfApprovedGeneric;
                    dRFIPHeaderAndDetails.TypeOfFiling = dRFIPModel.TypeOfFiling;
                    dRFIPHeaderAndDetails.CostofLitigation = dRFIPModel.CostofLitigation;
                    dRFIPHeaderAndDetails.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                    dRFIPHeaderAndDetails.CreatedDate = DateTime.Today;

                    List<Tbl_DRF_Patent_Details> dRFIPModelDetailsList = new List<Tbl_DRF_Patent_Details>();

                    if (dRFIPModel.DRFIPModelDetailsList.Length > 0)
                    {
                        List<Tbl_DRF_Patent_Details> List = JsonConvert.DeserializeObject<List<Tbl_DRF_Patent_Details>>(dRFIPModel.DRFIPModelDetailsList);

                        for (int i = 0; i < List.Count; i++)
                        {
                            Tbl_DRF_Patent_Details dRFIPModelDetails = new Tbl_DRF_Patent_Details();
                            dRFIPModelDetails.PatentNumbers = List[i].PatentNumbers;
                            dRFIPModelDetails.OriginalExpiryDate = List[i].OriginalExpiryDate;
                            dRFIPModelDetails.Type = List[i].Type;
                            dRFIPModelDetails.ExtensionApplication = List[i].ExtensionApplication;
                            dRFIPModelDetails.ExtnExpiryDate = List[i].ExtnExpiryDate;
                            dRFIPModelDetails.Comment = List[i].Comment;
                            dRFIPModelDetails.Strategy = List[i].Strategy;
                            dRFIPModelDetailsList.Add(dRFIPModelDetails);
                        }
                    }
                    dRFIPHeaderAndDetails.tbl_DRF_Patent_Details = dRFIPModelDetailsList;


                    int data = _drfIP.updateDRFIP(dRFIPHeaderAndDetails);

                    if (data == 1)
                    {
                        ModelState.Clear();
                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        return Json(new { data = "fail" }, new JsonSerializerSettings());
                    }
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }

        [Authorize(Roles = "Prescriber,Finance Manager")]
        [HttpPost]
        [ActionName("UpdateDRFFinanceApproval")]
        public ActionResult UpdateDRFFinanceApproval(DRFFinanceApproval dRFFinanceApproval)
         {

            if (ModelState.IsValid)
            {
                Tbl_DRF_FinanceDetails tbl_DRF_FinanceDetails = new Tbl_DRF_FinanceDetails();

                tbl_DRF_FinanceDetails.InitializationID = Convert.ToInt32(dRFFinanceApproval.InitializationID);
                tbl_DRF_FinanceDetails.Overallbusinesscase = dRFFinanceApproval.IsOverallBusinessCaseFine;
                tbl_DRF_FinanceDetails.Exworks = dRFFinanceApproval.Exworks;
                tbl_DRF_FinanceDetails.GCminimum = dRFFinanceApproval.GCMinimum;

                tbl_DRF_FinanceDetails.ExworksYearTwo = dRFFinanceApproval.ExworksYearTwo;
                tbl_DRF_FinanceDetails.ExworksYearThree = dRFFinanceApproval.ExworksYearThree;

                tbl_DRF_FinanceDetails.GCminimumYearTwo = dRFFinanceApproval.GCMinimumYearTwo;
                tbl_DRF_FinanceDetails.GCminimumYearThree = dRFFinanceApproval.GCMinimumYearThree;

                tbl_DRF_FinanceDetails.Expenses = dRFFinanceApproval.Expenses;
                tbl_DRF_FinanceDetails.LitigationCost = dRFFinanceApproval.LitigationCost;
                tbl_DRF_FinanceDetails.FreightCost = dRFFinanceApproval.FreightCost;
                tbl_DRF_FinanceDetails.RegistrationCost = dRFFinanceApproval.RegistrationCost;
                tbl_DRF_FinanceDetails.ConsultantCost = dRFFinanceApproval.ConsultantCost;
                tbl_DRF_FinanceDetails.LegalizationCost = dRFFinanceApproval.LegalizationCost;
                tbl_DRF_FinanceDetails.TranslationCost = dRFFinanceApproval.TranslationCost;
                tbl_DRF_FinanceDetails.OtherCost = dRFFinanceApproval.OtherCost;
                tbl_DRF_FinanceDetails.BECost = dRFFinanceApproval.BECost;
                tbl_DRF_FinanceDetails.BioCost = dRFFinanceApproval.BioCost;
                tbl_DRF_FinanceDetails.CTCost = dRFFinanceApproval.CTCost;
                tbl_DRF_FinanceDetails.FilingCost = dRFFinanceApproval.FilingCost;

                tbl_DRF_FinanceDetails.Freight = dRFFinanceApproval.Freight;
                tbl_DRF_FinanceDetails.FreightYearTwo = dRFFinanceApproval.FreightYearTwo;
                tbl_DRF_FinanceDetails.FreightYearThree = dRFFinanceApproval.FreightYearThree;

                tbl_DRF_FinanceDetails.TotalContribution = dRFFinanceApproval.TotalContribution;
                tbl_DRF_FinanceDetails.TotalPercentage = dRFFinanceApproval.TotalPercentage;
                tbl_DRF_FinanceDetails.NetContribution = dRFFinanceApproval.NetContribution;
                tbl_DRF_FinanceDetails.NetPercentage = dRFFinanceApproval.NetPercentage;

                tbl_DRF_FinanceDetails.Createdby = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                tbl_DRF_FinanceDetails.CreatedDate = DateTime.Today;

                int data = _dRFFinance.updateDRFFinanceApprovel(tbl_DRF_FinanceDetails);

                if (data == 1)
                {
                    ModelState.Clear();
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }

            }

            return Json(new { data = "fail" }, new JsonSerializerSettings());
        }
         
        [Authorize(Roles = "Country Manager,Line Manager,HOD Of Dossier,Prescriber")]
        [HttpPost]
        [ActionName("UpdateDRFApprovals")]
        [Obsolete]
        public ActionResult UpdateDRFApprovals(DRFFormApproval dRFFormApproval)
        {
            string userName = HttpContext.Session.GetString("CurrentUserName");
            //var result = _db.Tbl_DRF_Initialization.AsNoTracking().Where(t => t.InitializationID == dRFFormApproval.InitializationID).Select(t => t.DRFNo).FirstOrDefault();
            var result = _db.Tbl_DRF_Initialization.AsNoTracking().Where(t => t.InitializationID == dRFFormApproval.InitializationID).Select (t=> new {t.DRFNo, t.CountryID }).FirstOrDefault();
            string userMessage = "DRF Name : " + result.DRFNo;
            string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago.";
            int intCountryID = result.CountryID.Value;//use for only email

            var Strengthname = (from PD in _db.Tbl_DRF_Initialization
                                join asp in _db.Tbl_Master_Strength on PD.Strength equals asp.Id
                                where PD.InitializationID == dRFFormApproval.InitializationID
                                select new
                                {
                                    asp.Strength
                                }).FirstOrDefault();

            var CountryName = (from PD in _db.Tbl_DRF_Initialization
                               join asp in _db.Master_Country on PD.CountryID equals asp.Id
                               where PD.InitializationID == dRFFormApproval.InitializationID
                               select new
                               {
                                   asp.Country
                               }).FirstOrDefault();

            var projectName = _db.Tbl_DRF_Initialization
                                    .AsNoTracking()
                                    .Where(x => x.InitializationID == dRFFormApproval.InitializationID)
                                    .Select(x => x.GenericName).FirstOrDefault();


            string tempMessage = "";
            if (ModelState.IsValid)
            {
                Tbl_DRF_FormApprovals tbl_DRF_FormApprovals = new Tbl_DRF_FormApprovals();
                tbl_DRF_FormApprovals.InitializationID = Convert.ToInt32(dRFFormApproval.InitializationID);

                Int32 CurrentStatusID = _db.Tbl_DRF_Initialization
                                        .AsNoTracking()
                                        .Where(x => x.InitializationID == dRFFormApproval.InitializationID)
                                        .Select(x => x.StatusID).FirstOrDefault();

                var DRFCompanyID = _db.Tbl_DRF_Initialization
                                      .AsNoTracking()
                                      .Where(x => x.InitializationID == dRFFormApproval.InitializationID)
                                      .Select(x => x.CompanyID).FirstOrDefault();

                if (CurrentStatusID == 8)
                {
                    
                    if (dRFFormApproval.UserRole.Contains("Country Manager"))
                    {
                        if (dRFFormApproval.DRFApproval == true)
                        {
                            tbl_DRF_FormApprovals.CountryManagerApproval = true;
                            tempMessage = "Approved";
                        }
                        else
                        {
                            tbl_DRF_FormApprovals.CountryManagerApproval = false;
                            tempMessage = "Rejected";
                        }

                        tbl_DRF_FormApprovals.CMCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        tbl_DRF_FormApprovals.CMCreatedDate = DateTime.Now;
                        tbl_DRF_FormApprovals.Comment = dRFFormApproval.Comment;
                        int data = _drfINI.UpdateDRFApprovals(tbl_DRF_FormApprovals, dRFFormApproval.UserRole, CurrentStatusID);
                        ModelState.Clear();

                        //string strEmailMessage = userName + dRFFormApproval.UserRole + " has " + tempMessage + " following DRF : " + "</br>" + "DRF Name : " + result.DRFNo;
                        string strEmailMessage = userName + dRFFormApproval.UserRole + " has " + tempMessage + " following DRF : " + "</br>" + "DRF No : " + result.DRFNo + "</br>" + "DRF Name : " + projectName + "</br>" + "Strength : " + Strengthname.ToString().Replace("{", "").Replace("}", "").Replace("Strength =", "") + "</br>" + "Country : " + CountryName.ToString().Replace("{", "").Replace("}", "").Replace("Country =", "");

                        HttpContext.Session.SetString("CurrentMoleculeCompanyID", DRFCompanyID.ToString());

                        //send email notification code added by yogesh balapure on date 31/03/2021
                        if (Convert.ToBoolean(_config.GetSection("MailSend:IsUpdateDRFApproval").Value) == true)
                        {
                            MailDetails(strEmailMessage, "Project Initial Approved", intCountryID, "DRF " + tempMessage);
                        }                            

                        return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {

                        if(dRFFormApproval.UserRole == "Prescriber")
                        {
                            if (dRFFormApproval.DRFApproval == true)
                            {
                                tbl_DRF_FormApprovals.CountryManagerApproval = true;
                                tbl_DRF_FormApprovals.LineManagerApproval = true;
                                tbl_DRF_FormApprovals.HODofDossierApproval = true;
                                tempMessage = "Approved";
                            }
                            else
                            {
                                tbl_DRF_FormApprovals.CountryManagerApproval = false;
                                tbl_DRF_FormApprovals.LineManagerApproval = false;
                                tbl_DRF_FormApprovals.HODofDossierApproval = false;
                                tempMessage = "Rejected";
                            }

                            tbl_DRF_FormApprovals.CMCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FormApprovals.CMCreatedDate = DateTime.Now;
                            tbl_DRF_FormApprovals.LMCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FormApprovals.LMCreatedDate = DateTime.Now;
                            tbl_DRF_FormApprovals.HODCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FormApprovals.HODCreatedDate = DateTime.Now;
                            tbl_DRF_FormApprovals.Comment = dRFFormApproval.Comment;
                            int data = _drfINI.UpdateDRFApprovals(tbl_DRF_FormApprovals, dRFFormApproval.UserRole, CurrentStatusID);
                            ModelState.Clear();

                           // string strEmailMessage = userName + " (Administrator)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF Name : " + result.DRFNo;
                            string strEmailMessage = userName + " (Administrator)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF No : " + result.DRFNo + "</br>" + "DRF Name : " + projectName + "</br>" + "Strength : " + Strengthname.ToString().Replace("{", "").Replace("}", "").Replace("Strength =", "") + "</br>" + "Country : " + CountryName.ToString().Replace("{", "").Replace("}", "").Replace("Country =", "");
                            //send email notification code added by yogesh balapure on date 31/03/2021
                            if (Convert.ToBoolean(_config.GetSection("MailSend:IsUpdateDRFApproval").Value) == true)
                            {
                                MailDetails(strEmailMessage, "Project Initial Approved", intCountryID, "DRF " + tempMessage);
                            }               

                            return Json(new { data = "success" }, new JsonSerializerSettings());

                        }
                        else
                        {
                            return Json(new { data = "fail", message = "Need to be approved by Country Manager" }, new JsonSerializerSettings());
                        }
                       
                    }
                }
                else if((CurrentStatusID == 22) || (CurrentStatusID == 28))
                {
                    if (dRFFormApproval.UserRole == "Line Manager")
                    {
                        if (dRFFormApproval.DRFApproval == true)
                        {
                            tbl_DRF_FormApprovals.LineManagerApproval = true;
                            tempMessage = "Approved";
                        }
                        else
                        {
                            tbl_DRF_FormApprovals.LineManagerApproval = false;
                            tempMessage = "Rejected";
                        }

                        tbl_DRF_FormApprovals.LMCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        tbl_DRF_FormApprovals.LMCreatedDate = DateTime.Now;
                        tbl_DRF_FormApprovals.Comment = dRFFormApproval.Comment;
                        int data = _drfINI.UpdateDRFApprovals(tbl_DRF_FormApprovals, dRFFormApproval.UserRole, CurrentStatusID);
                        ModelState.Clear();

                       // string strEmailMessage = userName + " (Line Manager)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF Name : " + result.DRFNo;
                        string strEmailMessage = userName + " (Line Manager)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF No : " + result.DRFNo + "</br>" + "DRF Name : " + projectName + "</br>" + "Strength : " + Strengthname.ToString().Replace("{", "").Replace("}", "").Replace("Strength =", "") + "</br>" + "Country : " + CountryName.ToString().Replace("{", "").Replace("}", "").Replace("Country =", ""); 
                       
                        //send email notification code added by yogesh balapure on date 31/03/2021
                        if (Convert.ToBoolean(_config.GetSection("MailSend:IsUpdateDRFApproval").Value) == true)
                        {
                            MailDetails(strEmailMessage, "Project Initial Approved", intCountryID, "DRF " + tempMessage);
                        }                         
                            return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        if (dRFFormApproval.UserRole == "Prescriber")
                        {
                            if (dRFFormApproval.DRFApproval == true)
                            {
                                tbl_DRF_FormApprovals.CountryManagerApproval = true;
                                tbl_DRF_FormApprovals.LineManagerApproval = true;
                                tbl_DRF_FormApprovals.HODofDossierApproval = true;
                                tempMessage = "Approved";
                            }
                            else
                            {
                                tbl_DRF_FormApprovals.CountryManagerApproval = false;
                                tbl_DRF_FormApprovals.LineManagerApproval = false;
                                tbl_DRF_FormApprovals.HODofDossierApproval = false;
                                tempMessage = "Rejected";
                            }

                            tbl_DRF_FormApprovals.CMCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FormApprovals.CMCreatedDate = DateTime.Now;
                            tbl_DRF_FormApprovals.LMCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FormApprovals.LMCreatedDate = DateTime.Now;
                            tbl_DRF_FormApprovals.HODCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FormApprovals.HODCreatedDate = DateTime.Now;
                            tbl_DRF_FormApprovals.Comment = dRFFormApproval.Comment;
                            int data = _drfINI.UpdateDRFApprovals(tbl_DRF_FormApprovals, dRFFormApproval.UserRole, CurrentStatusID);
                            ModelState.Clear();
                           // string strEmailMessage = userName + " (Administrator)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF Name : " + result.DRFNo;
                            string strEmailMessage = userName + " (Administrator)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF No : " + result.DRFNo + "</br>" + "DRF Name : " + projectName + "</br>" + "Strength : " + Strengthname.ToString().Replace("{", "").Replace("}", "").Replace("Strength =", "") + "</br>" + "Country : " + CountryName.ToString().Replace("{", "").Replace("}", "").Replace("Country =", "");                             //send email notification code added by yogesh balapure on date 31/03/2021
                            if (Convert.ToBoolean(_config.GetSection("MailSend:IsUpdateDRFApproval").Value) == true)
                            {
                                MailDetails(strEmailMessage, "Project Initial Approved", intCountryID, "DRF " + tempMessage);
                            }                               
                                return Json(new { data = "success" }, new JsonSerializerSettings());

                        }
                        else
                        {
                            return Json(new { data = "fail", message = "Need to be approved by Line Manager" }, new JsonSerializerSettings());
                        }
                    }
                }
                else if ((CurrentStatusID == 23) || (CurrentStatusID == 29))
                {
                    if (dRFFormApproval.UserRole == "HOD Of Dossier")
                    {
                        if (dRFFormApproval.DRFApproval == true)
                        {
                            tbl_DRF_FormApprovals.HODofDossierApproval = true;
                            tempMessage = "Approved";
                        }
                        else
                        {
                            tbl_DRF_FormApprovals.HODofDossierApproval = false;
                            tempMessage = "Rejected";
                        }
                        tbl_DRF_FormApprovals.HODCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                        tbl_DRF_FormApprovals.HODCreatedDate = DateTime.Now;
                        tbl_DRF_FormApprovals.Comment = dRFFormApproval.Comment;

                        int data = _drfINI.UpdateDRFApprovals(tbl_DRF_FormApprovals, dRFFormApproval.UserRole, CurrentStatusID);
                        ModelState.Clear();
                       // string strEmailMessage = userName + " (HOD of Dossier)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF Name : " + result.DRFNo;
                        string strEmailMessage = userName + " (HOD of Dossier)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF No : " + result.DRFNo + "</br>" + "DRF Name : " + projectName + "</br>" + "Strength : " + Strengthname.ToString().Replace("{", "").Replace("}", "").Replace("Strength =", "") + "</br>" + "Country : " + CountryName.ToString().Replace("{", "").Replace("}", "").Replace("Country =", "");
                        //send email notification code added by yogesh balapure on date 31/03/2021
                        if (Convert.ToBoolean(_config.GetSection("MailSend:IsUpdateDRFApproval").Value) == true)
                        {
                            MailDetails(strEmailMessage, "Project Initial Approved", intCountryID, "DRF " + tempMessage);
                        }                         
                            return Json(new { data = "success" }, new JsonSerializerSettings());
                    }
                    else
                    {
                        if (dRFFormApproval.UserRole == "Prescriber")
                        {
                            if (dRFFormApproval.DRFApproval == true)
                            {
                                tbl_DRF_FormApprovals.CountryManagerApproval = true;
                                tbl_DRF_FormApprovals.LineManagerApproval = true;
                                tbl_DRF_FormApprovals.HODofDossierApproval = true;
                                tempMessage = "Approved";
                            }
                            else
                            {
                                tbl_DRF_FormApprovals.CountryManagerApproval = false;
                                tbl_DRF_FormApprovals.LineManagerApproval = false;
                                tbl_DRF_FormApprovals.HODofDossierApproval = false;
                                tempMessage = "Rejected";
                            }

                            tbl_DRF_FormApprovals.CMCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FormApprovals.CMCreatedDate = DateTime.Now;
                            tbl_DRF_FormApprovals.LMCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FormApprovals.LMCreatedDate = DateTime.Now;
                            tbl_DRF_FormApprovals.HODCreatedBy = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                            tbl_DRF_FormApprovals.HODCreatedDate = DateTime.Now;
                            tbl_DRF_FormApprovals.Comment = dRFFormApproval.Comment;

                            int data = _drfINI.UpdateDRFApprovals(tbl_DRF_FormApprovals, dRFFormApproval.UserRole, CurrentStatusID);
                            ModelState.Clear();
                           // string strEmailMessage = userName + " (Administrator)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF Name : " + result.DRFNo;
                            string strEmailMessage = userName + " (Administrator)" + " has " + tempMessage + " following DRF : " + "</br>" + "DRF No : " + result.DRFNo + "</br>" + "DRF Name : " + projectName + "</br>" + "Strength : " + Strengthname.ToString().Replace("{", "").Replace("}", "").Replace("Strength =", "") + "</br>" + "Country : " + CountryName.ToString().Replace("{", "").Replace("}", "").Replace("Country =", ""); 
                            //send email notification code added by yogesh balapure on date 31/03/2021
                            if (Convert.ToBoolean(_config.GetSection("MailSend:IsUpdateDRFApproval").Value) == true)
                            {
                                MailDetails(strEmailMessage, "Project Initial Approved", intCountryID, "DRF " + tempMessage);
                            }                            
                                return Json(new { data = "success" }, new JsonSerializerSettings());

                        }
                        else
                        {
                            return Json(new { data = "fail", message = "Need to be approved by HOD of Dossier" }, new JsonSerializerSettings());
                        }
                    }
                }

                //await _notificationHubContext.Clients.All.SendAsync("sendToUser", userName, userMessage, messageTime);                            


            }

            return Json(new { data = "fail", message="Please fill all mendatory fields." }, new JsonSerializerSettings());
        }

        [Authorize]
        [HttpPost]
        [ActionName("GetAllProductMasterList")]
        public ActionResult GetAllProductMasterList()
        {
            var allProductDataList = _drfINI.GetAllProductMasterNameList();

            return Json(new { success = true, Data = allProductDataList });

        }
        [Authorize]
        [HttpPost]
        [ActionName("GetAllProductMasterListByGenericName")]
        public ActionResult GetAllProductMasterListByGenericName(string GenericName)
        {
            var allFormulationList = _drfINI.GetAllDetailsByProductMasterName("formulationname", GenericName);
            var allStrengthList = _drfINI.GetAllDetailsByProductMasterName("strengthname", GenericName);
            var allPackstyleList = _drfINI.GetAllDetailsByProductMasterName("packstylename", GenericName);
            var allPackSizeList = _drfINI.GetAllDetailsByProductMasterName("packsizename", GenericName);
            var allPlantList = _drfINI.GetAllDetailsByProductMasterName("plantname", GenericName);                     

            return Json(new {data= new { success = true, allFormulationList = allFormulationList, allStrengthList = allStrengthList, allPackstyleList = allPackstyleList, allPackSizeList = allPackSizeList, allPlantList = allPlantList } });

        }

        [Authorize]
        [HttpPost]
        [ActionName("InsertDraft_DRFInitializationDetails")]        
        public ActionResult InsertDraft_DRFInitializationDetails(DRFInitializationDraftModel dRFInitializationDraftModel)
        {           
            var projectName = dRFInitializationDraftModel.GenericName.Trim().ToUpper();
            var chkduplicate = _drfINI.Check_Exists_DraftInitializationData(dRFInitializationDraftModel.CountryID, dRFInitializationDraftModel.GenericName, dRFInitializationDraftModel.Form, dRFInitializationDraftModel.Strength, dRFInitializationDraftModel.PackSize, dRFInitializationDraftModel.PackStyle, dRFInitializationDraftModel.Plant, dRFInitializationDraftModel.ProductTypeId);
            var Strengthname = (from x in _db.Tbl_Master_Strength where x.Id == dRFInitializationDraftModel.Strength select x.Strength).FirstOrDefault();
            var CountryName = (from x in _db.Master_Country where x.Id == dRFInitializationDraftModel.CountryID select x.Country).FirstOrDefault();
            
            int? intCountryID = dRFInitializationDraftModel.CountryID;

            if (chkduplicate != null && dRFInitializationDraftModel.DraftID ==0)
            {
                ViewBag.DuplicateGenericName = "Generic Name " + dRFInitializationDraftModel.GenericName.Trim() + " & Strength " + Strengthname + " is already exists in database.";
            }
            else
            {
                if(dRFInitializationDraftModel.DraftID == null) { dRFInitializationDraftModel.DraftID = 0; }
                dRFInitializationDraftModel.CreatedBy= Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
                dRFInitializationDraftModel.CreatedDate = DateTime.Today;
                int data = _drfINI.InsertUpdateDraft_InitializationData(dRFInitializationDraftModel);
                if (data != 0)
                {                       
                    return Json(new { data = "success" }, new JsonSerializerSettings());
                }
                else
                {
                    return Json(new { data = "fail" }, new JsonSerializerSettings());
                }
            }
            return Json(new { data = "fail", message = ViewBag.DuplicateGenericName }, new JsonSerializerSettings());            
        }

        [Authorize]
        [HttpPost]
        [ActionName("GetDraft_DRFInitializationDetails")]        
        public ActionResult GetDraft_DRFInitializationDetails(int DraftID)
        {
            int UserID= Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            var draftdata = _drfINI.GetDraftInitializationDataByUserID_DraftID(UserID, DraftID);

            return Json(new { data = new { success = true, draftdata = draftdata } });
        }

        [Authorize]
        [HttpPost]
        [ActionName("GetAll_Draft_DRFInitializationData")]
        public ActionResult GetAll_Draft_DRFInitializationData()
        {
            int UserID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            var alldraftdata = _drfINI.GetAllDraftInitializationDataByUserID(UserID);

            return Json(new { data = new { success = true, alldraftdata = alldraftdata } });
        }

        [Authorize]
        [HttpPost]
        [ActionName("DeleteDraft_DRFInitializationDetails")]
        public ActionResult DeleteDraft_DRFInitializationDetails(int DraftID)
        {
            int UserID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserId"));
            var alldraftdata = _drfINI.DeleteDraftInitializationDataByUserID_DraftID(UserID, DraftID);

            return Json(new { data = "success" }, new JsonSerializerSettings());
        }


        public void CreateFolderStructure(string NewFolderPath,string FolderName)
        {
            string FolderPath = NewFolderPath + @"\"+ FolderName;
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
        }
        public void CreateFolderStructure1()
        {
            ProjectFolder = _config.GetSection("EmcureFolderPath").Value + "EM-" + DateTime.Now.Year + "-Rahul" + "EM-" + DateTime.Now.Year + "-000000";

            if (!Directory.Exists(ProjectFolder))
            {
                Directory.CreateDirectory(ProjectFolder);
            }

            IList<Tbl_Master_FolderStructure> tbl_Master_FolderStructures = new List<Tbl_Master_FolderStructure>();
            tbl_Master_FolderStructures = _dRFFinal.GetFolderStructureCountryWise(3,22);

            String abc = "";
            string NewFolderPath1 = "";
            string NewFolderPath = ProjectFolder;
            if (tbl_Master_FolderStructures.Count > 0)
            {
                for (int i = 0; i < tbl_Master_FolderStructures.Count; i++)
                {
                    if (tbl_Master_FolderStructures[i].ParentFolderID == 0)
                    {
                        CreateFolderStructure(ProjectFolder, tbl_Master_FolderStructures[i].FolderName);
                    }
                    else
                    {
                        if (NewFolderPath != NewFolderPath1)
                        {
                            if (Directory.Exists(NewFolderPath))
                            {
                                var mainName = (from TMF in _db.Tbl_Master_FolderStructure
                                                where TMF.Id == tbl_Master_FolderStructures[i].ParentFolderID
                                                select new
                                                {
                                                    TMF.FolderName
                                                }
                                                ).FirstOrDefault();

                                NewFolderPath += @"\" + mainName.FolderName; 
                                NewFolderPath1 = NewFolderPath;
                                abc = mainName.FolderName;
                                CreateFolderStructure(NewFolderPath, tbl_Master_FolderStructures[i].FolderName);
                            }
                        }
                        else
                        {
                            var mainName = (from TMF in _db.Tbl_Master_FolderStructure
                                            where TMF.Id == tbl_Master_FolderStructures[i].ParentFolderID
                                            select new
                                            {
                                                TMF.FolderName
                                            }
                                                      ).FirstOrDefault();

                            if (abc != mainName.FolderName)
                            {
                                if (Directory.Exists(NewFolderPath))
                                {
                                    NewFolderPath1 = NewFolderPath += @"\" + mainName.FolderName;
                                    CreateFolderStructure(NewFolderPath1, tbl_Master_FolderStructures[i].FolderName);
                                    NewFolderPath = NewFolderPath1;
                                    abc = mainName.FolderName;
                                }
                            }
                            else
                            {
                                CreateFolderStructure(NewFolderPath, tbl_Master_FolderStructures[i].FolderName);
                            }
                        }

                    }

                }
            }

        }

        public void CreateACDTFolders()
        {
            //Part 1
            string Part1 = ProjectFolder + @"\PART I";
            if (!Directory.Exists(Part1))
            {
                Directory.CreateDirectory(Part1);
            }
            //Part 2
            string Part2 = ProjectFolder + @"\PART II";
            if (!Directory.Exists(Part2))
            {
                Directory.CreateDirectory(Part2);
            }
            //Part 3
            string Part3 = ProjectFolder + @"\PART III";
            if (!Directory.Exists(Part3))
            {
                Directory.CreateDirectory(Part3);
            }
            //Part 4
            string Part4 = ProjectFolder + @"\PART IV";
            if (!Directory.Exists(Part4))
            {
                Directory.CreateDirectory(Part4);
            }
        }

        [Obsolete]
        private void MailDetails( string EmailMessage, string strEmailDetails,int intCountryID,string strSubject)
        {
            try
            {
                string strEmailMessage = EmailMessage;
                //send email notification code added by yogesh balapure on date 08/02/2020
                //get smtp details 
                SMTPDetailsModel sMTPDetailsModel = _sMTPService.SMTPDetails();
                EmailDetailsModel emailDetailsModel = _emailService.EmailDetails(strEmailDetails);
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

                if (emailDetailsModel != null)
                {
                    //get details
                    emailDetailsVM.ToMail = emailDetailsModel.ToList;
                    List<string> testCC = new List<string>();
                    if (!string.IsNullOrEmpty(emailDetailsModel.CCList))
                    {
                        if (emailDetailsModel.CCList.Contains(";"))
                        {
                            string[] splitcc = emailDetailsModel.CCList.Split(";");
                            foreach (var ccdata in splitcc)
                            {
                                testCC.Add(ccdata.Trim());
                            }
                        }
                        else
                        {
                            testCC.Add(emailDetailsModel.CCList);
                        }
                    }
                    List<string> testBCC = new List<string>();
                    var continentresult = _db.Master_Country.AsNoTracking().Where(x => x.Id == intCountryID).FirstOrDefault();
                    if(continentresult != null)
                    {
                        //Get BCC List from DB
                        int intContinentID = continentresult.ContinentID.Value;
                        var bccList = (from TMUCCM in _db.Tbl_Master_User_Country_Mapping
                                       join PCRD in _db.PrescriberDetails on TMUCCM.UserID equals PCRD.AspNetUserId
                                       join ANU in _db.AspNetUsers on PCRD.AspNetUserId equals ANU.UserId
                                       join MC in _db.Master_Country on TMUCCM.CountryID equals MC.Id
                                       join MCNT in _db.Master_Continent on TMUCCM.ContinentID equals MCNT.Id
                                       where TMUCCM.CountryID == intCountryID && TMUCCM.IsActive == true && PCRD.CompanyID == Convert.ToInt32(HttpContext.Session.GetString("CurrentMoleculeCompanyID"))
                                       //where TMUCCM.ContinentID == intContinentID && TMUCCM.IsActive == true
                                       select new { TMUCCM.Id, TMUCCM.UserID, PCRD.FirstName, PCRD.LastName, ANU.Email, TMUCCM.ContinentID, MCNT.Continent, TMUCCM.CountryID, MC.Country }
                                       ).ToList();

                        if (bccList != null && bccList.Count > 0)
                        {
                            foreach (var ddata in bccList)
                            {
                                if (!string.IsNullOrEmpty(ddata.Email))
                                {
                                    testBCC.Add(ddata.Email.Trim());
                                }
                            }
                        }

                        emailDetailsVM.CCMail = testCC;
                        emailDetailsVM.BCCMail = testBCC;
                        emailDetailsVM.Subject = strSubject;

                        clsTemplate _clsTemplate = new clsTemplate(_config, _env);
                        string tempurl = _config.GetSection("ApplicationURL:DrfUrlLink").Value + intCountryID;
                        emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentMoleculeCompanyID")));
                        emailDetailsVM.DispalyName = "";
                    }

                    
                }

                if (sMTPDetailsModel != null && emailDetailsModel != null)
                {
                    EmailHelper emailHelper = new EmailHelper();
                    var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                }
                //End of email notification details
            }
            catch(Exception ex)
            {

            }

        }

        [Obsolete]
        private void SendEmailForDRFDetails(int CurrentUserID,int intInitializationID,string strSubject,string DepartmentName)
        {

            //Add email notification as told by Rahul Patil on date 01/09/2021
            //get drf details using initializationid

            //var drfdetails = (from a in _db.Tbl_DRF_Initialization
            //                  join b in _db.Tbl_DRF_FormApprovals on a.InitializationID equals b.InitializationID
            //                  join c in _db.Master_Country on a.CountryID equals c.Id
            //                  join p1 in _db.PrescriberDetails on b.DRFCreatedBy equals p1.AspNetUserId
            //                  join p2 in _db.PrescriberDetails on b.CMCreatedBy equals p2.AspNetUserId
            //                  join p3 in _db.PrescriberDetails on b.LMCreatedBy equals p3.AspNetUserId
            //                  join p4 in _db.PrescriberDetails on b.HODCreatedBy equals p4.AspNetUserId
            //                  join anu1 in _db.AspNetUsers on p1.AspNetUserId equals anu1.UserId
            //                  join anu2 in _db.AspNetUsers on p2.AspNetUserId equals anu2.UserId
            //                  join anu3 in _db.AspNetUsers on p3.AspNetUserId equals anu3.UserId
            //                  join anu4 in _db.AspNetUsers on p4.AspNetUserId equals anu4.UserId
            //                  where a.InitializationID == intInitializationID
            //                  select new
            //                  {
            //                      GenericName = a.GenericName,
            //                      CountryName = c.Country,
            //                      DRFCreatedByID = b.DRFCreatedBy,
            //                      DRFCreatedName = p1.FirstName + " " + p1.LastName,
            //                      DRFCreatedEmail = anu1.Email,
            //                      CMCreatedByID = b.CMCreatedBy,
            //                      DRFCMCreatedName = p2.FirstName + " " + p2.LastName,
            //                      DRFCMCreatedEmail = anu2.Email,
            //                      LMCreatedByID = b.LMCreatedBy,
            //                      DRFLMCreatedName = p3.FirstName + " " + p3.LastName,
            //                      DRFLMCreatedEmail = anu3.Email,
            //                      HODCreatedByID = b.HODCreatedBy,
            //                      HODCreatedName = p4.FirstName + " " + p4.LastName,
            //                      HODCreatedEmail = anu4.Email
            //                  }).FirstOrDefault();

            var drfdetails = (from a in _db.Tbl_DRF_Initialization
                              join b in _db.Tbl_DRF_FormApprovals on a.InitializationID equals b.InitializationID
                              join c in _db.Master_Country on a.CountryID equals c.Id
                              join d in _db.Tbl_Master_Strength on a.Strength equals d.Id
                              from p1 in _db.PrescriberDetails.Where(x=> x.AspNetUserId == b.DRFCreatedBy).DefaultIfEmpty()
                              from p2 in _db.PrescriberDetails.Where(x => x.AspNetUserId == b.CMCreatedBy).DefaultIfEmpty()
                              from p3 in _db.PrescriberDetails.Where(x => x.AspNetUserId == b.LMCreatedBy).DefaultIfEmpty()
                              from p4 in _db.PrescriberDetails.Where(x => x.AspNetUserId == b.HODCreatedBy).DefaultIfEmpty()                                  
                              from anu1 in _db.AspNetUsers.Where(x => x.UserId == p1.AspNetUserId).DefaultIfEmpty()
                              from anu2 in _db.AspNetUsers.Where(x => x.UserId == p2.AspNetUserId).DefaultIfEmpty()
                              from anu3 in _db.AspNetUsers.Where(x => x.UserId == p3.AspNetUserId).DefaultIfEmpty()
                              from anu4 in _db.AspNetUsers.Where(x => x.UserId == p4.AspNetUserId).DefaultIfEmpty()                              
                              where a.InitializationID == intInitializationID
                              select new
                              {
                                  DRFNumber = a.DRFNo,
                                  GenericName = a.GenericName,
                                  Strengthname = d.Strength,
                                  CountryName = c.Country,
                                  DRFCreatedByID = b.DRFCreatedBy,
                                  DRFCreatedName = p1.FirstName + " " + p1.LastName,
                                  DRFCreatedEmail = anu1.Email,
                                  CMCreatedByID = b.CMCreatedBy,
                                  DRFCMCreatedName = p2.FirstName + " " + p2.LastName,
                                  DRFCMCreatedEmail = anu2.Email,
                                  LMCreatedByID = b.LMCreatedBy,
                                  DRFLMCreatedName = p3.FirstName + " " + p3.LastName,
                                  DRFLMCreatedEmail = anu3.Email,
                                  HODCreatedByID = b.HODCreatedBy,
                                  HODCreatedName = p4.FirstName + " " + p4.LastName,
                                  HODCreatedEmail = anu4.Email
                              }).FirstOrDefault();
            //get current user details emailid, user name            
            string tempCurrentUserName=null;
            string tempCurrentUserEmaiID=null;
            var currentuserdetails = (from p in _db.PrescriberDetails
                                      join q in _db.AspNetUsers on p.AspNetUserId equals q.UserId
                                      //join r in _db.AspNetUserRoles on q.Id equals r.UserId
                                      //join s in _db.AspNetRoles on r.RoleId equals s.Id
                                      where q.IsEnabled == true && p.AspNetUserId == CurrentUserID
                                      select new { p.AspNetUserId, p.FirstName, p.LastName, q.Email }).ToList();
            if (currentuserdetails != null & currentuserdetails.Count > 0)
            {
                tempCurrentUserName = currentuserdetails[0].FirstName + " " + currentuserdetails[0].LastName;
                tempCurrentUserEmaiID = currentuserdetails[0].Email;
            }           
                        
            string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago.";
            string userName = tempCurrentUserName.Trim() + " has DRF Updated of " + DepartmentName + "." + "</br>";
            userName = userName  +"DRF No : " + drfdetails.DRFNumber + "</br>" + "DRF Name: " + drfdetails.GenericName.Trim() + "</br>" + "Strength : " + drfdetails.Strengthname + "</br>" + "Country : " + drfdetails.CountryName;
            string strEmailMessage = userName + "</br>";

            //send email notification code added by yogesh balapure on date 01/09/2021
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

            if (drfdetails != null)
            {
                //get details
                emailDetailsVM.ToMail = "rahula.patil@emcure.co.in";
                List<string> testCC = new List<string>();
                testCC.Add("rahula.patil@emcure.co.in");
                List<string> testBCC = new List<string>();
                if (drfdetails != null && tempCurrentUserEmaiID != null)
                {                   
                    testBCC.Add(tempCurrentUserEmaiID.Trim());
                    if(!string.IsNullOrEmpty(drfdetails.DRFCreatedEmail))
                    {
                        testBCC.Add(drfdetails.DRFCreatedEmail.Trim());
                    }
                    if (!string.IsNullOrEmpty(drfdetails.DRFCMCreatedEmail))
                    {
                        testBCC.Add(drfdetails.DRFCMCreatedEmail.Trim());
                    }
                    if (!string.IsNullOrEmpty(drfdetails.DRFLMCreatedEmail))
                    {
                        testBCC.Add(drfdetails.DRFLMCreatedEmail.Trim());
                    }
                    if (!string.IsNullOrEmpty(drfdetails.HODCreatedEmail))
                    {
                        testBCC.Add(drfdetails.HODCreatedEmail.Trim());
                    }                     
                }

                emailDetailsVM.CCMail = testCC;
                emailDetailsVM.BCCMail = testBCC;
                emailDetailsVM.Subject = strSubject;
                clsTemplate _clsTemplate = new clsTemplate(_config, _env);
                string tempurl = _config.GetSection("ApplicationURL:NewDRFLink").Value;
                emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentMoleculeCompanyID")));
                emailDetailsVM.DispalyName = "";
            }

            if (sMTPDetailsModel != null && drfdetails != null && tempCurrentUserName != null && tempCurrentUserEmaiID != null)
            {
                EmailHelper emailHelper = new EmailHelper();
                if (Convert.ToBoolean(_config.GetSection("MailSend:IsDepartmentDataInsert").Value) == true)
                {
                    var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                }
            }
        }

        [Obsolete]
        private void SendEmailForDRFUpdation(int CurrentUserID, int intInitializationID, string strSubject,string UpdateRemark)
        {            
            //get drf details using initializationid           

            var drfdetails = (from a in _db.Tbl_DRF_Initialization
                              join b in _db.Tbl_DRF_FormApprovals on a.InitializationID equals b.InitializationID
                              join c in _db.Master_Country on a.CountryID equals c.Id
                              join d in _db.Tbl_Master_Strength on a.Strength equals d.Id
                              from p1 in _db.PrescriberDetails.Where(x => x.AspNetUserId == b.DRFCreatedBy).DefaultIfEmpty()
                              //from p2 in _db.PrescriberDetails.Where(x => x.AspNetUserId == b.CMCreatedBy).DefaultIfEmpty()
                              //from p3 in _db.PrescriberDetails.Where(x => x.AspNetUserId == b.LMCreatedBy).DefaultIfEmpty()
                              //from p4 in _db.PrescriberDetails.Where(x => x.AspNetUserId == b.HODCreatedBy).DefaultIfEmpty()
                              from anu1 in _db.AspNetUsers.Where(x => x.UserId == p1.AspNetUserId).DefaultIfEmpty()
                              //from anu2 in _db.AspNetUsers.Where(x => x.UserId == p2.AspNetUserId).DefaultIfEmpty()
                              //from anu3 in _db.AspNetUsers.Where(x => x.UserId == p3.AspNetUserId).DefaultIfEmpty()
                              //from anu4 in _db.AspNetUsers.Where(x => x.UserId == p4.AspNetUserId).DefaultIfEmpty()
                              where a.InitializationID == intInitializationID
                              select new
                              {
                                  DRFNumber = a.DRFNo,
                                  GenericName = a.GenericName,
                                  Strengthname = d.Strength,
                                  CountryName = c.Country,
                                  DRFCreatedByID = b.DRFCreatedBy,
                                  DRFCreatedName = p1.FirstName + " " + p1.LastName,
                                  DRFCreatedEmail = anu1.Email,
                                  //CMCreatedByID = b.CMCreatedBy
                                  //DRFCMCreatedName = p2.FirstName + " " + p2.LastName,
                                  //DRFCMCreatedEmail = anu2.Email,
                                  //LMCreatedByID = b.LMCreatedBy,
                                  //DRFLMCreatedName = p3.FirstName + " " + p3.LastName,
                                  //DRFLMCreatedEmail = anu3.Email,
                                  //HODCreatedByID = b.HODCreatedBy,
                                  //HODCreatedName = p4.FirstName + " " + p4.LastName,
                                  //HODCreatedEmail = anu4.Email
                              }).FirstOrDefault();
            //get current user details emailid, user name            
            string tempCurrentUserName = null;
            string tempCurrentUserEmaiID = null;
            var currentuserdetails = (from p in _db.PrescriberDetails
                                      join q in _db.AspNetUsers on p.AspNetUserId equals q.UserId                                      
                                      where q.IsEnabled == true && p.AspNetUserId == CurrentUserID
                                      select new { p.AspNetUserId, p.FirstName, p.LastName, q.Email }).ToList();
            if (currentuserdetails != null & currentuserdetails.Count > 0)
            {
                tempCurrentUserName = currentuserdetails[0].FirstName + " " + currentuserdetails[0].LastName;
                tempCurrentUserEmaiID = currentuserdetails[0].Email;
            }

            string messageTime = Convert.ToString(DateTime.Now.Second) + " seconds ago.";
            string userName = tempCurrentUserName.Trim() + " has DRF Updated." + "</br>";
            userName = userName + "DRF No : " + drfdetails.DRFNumber + "</br>" + "DRF Name: " + drfdetails.GenericName.Trim() + "</br>" + "Strength : " + drfdetails.Strengthname + "</br>" + "Country : " + drfdetails.CountryName;
            string strEmailMessage = userName + "</br>" + "Update Reason : " + UpdateRemark + "</br>";

            //send email notification code added by yogesh balapure on date 01/09/2021
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

            if (drfdetails != null)
            {
                //get details
                emailDetailsVM.ToMail = "rahula.patil@emcure.co.in";
                List<string> testCC = new List<string>();
                testCC.Add("rahula.patil@emcure.co.in");
                List<string> testBCC = new List<string>();
                if (drfdetails != null && tempCurrentUserEmaiID != null)
                {
                    testBCC.Add(tempCurrentUserEmaiID.Trim());
                    if (!string.IsNullOrEmpty(drfdetails.DRFCreatedEmail))
                    {
                        //testBCC.Add(drfdetails.DRFCreatedEmail.Trim());

                        //only for test
                        testBCC.Add("rahula.patil@emcure.co.in");
                    }
                    //if (!string.IsNullOrEmpty(drfdetails.DRFCMCreatedEmail))
                    //{
                    //    testBCC.Add(drfdetails.DRFCMCreatedEmail.Trim());
                    //}
                    //if (!string.IsNullOrEmpty(drfdetails.DRFLMCreatedEmail))
                    //{
                    //    testBCC.Add(drfdetails.DRFLMCreatedEmail.Trim());
                    //}
                    //if (!string.IsNullOrEmpty(drfdetails.HODCreatedEmail))
                    //{
                    //    testBCC.Add(drfdetails.HODCreatedEmail.Trim());
                    //}
                }

                emailDetailsVM.CCMail = testCC;
                emailDetailsVM.BCCMail = testBCC;
                emailDetailsVM.Subject = strSubject;
                clsTemplate _clsTemplate = new clsTemplate(_config, _env);
                string tempurl = _config.GetSection("ApplicationURL:NewDRFLink").Value;
                emailDetailsVM.Body = _clsTemplate.CreateCommonMailBody(strEmailMessage, tempurl, Convert.ToInt32(HttpContext.Session.GetString("CurrentMoleculeCompanyID")));
                emailDetailsVM.DispalyName = "";
            }

            if (sMTPDetailsModel != null && drfdetails != null && tempCurrentUserName != null && tempCurrentUserEmaiID != null)
            {
                EmailHelper emailHelper = new EmailHelper();
                if (Convert.ToBoolean(_config.GetSection("MailSend:IsDepartmentDataInsert").Value) == true)
                {
                    var _task = Task.Run(() => emailHelper.SendMail(sMTPDetailsVM, emailDetailsVM));
                }
            }
        }
    }
}