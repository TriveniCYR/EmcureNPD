using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmcureNPD.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]

    public class ModuleController : ControllerBase
    {
        #region Properties

        private readonly IMasterModuleService _MasterModuleService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        public ModuleController(IMasterModuleService MasterModuleService, IResponseHandler<dynamic> ObjectResponse)
        {
            _MasterModuleService = MasterModuleService;

            _ObjectResponse = ObjectResponse;
        }

        /// <summary>
        /// Description - To Get All Module
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
        [HttpGet, Route("GetAllModule")]
        public async Task<IActionResult> GetAllModule()
        {
            try
            {
                var oModuleList = await _MasterModuleService.GetAll();
                if (oModuleList != null)
                    return _ObjectResponse.Create(oModuleList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

    }
}
