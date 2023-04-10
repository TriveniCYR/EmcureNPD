using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Implementation;
using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ServiceImplementations;
using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.API.Controllers.Commercial
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommercialPIDFFormController : ControllerBase
    {
        #region Properties

        private readonly ICommercialFormService _pidfCommercialFormService;
        private readonly IMasterUserService _masterUserService;

        private readonly IResponseHandler<dynamic> _ObjectResponse;

        #endregion Properties

        #region Constructor

        public CommercialPIDFFormController(ICommercialFormService PIDFormService, IResponseHandler<dynamic> ObjectResponse,
            IMasterUserService masterUserService)
        {
            _pidfCommercialFormService = PIDFormService;
            _ObjectResponse = ObjectResponse;
            _masterUserService = masterUserService;
        }

        #endregion Constructor


        /// <summary>
        /// Description - To Get All Formulation
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
        //[HttpGet, Route("GetPIDForm")]
        //public async Task<IActionResult> GetPIDForm()
        //{
        //    var oFormulationList = await _pidfCommercialFormService.FillDropdown();

        //    if (oFormulationList != null)
        //        return _ObjectResponse.Create(oFormulationList, (int)HttpStatusCode.OK);
        //    else
        //        return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
        //}
        /// <summary>
        /// Description - To Insert and Update IPD Form
        /// </summary>
        /// <param name="oIPD"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="405">Method Not Allowed</response>
        /// <response code="500">Internal Server</response>

        /// <summary>
        /// Description - To Get All IPD PIDFList
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
        [HttpPost, Route("GetAllIPDPIDFList")]
        public async Task<IActionResult> GetAllIPDPIDFList([FromForm] DataTableAjaxPostModel model)
        {
            try
            {
                return _ObjectResponse.CreateData(await _pidfCommercialFormService.GetAllIPDPIDFList(model), (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        /// <summary>
        /// Description - To Get All Region Base on User
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
        [HttpGet, Route("GetAllRegion/{userId}")]
        public async Task<IActionResult> GetAllRegion(int userId)
        {
            try
            {
                var oRegionList = await _pidfCommercialFormService.GetAllRegion(userId);
                if (oRegionList != null)
                    return _ObjectResponse.Create(oRegionList, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpGet, Route("GetCountryRefByRegionIds/{regionIds}")]
        public async Task<IActionResult> GetCountryRefByRegionIds(string regionIds)
        {
            try
            {
                var oRegionList = await _pidfCommercialFormService.GetCountryRefByRegionIds(regionIds);
                if (oRegionList != null)
                    return _ObjectResponse.Create(oRegionList, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        [HttpPost]
        [Route("ApproveRejectIpdPidf")]
        public async Task<IActionResult> ApproveRejectIpdPidf(EntryApproveRej oApprRej)
        {
            try
            {
                DBOperation oResponse = await _pidfCommercialFormService.ApproveRejectIpdPidf(oApprRej);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (int)HttpStatusCode.OK, "Save Successfully");
                else
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.NotFound ? "Record not found" : "Bad request");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }
        /// <summary>
        /// Description - To Get IPD Form By Id
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
        [HttpGet, Route("GetCommercialFormData/{pidfId}/{bussnessId}/{strengthid}")]
        public async Task<IActionResult> GetCommercialFormData([FromRoute] long pidfId, int bussnessId, int? strengthid)
        {
            try
            {
                //pidfId = int.Parse(UtilityHelper.Decreypt(strpidfId));
                var oPIDFEntity = await _pidfCommercialFormService.GetCommercialFormData(pidfId, bussnessId, strengthid);
                if (oPIDFEntity != null)
                    return _ObjectResponse.Create(oPIDFEntity, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "Record not found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpPost]
        [Route("SaveCommercialPIDF")]
        public async Task<IActionResult> SaveCommercialPIDF(PIDFCommercialEntity commercialpidfobj)
        {
            try
            {
                commercialpidfobj.CreatedBy = int.Parse(UtilityHelper.Decreypt(commercialpidfobj.encCreatedBy));

                DBOperation oResponse = await _pidfCommercialFormService.AddUpdateCommercialPIDF(commercialpidfobj);
                if (oResponse == DBOperation.Success)
                    return _ObjectResponse.Create(true, (int)HttpStatusCode.OK, "Commercial Details Submitted");
                //return _ObjectResponse.Create(true, (Int32)HttpStatusCode.OK, (commercialpidfobj.PidfcommercialId> 0 ? "Updated Successfully" : "Inserted Successfully"));
                else
                    return _ObjectResponse.Create(false, (int)HttpStatusCode.BadRequest, oResponse == DBOperation.NotFound ? "Record not found" : "Bad request");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

        [HttpGet, Route("GetAllFinalSelection")]
        public async Task<IActionResult> GetAllFinalSelection()
        {
            try
            {
                var oFSList = await _pidfCommercialFormService.GetAllFinalSelection();
                if (oFSList != null)
                    return _ObjectResponse.Create(oFSList, (int)HttpStatusCode.OK);
                else
                    return _ObjectResponse.Create(null, (int)HttpStatusCode.BadRequest, "No Records found");
            }
            catch (Exception ex)
            {
                return _ObjectResponse.Create(false, (int)HttpStatusCode.InternalServerError, Convert.ToString(ex.StackTrace));
            }
        }

    }
}
