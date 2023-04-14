﻿using EmcureNPD.Business.Models;
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
    public class ManagementController : BaseController
    {
        #region Properties
        private readonly IConfiguration _cofiguration;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly IStringLocalizer<Shared> _stringLocalizerShared;
        private readonly IStringLocalizer<Master> _stringLocalizerMaster;
        #endregion
        public ManagementController(IConfiguration configuration, IStringLocalizer<Master> stringLocalizerMaster,

            IStringLocalizer<Errors> stringLocalizerError, IStringLocalizer<Shared> stringLocalizerShared)
        {
            _cofiguration = configuration;
            _stringLocalizerError = stringLocalizerError;
            _stringLocalizerShared = stringLocalizerShared;
            _stringLocalizerMaster = stringLocalizerMaster;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PIDFManagementApproval()
        {
            int rolId = (int)HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
            RolePermissionModel objPermssion = UtilityHelper.GetCntrActionAccess((int)ModulePermissionEnum.ManagementHOD, rolId);
            ViewBag.Access = objPermssion;
            var pidfid = Request.Query["pidfid"];
            if (pidfid != "")
            {
                ProjectsModel model = new ProjectsModel();
                List<ProjectNameModel> ListProjectName = new List<ProjectNameModel>();
                List<ProjectStrength> ListprojectStrengths = new List<ProjectStrength>();
                List<Manager> Listmanager = new List<Manager>();
                List<HeadWiseBudget> ListHeadWiseBudget = new List<HeadWiseBudget>();
                List<ProjectDetails> ListProjectDetails = new List<ProjectDetails>();
                List<CumulativePhaseWiseBudget> ListCumulativePhaseWiseBudget = new List<CumulativePhaseWiseBudget>();
                List<Deliverables> ListDeliverables = new List<Deliverables>();
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                HttpContext.Request.Cookies.TryGetValue(UserHelper.EmcureNPDToken, out string token);
                APIRepository objapi = new(_cofiguration);

                responseMessage = objapi.APICommunication(APIURLHelper.GetProjectNameAndStrength + "/" + pidfid, HttpMethod.Get, token).Result;

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
                                ProjectName = item.projectName

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
    }
}