using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.API.Filters;
using EmcureNPD.Business.Core.Implementation;

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

        #endregion Properties

        #region Constructor

        public DashboardController(IDashboardService DashboardService, IResponseHandler<dynamic> ObjectResponse)
        {
           
            _ObjectResponse = ObjectResponse;
            _dashboardService = DashboardService;
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
}
