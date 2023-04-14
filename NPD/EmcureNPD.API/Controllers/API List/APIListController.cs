using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmcureNPD.API.Controllers.Masters
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class APIListController : ControllerBase
    {
        #region Properties

        private readonly IAPIListService _APIListService;

        private readonly IResponseHandler<dynamic> _ObjectResponse; 
        private readonly IExceptionService _ExceptionService;

        #endregion Properties

        #region Constructor

        public APIListController(IAPIListService APIListService, IResponseHandler<dynamic> ObjectResponse, IExceptionService exceptionService)
        {
            _APIListService = APIListService;
            _ObjectResponse = ObjectResponse;
            _ExceptionService = exceptionService;
        }

        #endregion Constructor

        /// <summary>
        /// Description - To Get All PIDFList
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
        [HttpPost, Route("GetAllAPIList")]
        public async Task<IActionResult> GetAllAPIList([FromForm] DataTableAjaxPostModel model)
        {
            try
            {
                return _ObjectResponse.CreateData(await _APIListService.GetAllAPIList(model), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
    }
}
