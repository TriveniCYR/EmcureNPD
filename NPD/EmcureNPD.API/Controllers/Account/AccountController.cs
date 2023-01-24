using EmcureNPD.API.Helpers.Response;
using EmcureNPD.API.Middlewares;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using EmcureNPD.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Net;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Properties

        private readonly IConfiguration _configuration;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IMasterUserService _MasterUserService;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        #endregion Properties

        #region Constructor

        public AccountController(IConfiguration configuration, IResponseHandler<dynamic> ObjectResponse, IMasterUserService MasterUserService, IStringLocalizer<Errors> stringLocalizerError)
        {
            _configuration = configuration;
            _ObjectResponse = ObjectResponse;
            _MasterUserService = MasterUserService;
            _stringLocalizerError = stringLocalizerError;
        }

        #endregion Constructor

        #region API Methods

        /// <summary>
        /// Description - To Login User and return JWT Token String
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="415">Unsupported Media Type</response>
        /// <response code="500">Internal Server</response>
        [AllowAnonymous]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel oLogin)
        {
            try
            {
                var _User = await _MasterUserService.Login(oLogin);

                if (_User != null)
                {
                    int expMinutes = Convert.ToInt32(_configuration["jwt:expiryMinutes"]);

                    var userEntity = JwtAuthenticationServiceConfig.ValidateToken(_User, _configuration["jwt:audience"].ToString(),
                        _configuration["jwt:issuer"].ToString(), Guid.NewGuid(), DateTime.Now.AddMinutes(expMinutes), _configuration["jwt:secretKey"]);

                    return _ObjectResponse.Create(userEntity, (Int32)HttpStatusCode.OK);
                }
                else
                    return _ObjectResponse.Create(string.Empty, (Int32)HttpStatusCode.Unauthorized, _stringLocalizerError["LoinFailed"]);
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [AllowAnonymous]
        [HttpGet, Route("GetAllBusinessUnit")]
        public async Task<IActionResult> GetAllBusinessUnit()
        {
            try
            {
                var oBusinessUnitList = await _MasterUserService.GetAll();
                if (oBusinessUnitList != null)
                    return _ObjectResponse.Create(oBusinessUnitList, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [AllowAnonymous]
        [HttpPost, Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel forgotPasswordViewModel)
        {
            try
            {
                var _forgotPasswordOperation = await _MasterUserService.ForgotPassword(forgotPasswordViewModel.Email);
              
                if (_forgotPasswordOperation == DBOperation.Success)
                    return _ObjectResponse.Create(_forgotPasswordOperation, (Int32)HttpStatusCode.OK);
                else if(_forgotPasswordOperation == DBOperation.NotFound)
                {
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
                }
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [AllowAnonymous]
        [HttpPost, Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] MasterUserResetPasswordEntity ResetPasswordViewModel)
        {
            try
            {
                var resetOperation = await _MasterUserService.ResetPassword(ResetPasswordViewModel);                
                if (resetOperation == DBOperation.Success)
                    return _ObjectResponse.Create(resetOperation, (Int32)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");

            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        
        #endregion API Methods
    }
}