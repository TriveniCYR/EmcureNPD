using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.API.Filters;
using EmcureNPD.Business.Core.Implementation;
using Newtonsoft.Json;
using System.Web.Http.ModelBinding;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;

namespace EmcureNPD.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class DashboardController : ControllerBase
    {
        #region Properties
       
        private readonly IDashboardService _dashboardService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;
      //  private readonly IDashboardReportService _dashboardReportService;

        #endregion Properties

        #region Constructor

        public DashboardController(IDashboardService DashboardService, IResponseHandler<dynamic> ObjectResponse) //IDashboardReportService dashboardReportService)
        {
           
            _ObjectResponse = ObjectResponse;
            _dashboardService = DashboardService;
           // _dashboardReportService = dashboardReportService;
        }

        #endregion Constructor

        /// <summary>
        /// Description - To Get All Formulation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpGet, Route("FillDropdown")]
        public async Task<IActionResult> FillDropdown()
        {
            try
            {
                var data = await _dashboardService.FillDropdown();
                return _ObjectResponse.CreateData(data, (Int32)HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }

        [HttpGet, Route("GetPIDFByYearAndBusinessUnit/{id}/{years}")]
        public async Task<IActionResult> GetPIDFByYearAndBusinessUnit([FromRoute] int id,[FromRoute]string years)
        {
            try
            {
                var data = await _dashboardService.GetPIDFByYearAndBusinessUnit(id,years);
                return _ObjectResponse.CreateData(data, (Int32)HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
    }

    /*[ActionName("GetDashboardData1")]
    [HttpPost]
    public IActionResult GetDashboardData1(DashboardRequestVM dashboardRequestVM)//string fromDate, string toDate)
    {
        string fromdate;
        string todate;
        string countryName;
        int CompanyID = Convert.ToInt32(HttpContext.Session.GetString("CurrentUserCompanyID"));

        if (string.IsNullOrEmpty(dashboardRequestVM.fromDate) && string.IsNullOrEmpty(dashboardRequestVM.toDate) && string.IsNullOrEmpty(dashboardRequestVM.countryName))
        {
            fromdate = DateTime.Now.AddDays(-365).ToString("yyyy-MM-dd");
            todate = DateTime.Now.ToString("yyyy-MM-dd");
            countryName = null;
        }
        else
        {
            fromdate = dashboardRequestVM.fromDate;
            todate = dashboardRequestVM.toDate;
            countryName = dashboardRequestVM.countryName;

        }

        ModelState.Clear();
        DashboardDetails dashboardDetails = new DashboardDetails();
        var model = _dashboardReportService.dashboardDetails(fromdate, todate, countryName, CompanyID);

        dashboardDetails.dashboardTabDatas.totalInitial = model.dashboardTabDatas.totalInitial;
        dashboardDetails.dashboardTabDatas.totalRejected = model.dashboardTabDatas.totalRejected;
        dashboardDetails.dashboardTabDatas.totalInitialApproved = model.dashboardTabDatas.totalInitialApproved;
        dashboardDetails.dashboardTabDatas.totalPartialApproved = model.dashboardTabDatas.totalPartialApproved;
        dashboardDetails.dashboardTabDatas.totalPendingFinanceApproval = model.dashboardTabDatas.totalPendingFinanceApproval;
        dashboardDetails.dashboardTabDatas.totalFinanceApproved = model.dashboardTabDatas.totalFinanceApproved;
        dashboardDetails.dashboardTabDatas.totalFinalApproved = model.dashboardTabDatas.totalFinalApproved;
        dashboardDetails.dashboardTabDatas.totalFinalRejected = model.dashboardTabDatas.totalFinalRejected;

        //dynamic exo = new System.Dynamic.ExpandoObject();

        //foreach (var ddata in model.dashboardWorldMapDatas)
        //{
        //    ((IDictionary<String, Object>)exo).Add(ddata.countryCode, ddata.maxCount);
        //}
        dynamic exo = new System.Dynamic.ExpandoObject();
        string sampledata = "";
        foreach (var ddata in model.dashboardWorldMapDatas)
        {
            sampledata += ddata.countryCode + ":" + ddata.maxCount + ",";
            //((IDictionary<String, Object>)exo).Add(ddata.countryCode, ddata.maxCount);
        }

        dashboardDetails.dashboardWorldMapDatas = model.dashboardWorldMapDatas;
        sampledata = sampledata.Remove(sampledata.Length - 1, 1);
        sampledata = "{" + sampledata + "}";
        return Json(new { data = dashboardDetails }, new JsonSerializerSettings());
    }*/
}
