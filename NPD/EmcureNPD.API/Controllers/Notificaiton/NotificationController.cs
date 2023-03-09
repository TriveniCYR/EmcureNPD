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
using Microsoft.AspNetCore.SignalR;
using EmcureNPD.Business.Core;

namespace EmcureNPD.API.Controllers.Notificaiton {
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase {

        #region Properties
        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly INotificationService _NotificationService;
        private readonly IHelper _helper;

        #endregion Properties

        #region Constructor

        public NotificationController(IConfiguration configuration, IResponseHandler<dynamic> ObjectResponse, INotificationService NotificationService, IHelper helper) {
            _configuration = configuration;
            _ObjectResponse = ObjectResponse;
            _NotificationService = NotificationService;
            _helper = helper;
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
        [HttpGet, Route("GetAllNotification")]
        public async Task<IActionResult> GetAllNotification() {
            try {
                return _ObjectResponse.CreateData(await _NotificationService.GetAll(), (Int32)HttpStatusCode.OK);
            } catch (Exception ex) {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetFilteredNotifications/{ColumnName}/{SortDir}/{start}/{length}")]
        [OutputCache(Duration = 120, VaryByParam = "RoleId")]
        public async Task<IActionResult> GetFilteredNotifications(string ColumnName, string SortDir, int start, int length,int RoleId)
		{
			try
			{
                RoleId = _helper.GetLoggedInUser().RoleId;
                return _ObjectResponse.CreateData(await _NotificationService.GetFilteredNotifications(ColumnName, SortDir, start, length,RoleId), (Int32)HttpStatusCode.OK);
			}
			catch (Exception ex)
			{
				return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
			}
		}
	}
}
