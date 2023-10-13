using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using EmcureNPD.Web.Helpers;
using EmcureNPD.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using AdditionalCost = EmcureNPD.Business.Models.AdditionalCost;

namespace EmcureNPD.Web.Controllers
{
    public class ManagementController : BaseController
    {
        #region Properties

        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private readonly IStringLocalizer<Master> _stringLocalizerMaster;
        private readonly IHelper _helper;

        #endregion Properties

        public ManagementController(IConfiguration configuration, IStringLocalizer<Master> stringLocalizerMaster,

            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared, IHelper helper)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _stringLocalizerMaster = stringLocalizerMaster;
            _helper = helper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PIDFManagementApproval()
        {
            int rolId = _helper.GetLoggedInRoleId();
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.ManagementHOD, rolId);
            ViewBag.Access = objPermssion;
            var pidfid = Request.Query["pidfid"];
            var buid = Request.Query["bui"];
            if (pidfid != "")
            {
                ProjectsModel model = new ProjectsModel();
                List<ProjectNameModel> ListProjectName = new List<ProjectNameModel>();
                List<ProjectStrength> ListprojectStrengths = new List<ProjectStrength>();
                List<Manager> Listmanager = new List<Manager>();
                List<HeadWiseBudget> ListHeadWiseBudget = new List<HeadWiseBudget>();
                List<ProjectDetails> ListProjectDetails = new List<ProjectDetails>();
                List<CumulativePhaseWiseBudget> ListCumulativePhaseWiseBudget = new List<CumulativePhaseWiseBudget>();
                List<AdditionalCost> ListAdditionalCost = new List<AdditionalCost>();
                List<PBFDetails> ListPBFDetails=new List<PBFDetails>();
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetProjectNameAndStrength + "/" + pidfid + "/" + buid, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ProjectsView>(jsonResponse);
                    if (data.table.Count > 0)
                    {
                        foreach (var item in data.table)
                        {
                            ListProjectName.Add(new ProjectNameModel
                            {
                                ProjectName = item.projectName,
                                CurrencyCode = item.CurrencyCode,
                                CurrencySymbol = item.CurrencySymbol
                            });
                        }
                        model.lsProjectName = ListProjectName;
                    }
                    if (data.table1.Count > 0)
                    {
                        foreach (var item in data.table1)
                        {
                            ListprojectStrengths.Add(new ProjectStrength
                            {
                                Strength = item.strength,
                                ProjectCode = item.projectCode,
                                IsInhouse=item.isInhouse
                            });
                        }
                        model.lsProjectStrength = ListprojectStrengths;
                    }
                    if (data.table2.Count > 0)
                    {
                        foreach (var item in data.table2)
                        {
                            Listmanager.Add(new Manager
                            {
                                UserId = item.userId,
                                ManagerName = item.managerName,
                                DesignationName = item.designationName,
                                StatusId = item.statusId,
                                CreatedDate = item.CreatedDate
                            });
                        }
                        model.lsManager = Listmanager;
                    }
                    if (data.table3.Count > 0)
                    {
                        for (int i = 0; i < data.table3.Count; i++)
                        {
                            ListHeadWiseBudget.Add(new HeadWiseBudget
                            {
                                BudgetsHeades = data.table3[i].BudgetsHeades,
                                Prototype = data.table3[i].Prototype,
                                ScaleUp = data.table3[i].ScaleUp,
                                Exhibit = data.table3[i].Exhibit,
                                TOTAL = data.table3[i].TOTAL
                            });
                        }
                        model.lsHeadWiseBudget = ListHeadWiseBudget;
                    }
                    if (data.table4.Count > 0)
                    {
                        for (int i = 0; i < data.table4.Count; i++)
                        {
                            ListProjectDetails.Add(new ProjectDetails
                            {
                                Market = data.table4[i].Market,
                                SponsorBusinessPartner = data.table4[i].SponsorBusinessPartner,
                                GroupLeader = data.table4[i].GroupLeader,
                                ProjectComplexity = data.table4[i].ProjectComplexity,
                                TotalProjectDuration = data.table4[i].TotalProjectDuration,
                                API = data.table4[i].API,
                                APISource = data.table4[i].APISource,
                                APICommercialQuantity = data.table4[i].APICommercialQuantity,
                                APIPrice = data.table4[i].APIPrice,
                                APIRequirement = data.table4[i].APIRequirement,
                                Prototype = data.table4[i].Prototype,
                                ScaleUp = data.table4[i].ScaleUp,
                                Exhibit = data.table4[i].Exhibit,
                                ProjectBudget = data.table4[i].ProjectBudget,
                                ProjectCompletionFilingDate = data.table4[i].ProjectCompletionFilingDate,
                                BEStudies = data.table4[i].BEStudies,
                                Note_Remark = data.table4[i].Note_Remark,
                                PlantName = data.table4[i].PlantName,
                                ProjectInitiationDate = data.table4[i].ProjectInitiationDate,
                            });
                        }
                        model.lsProjectDetails = ListProjectDetails;
                    }
                    if (data.table5.Count > 0)
                    {
                        for (int i = 0; i < data.table5.Count; i++)
                        {
                            ListCumulativePhaseWiseBudget.Add(new CumulativePhaseWiseBudget
                            {
                                Feasability = data.table5[i].Feasability,
                                Prototype = data.table5[i].Prototype,
                                ScaleUp = data.table5[i].ScaleUp,
                                AMV = data.table5[i].AMV,
                                Exhibit = data.table5[i].Exhibit,
                                Filing = data.table5[i].Filing,
                                Total = (data.table5[i].Filing),
                            });
                        }
                        model.lsCumulativePhaseWiseBudget = ListCumulativePhaseWiseBudget;
                    }
                    if (data.table6.Count > 0)
                    {
                        for (int i = 0; i < data.table6.Count; i++)
                        {
                            ListAdditionalCost.Add(new AdditionalCost
                            {
                                BusinessUnitId = data.table6[i].BusinessUnitId,
                                BusinessUnitName = data.table6[i].BusinessUnitName,
                                ReferenceProductCost = data.table6[i].ReferenceProductCost,
                                BioStudyCost = data.table6[i].BioStudyCost,
                                CapexMiscCost = data.table6[i].CapexMiscCost,
                                FillingCost = data.table6[i].FillingCost,
                                Total = data.table6[i].Total
                            });
                        }
                        model.lsAdditionalCost = ListAdditionalCost;
                    }
                    if (data.table7.Count > 0)
                    {
                        for (int i = 0; i < data.table7.Count; i++)
                        {
                            ListPBFDetails.Add(new PBFDetails
                            {
                                cost = data.table7[i].cost,
                                workflowname = data.table7[i].tentative,
                                pbfworkflowname = data.table7[i].pbfworkflowtaskname,
                                tentative = data.table7[i].workflowname,
                                pbfworkflowtaskname = data.table7[i].pbfworkflowname,
                            });
                        }
                        model.lsPBFDetails = ListPBFDetails;
                    }
                    model.financeModel= PIDFFinance(pidfid.ToString(), buid.ToString());
                    return View(model);
                }
            }
            return View();
        }


        //-------------Finanace Data----------------------

        private FinanceModel PIDFFinance(string pidfid,string buid)
        {
            FinanceModel model = new FinanceModel();
            if (pidfid != "")
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetPidfFinance + "/" + pidfid, HttpMethod.Get, token).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Root>(jsonResponse);
                    if (data.table.Count > 0)
                    {
                        model.BussinessUnitId = Convert.ToInt32(UtilityHelper.Decreypt(Convert.ToString(buid)));
                        model.PidffinaceId = data.table[0].pidffinaceId;
                        model.Pidfid = UtilityHelper.Encrypt(Convert.ToString(data.table[0].pidfid));
                        model.dycrPidfid = data.table[0].pidfid;
                        model.Entity = data.table[0].entity;
                        model.Product = data.table[0].product;
                        model.ForecastDate = data.table[0].forecastDate;
                        model.Currencyid = data.table[0].Currencyid;
                        model.DosageFrom = data.table[0].dosageFrom;
                        model.ManufacturingSiteOrPartner = data.table[0].manufacturingSiteOrPartner;
                        model.Skus = data.table[0].skus;
                        model.Mspersentage = data.table[0].mspersentage;
                        model.TargetPriceScenario = data.table[0].targetPriceScenario;
                        model.ProjectStartDate = data.table[0].projectStartDate;
                        model.BatchManufacturing = (data.table[0].batchManufacturing);
                        model.ExpectedFilling = (data.table[0].expectedFilling);
                        model.ApprovalPeriodinDays = data.table[0].approvalPeriodinDays;
                        model.ApprovalDate = data.table[0].approvalDate;
                        model.ProductLaunchDate = data.table[0].productLaunchDate;
                        model.GestationPeriodinYears = Convert.ToDecimal(data.table[0].gestationPeriodinYears);
                        model.MarketShareErosionrate = Convert.ToDecimal(data.table[0].marketShareErosionrate);
                        model.PriceErosion = Convert.ToDecimal(data.table[0].priceErosion);
                        model.EscalationinCOGS = data.table[0].EscalationinCOGS;
                        model.DiscountRate = Convert.ToDecimal(data.table[0].discountRate);
                        model.Incometaxrate = Convert.ToDecimal(data.table[0].incometaxrate);
                        model.Opexasapercenttosale = data.table[0].opexasapercenttosale;
                        model.ExternalProfitSharepercent = data.table[0].externalProfitSharepercent;
                        model.CollectioninDays = data.table[0].collectioninDays;
                        model.InventoryinDays = data.table[0].inventoryinDays;
                        model.CreditorinDays = data.table[0].creditorinDays;
                        model.MarketingAllowance = Convert.ToDecimal(data.table[0].marketingAllowance);
                        model.RegulatoryMaintenanceCost = Convert.ToDecimal(data.table[0].regulatoryMaintenanceCost);
                        model.GrosstoNet = Convert.ToDecimal(data.table[0].grosstoNet);
                        model.Noofbatchestobemanufactured = Convert.ToDouble(data.table[0].noofbatchestobemanufactured);
                        model.NoofbatchestobemanufacturedPhaseEndDate = data.table[0].noofbatchestobemanufacturedPhaseEndDate;
                        model.NoSkus = Convert.ToDouble (data.table[0].noSkus);
                        model.NoSkusPhaseEndDate = data.table[0].noSkusPhaseEndDate;
                        model.RandDanalyticalcost = Convert.ToDecimal(data.table[0].randDanalyticalcost);
                        model.RandDanalyticalcostPhaseEndDate = data.table[0].randDanalyticalcostPhaseEndDate;
                        model.Rldsamplecost = Convert.ToDecimal(data.table[0].rldsamplecost);
                        model.RldsamplecostPhaseEndDate = data.table[0].rldsamplecostPhaseEndDate;
                        model.BatchmanufacturingcostOrApiactualsEst = Convert.ToDecimal(data.table[0].batchmanufacturingcostOrApiactualsEst);
                        model.BatchmanufacturingcostOrApiactualsEstPhaseEndDate = data.table[0].batchmanufacturingcostOrApiactualsEstPhaseEndDate;
                        model.Sixmonthsstabilitycost = Convert.ToDecimal(data.table[0].sixmonthsstabilitycost);
                        model.SixmonthsstabilitycostPhaseEndDate = data.table[0].sixmonthsstabilitycostPhaseEndDate;
                        model.TechTransfer = Convert.ToDecimal(data.table[0].techTransfer);
                        model.TechTransferPhaseEndDate = data.table[0].techTransferPhaseEndDate;
                        model.Bestudies = Convert.ToDecimal(data.table[0].bestudies);
                        model.BestudiesPhaseEndDate = data.table[0].bestudiesPhaseEndDate;
                        model.Filingfees = Convert.ToDecimal(data.table[0].filingfees);
                        model.FilingfeesPhaseEndDate = data.table[0].filingfeesPhaseEndDate;
                        model.BioStuddyCost = Convert.ToDecimal(data.table[0].bioStuddyCost);
                        model.BioStuddyCostPhaseEndDate = data.table[0].bioStuddyCostPhaseEndDate;
                        model.Capex = Convert.ToDecimal(data.table[0].capex);
                        model.CapexPhaseEndDate = data.table[0].capexPhaseEndDate;
                        model.ToolingAndChangeParts = Convert.ToDecimal(data.table[0].toolingAndChangeParts);
                        model.ToolingAndChangePartsPhaseEndDate = data.table[0].toolingAndChangePartsPhaseEndDate;
                        model.Total = Convert.ToDecimal(data.table[0].total);
                        model.PIDFStatusId = data.table[0].PIDFStatusId;

                        var lsitem = GetFinanceBatchSizeCoating(model.PidffinaceId,buid);
                        List<ChildPidfFinanceBatchSizeCoating> ls = new List<ChildPidfFinanceBatchSizeCoating>();
                        foreach (var item in lsitem)
                        {
                            try
                            {
                                ls.Add(new ChildPidfFinanceBatchSizeCoating
                                {
                                    PidffinaceBatchSizeCoatingId = item.pidfFinaceBatchSizeCoatingId,
                                    PidffinaceId = item.pidfFinaceId,
                                    Batchsize = item.batchsize,
                                    Yield = item.Yield,
                                    Batchoutput = item.batchoutput,
                                    ApiCad = item.apI_CAD,
                                    ExcipientsCad = item.excipients_CAD,
                                    PmCad = item.pM_CAD,
                                    CcpcCad = item.ccpC_CAD,
                                    FreightCad = item.freight_CAD,
                                    EmcureCogsPack = item.emcureCOGs_pack,
                                    CreatedDate = item.createdDate,
                                    CreatedBy = item.createdBy,
                                    Skus = item.Skus,
                                    PakeSize = item.PakeSize,
                                    BrandPrice = item.BrandPrice,
                                    NetRealisation = item.NetRealisation,
                                    GenericListprice = item.GenericListprice,
                                    EstMat2016By12units = item.EstMat2016By12units,
                                    EstMat2020By12units = item.EstMat2020By12units,
                                    Cagrover2016By12estMatunits = item.Cagrover2016By12estMatunits,
                                    Marketinpacks = item.Marketinpacks,
                                    BatchsizeinLtrTabs = item.BatchsizeinLtrTabs
                                });
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        model.lsPidfFinanceBatchSizeCoating = ls;
                        model.JsonlsPidfFinanceBatchSizeCoating = JsonConvert.SerializeObject(ls);
                        model.JsonCommercialData = GetManagmentApprovalBatchSizeCoating(pidfid, buid);

                    }
                    int rolId = _helper.GetLoggedInRoleId();
                    RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.Finance, rolId);
                    ViewBag.Access = objPermssion;

                    if (model.PidffinaceId > 0)
                        return model;
                    
                }
            }
            return model;
        }

        [NonAction]
        private List<ChildTable> GetFinanceBatchSizeCoating(int? PidfFinaceid,string buid)
        {
            try
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetFinanceBatchSizeCoating + "/" + PidfFinaceid, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ChildRoot>(jsonResponse);
                    return data.table;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _helper.LogExceptions(ex);
                throw;
            }
        }

        [NonAction]
        private string GetManagmentApprovalBatchSizeCoating(string PIDFID, string buid)
        {
            try
            {
                int _pidfid = Convert.ToInt32(UtilityHelper.Decreypt(Convert.ToString(PIDFID)));
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetManagmentApprovalBatchSizeCoating + "/" + _pidfid + "/" + buid, HttpMethod.Get, token).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    return jsonResponse;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _helper.LogExceptions(ex);
                throw;
            }
        }

    }
}