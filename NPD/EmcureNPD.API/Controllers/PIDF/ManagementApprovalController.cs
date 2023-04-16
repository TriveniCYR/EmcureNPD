using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmcureNPD.API.Controllers.PIDF
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class ManagementApprovalController : ControllerBase
    {
        #region Properties

        private readonly IManagementApproval _managementApproval;

        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExceptionService _ExceptionService;

        #endregion Properties

        #region Constructor

        public ManagementApprovalController(IManagementApproval managementApproval, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment, IExceptionService exceptionService)
        {
            _managementApproval = managementApproval;
            _ObjectResponse = ObjectResponse;
            _webHostEnvironment = webHostEnvironment;
            _ExceptionService = exceptionService;
        }

        #endregion Constructor

        /// <summary>
		/// Description - To Get ProjectNameAndStrength
		/// </summary>
		/// <param name="oGetProjectNameAndStrength"></param>
		/// <returns></returns>
		/// <response code="200">OK</response>
		/// <response code="400">Bad Request</response>
		/// <response code="403">Forbidden</response>
		/// <response code="404">Not Found</response>
		/// <response code="405">Method Not Allowed</response>
		/// <response code="500">Internal Server</response>
		[HttpGet]
        [Route("GetProjectNameAndStrength/{Pidfid}")]
        public async Task<IActionResult> GetProjectNameAndStrength(string Pidfid)
        {
            try
            {
                Pidfid = UtilityHelper.Decreypt(Convert.ToString(Pidfid));
                return _ObjectResponse.CreateData(await _managementApproval.GetProjectNameAndStrength(Convert.ToInt32(Pidfid)), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
    }
}