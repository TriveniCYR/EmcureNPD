using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
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
using System.Linq;
using System.Net.Http;


namespace EmcureNPD.Web.Controllers
{
	public class FinanceController : BaseController
	{
		#region Properties
		private readonly IConfiguration _cofiguration;
		private readonly IStringLocalizer<Errors> _stringLocalizerError;
		private readonly IStringLocalizer<Shared> _stringLocalizerShared;
		private readonly IStringLocalizer<Master> _stringLocalizerMaster;
		#endregion
		public FinanceController(IConfiguration configuration, IStringLocalizer<Master> stringLocalizerMaster,

			IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared)
		{
			_cofiguration = configuration;
			_stringLocalizerError = stringLocalizerError;
			_stringLocalizerShared = stringLocalizerShared;
			_stringLocalizerMaster = stringLocalizerMaster;

		}
		public IActionResult PIDFFinance()
		{
			var pidfid = Request.Query["pidfid"];
			if (pidfid != "")
			{
				FinanceModel model = new FinanceModel();
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
						model.PidffinaceId = data.table[0].pidffinaceId;
						model.Pidfid = UtilityHelper.Encrypt(Convert.ToString(data.table[0].pidfid));
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
						model.BatchManufacturing = data.table[0].batchManufacturing;
						model.ExpectedFilling = data.table[0].expectedFilling;
						model.ApprovalPeriodinDays = data.table[0].approvalPeriodinDays;
						model.ApprovalDate = data.table[0].approvalDate;
						model.ProductLaunchDate = data.table[0].productLaunchDate;
						model.GestationPeriodinYears = data.table[0].gestationPeriodinYears;
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
						model.Noofbatchestobemanufactured = Convert.ToInt32(data.table[0].noofbatchestobemanufactured);
						model.NoofbatchestobemanufacturedPhaseEndDate = data.table[0].noofbatchestobemanufacturedPhaseEndDate;
						model.NoSkus = Convert.ToInt32(data.table[0].noSkus);
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
						var lsitem = GetFinanceBatchSizeCoating(model.PidffinaceId);
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
									CreatedBy = item.createdBy

								});
							}
							catch (Exception ex)
							{
							}

						}
						model.lsPidfFinanceBatchSizeCoating = ls;
					}
                    int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                    RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess(Convert.ToString(RouteData.Values["controller"]), rolId);
                    ViewBag.Access = objPermssion;
                    if (model.PidffinaceId > 0)
						return View(model);
					else
						return View();

				}
			}
			return View();
		}
		public IActionResult PIDFManagementApproval()
		{
            var pidfid = Request.Query["pidfid"];
			if (pidfid != "")
			{
				ProjectsModel model = new ProjectsModel();
				List<ProjectNameModel> ListProjectName = new List<ProjectNameModel>();
				List<ProjectStrength> ListprojectStrengths = new List<ProjectStrength>();
                List<Manager> Listmanager = new List<Manager>();
				List<HeadWiseBudget> ListHeadWiseBudget=new List<HeadWiseBudget>();
                List<ProjectDetails> ListProjectDetails = new List<ProjectDetails>();
				List<CumulativePhaseWiseBudget> ListCumulativePhaseWiseBudget=new List<CumulativePhaseWiseBudget>();
				List<Deliverables> ListDeliverables = new List<Deliverables>();
                HttpResponseMessage responseMessage = new HttpResponseMessage();
				HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
				APIRepository objapi = new(_cofiguration);

				responseMessage = objapi.APICommunication(APIURLHelper.GetProjectNameAndStrength + "/" + pidfid, HttpMethod.Get, token).Result;

				if (responseMessage.IsSuccessStatusCode)
				{
					string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ProjectsView>(jsonResponse);
					if(data.table.Count > 0)
					{
						foreach (var item in data.table)
						{
                           ListProjectName.Add(new ProjectNameModel
                            {
                                ProjectName = item.projectName

                            });
                        }
						model.lsProjectName = ListProjectName;

                    }
					if(data.table1.Count>0)
					{
                        foreach (var item in data.table1)
                        {
                          ListprojectStrengths.Add(new ProjectStrength
                            {
                                Strength = item.strength

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
                                ManagerName = item.managerName,
                                DesignationName=item.designationName

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
                                Prototype= data.table3[i].Prototype,
								ScaleUp= data.table3[i].ScaleUp,
                                Exhibit= data.table3[i].Exhibit,
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
                                Row = data.table4[i].Row
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
                                CostHeads = data.table5[i].CostHeads,
                                PercentOfTotal = data.table5[i].PercentOfTotal,
                                CostRsLacs = data.table5[i].CostRsLacs,
                            });
                        }
                        model.lsCumulativePhaseWiseBudget = ListCumulativePhaseWiseBudget;
                    }
                    if (data.table6.Count > 0)
                    {
                        for (int i = 0; i < data.table6.Count; i++)
                        {
                            ListDeliverables.Add(new Deliverables
                            {
                                PharmacoepialStandardsonQuality = data.table6[i].PharmacoepialStandardsonQuality,
                                Row = data.table6[i].Row
                            });
                        }
                        model.lsDeliverables = ListDeliverables;
                    }
                    return View(model);
                }
			}
               return View();
		}


		[HttpPost]
		public IActionResult PIDFFinancePost(FinanceModel PidfFinanceEntity)
		{
			PidfFinanceEntity.CreatedBy = Convert.ToInt32(HttpContext.Session.GetString(UserHelper.LoggedInUserId));
			HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
			APIRepository objapi = new(_cofiguration);

			string JsonPidfFinance = JsonConvert.SerializeObject(PidfFinanceEntity);
			HttpResponseMessage responseMessage = objapi.APICommunication(APIURLHelper.AddUpdatePidfFinance, HttpMethod.Post, token, new StringContent(JsonConvert.SerializeObject(PidfFinanceEntity))).Result;
			if (responseMessage.IsSuccessStatusCode)
			{
				TempData[UserHelper.SuccessMessage] = Convert.ToString(_stringLocalizerShared["RecordInsertUpdate"]);
				return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 7 });
			}
			else
			{
				TempData[UserHelper.ErrorMessage] = Convert.ToString(responseMessage.Content.ReadAsStringAsync().Result);
                return RedirectToAction("PIDFList", "PIDF", new { ScreenId = 7 });
			}
		}
		[NonAction]
		private List<ChildTable> GetFinanceBatchSizeCoating(int? PidfFinaceid)
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
				throw;
			}
		}
	}
}
