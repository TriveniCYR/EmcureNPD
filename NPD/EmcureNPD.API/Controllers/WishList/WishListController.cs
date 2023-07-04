using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Implementation;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.WishList
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeAttribute]
    public class WishListController : ControllerBase
    {
        #region Properties

        private readonly IWishList _wishListService;
        private readonly IResponseHandler<dynamic> _ObjectResponse;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExceptionService _ExceptionService;

        #endregion Properties
        #region Constructor

        public WishListController(IWishList wishListService, IResponseHandler<dynamic> ObjectResponse, IWebHostEnvironment webHostEnvironment, IExceptionService exceptionService)
        {
            _wishListService = wishListService;
            _ObjectResponse = ObjectResponse;
            _webHostEnvironment = webHostEnvironment;
            _ExceptionService = exceptionService;
        }

        #endregion Constructor
        /// <summary>
        /// Description - To AddUpdateWishList
        /// </summary>
        /// <param name="oAddUpdateWishList"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>
        [HttpPost]
        [Route("AddUpdateWishList")]
        public async Task<IActionResult> AddUpdateWishList(WishListEntity wishListEntity)
        {
            try
            {
                
                DBOperation oResponse = await _wishListService.AddUpdateWishList(wishListEntity);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (wishListEntity.WishListId > 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (Int32)HttpStatusCode.BadRequest, (oResponse == DBOperation.NotFound ? "Record not found" : "Bad request"));
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(false, (Int32)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get PidfFinance
        /// </summary>
        /// <param name="oGetWishList"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response> 
        [HttpGet]
        [Route("GetWishListById/{id}")]
        public async Task<IActionResult> GetWishList(int id)
        {
            try
            {
               // Pidfid = UtilityHelper.Decreypt(Convert.ToString(Pidfid));
                return _ObjectResponse.CreateData(await _wishListService.GetById(id), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }

        /// <summary>
        /// Description - To Get GetWishListByTypeId
        /// </summary>
        /// <param name="oGetWishListByTypeId"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response> 
        [HttpGet]
        [Route("GetWishListByTypeId/{id}")]
        public async Task<IActionResult> GetFinanceBatchSizeCoating(int id)
        {
            try
            {
                return _ObjectResponse.CreateData(await _wishListService.GetWishListByTypeId(id), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
        [HttpGet]
        [Route("GetWishList")]
        public async Task<IActionResult> GetWishList()
        {
            try
            {
                return _ObjectResponse.CreateData(await _wishListService.GetAll(), (Int32)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return _ObjectResponse.Create(null, (Int32)HttpStatusCode.BadRequest, "No Records found");
            }
        }
    }
}
