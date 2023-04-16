using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmcureNPD.API.Controllers.Notificaiton
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class NotificationController : ControllerBase
    {
        #region Properties

        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly INotificationService _NotificationService;
        private readonly ILogger<NotificationController> _logger;
        private readonly IHelper _helper;
        private readonly IExceptionService _ExceptionService;

        #endregion Properties

        #region Constructor

        public NotificationController(IConfiguration configuration, IResponseHandler<dynamic> ObjectResponse, INotificationService NotificationService, IHelper helper, ILogger<NotificationController> logger, IExceptionService exceptionService)
        {
            _configuration = configuration;
            _ObjectResponse = ObjectResponse;
            _NotificationService = NotificationService;
            _helper = helper;
            _logger = logger;
            _ExceptionService = exceptionService;
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
        public async Task<IActionResult> GetAllNotification([FromForm] DataTableAjaxPostModel model)
        {
            try
            {
                return _ObjectResponse.CreateData(await _NotificationService.GetAll(model), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                _logger.LogInformation($"ERROR:Notifications/GetAllNotification:{ex}");
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("GetFilteredNotifications/{ColumnName}/{SortDir}/{start}/{length}")]
        [OutputCache(Duration = 120, VaryByParam = "RoleId")]
        public async Task<IActionResult> GetFilteredNotifications(string ColumnName, string SortDir, int start, int length, int RoleId)
        {
            try
            {
                RoleId = _helper.GetLoggedInUser().RoleId;
                return _ObjectResponse.CreateData(await _NotificationService.GetFilteredNotifications(ColumnName, SortDir, start, length, RoleId), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                _logger.LogInformation($"ERROR:Notifications/GetFilteredNotifications:{ex}");
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("NotificationsClickedByUser")]
        public async Task<IActionResult> NotificationsClickedByUser()
        {
            try
            {
                return _ObjectResponse.CreateData(await _NotificationService.ClickedNotification(), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                _logger.LogInformation($"ERROR:Notifications/GetFilteredNotifications:{ex}");
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("NotificationsCountUser")]
        public async Task<IActionResult> NotificationsCountUser()
        {
            try
            {
                return _ObjectResponse.CreateData(await _NotificationService.NotificationCountForUser(), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                _logger.LogInformation($"ERROR:Notifications/GetFilteredNotifications:{ex}");
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
    }
}