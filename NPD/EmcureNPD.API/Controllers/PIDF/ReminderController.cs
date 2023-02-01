using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmcureNPD.API.Controllers.PIDF {
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class ReminderController : ControllerBase {
        #region Properties

        private readonly IReminderService _reminderService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public ReminderController(IReminderService reminderService, IResponseHandler<dynamic> ObjectResponse) {
            _reminderService = reminderService;
            _ObjectResponse = ObjectResponse;
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
        [HttpPost, Route("SendReminder")]
        public async Task<IActionResult> SendReminder() {
            try {
                return _ObjectResponse.Create(_reminderService.SendReminder(), (Int32)HttpStatusCode.OK);
            } catch (Exception ex) {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

    }
}
