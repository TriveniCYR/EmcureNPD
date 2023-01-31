using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Resource;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ServiceImplementations;
using EmcureNPD.Business.Models;
using System.Net;
using System.Threading.Tasks;
using System;

namespace EmcureNPD.API.Controllers.Notificaiton {
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase {
        
        #region Properties
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly INotificationService _NotificationService;
        #endregion Properties

        #region Constructor

        public NotificationController(IConfiguration configuration, IResponseHandler<dynamic> ObjectResponse, INotificationService NotificationService) {
            _configuration = configuration;
            _ObjectResponse = ObjectResponse;
            _NotificationService = NotificationService;
        }

        #endregion Constructor

        /// <summary>
        /// Description - To Get All Notification
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
        [HttpPost, Route("GetAllNotification")]
        public async Task<IActionResult> GetAllNotification([FromForm] DataTableAjaxPostModel model) {
            try {
                return _ObjectResponse.CreateData(await _NotificationService.GetAll(model), (Int32)HttpStatusCode.OK);
            } catch (Exception ex) {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
    }
}
