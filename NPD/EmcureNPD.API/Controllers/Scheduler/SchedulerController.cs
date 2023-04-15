using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmcureNPD.API.Controllers.Scheduler
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        #region Properties

        private readonly ISchedulerService _reminderService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IExceptionService _ExceptionService;

        #endregion Properties

        #region Constructor

        public SchedulerController(ISchedulerService reminderService, IResponseHandler<dynamic> ObjectResponse, IExceptionService exceptionService)
        {
            _reminderService = reminderService;
            _ObjectResponse = ObjectResponse;
            _ExceptionService = exceptionService;
        }

        #endregion Constructor

        /// <summary>
        /// Description - Send Reminder
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [AllowAnonymous]
        [HttpPost, Route("SendReminder")]
        public async Task<IActionResult> SendReminder()
        {
            try
            {
                return _ObjectResponse.Create(_reminderService.SendReminder(), (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - Send Reminder
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [AllowAnonymous]
        [HttpPost, Route("AutoUpdatePIDFStatus")]
        public async Task<IActionResult> AutoUpdatePIDFStatus()
        {
            try
            {
                return _ObjectResponse.Create(_reminderService.AutoUpdatePIDFStatus(), (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
    }
}