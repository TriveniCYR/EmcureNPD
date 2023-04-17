using Microsoft.AspNetCore.Mvc;
using EmcureNPD.Business.Core.Interface;
using System.Threading.Tasks;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Models;
using System;
using System.Net;
namespace EmcureNPD.API.Controllers.Exceptions
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogExceptionController : ControllerBase
    {
        #region Properties

        private readonly IExceptionService _ExceptionService;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        #endregion Properties

        #region Constructor

        public LogExceptionController(IExceptionService exceptionService, IResponseHandler<dynamic> ObjectResponse)
        {
            _ExceptionService = exceptionService;
            _ObjectResponse = ObjectResponse;
        }

        #endregion Constructor
        [HttpPost]
        [Route("LogException")]
        public async Task<IActionResult> LogException(Exception ex)
        {
            Utility.Enums.GeneralEnum.DBOperation oResponse = await _ExceptionService.LogException(ex);
            if (oResponse == Utility.Enums.GeneralEnum.DBOperation.Success)
                return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK);
            else
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest);
        }
    }
}
