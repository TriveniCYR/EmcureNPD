using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
	public interface IAPIService
	{
		
		//------------Start------API_IPD_Details_Form_Entity--------------------------
        Task<DBOperation> AddUpdateAPIIPD(IFormCollection entityPIDF,string _webrootPath);

		Task<PIDFAPIIPDFormEntity> GetAPIIPDFormData(long pidfId, string APIurl);
        //------------End------API_IPD_Details_Form_Entity--------------------------
        Task<PIDFAPIRnDFormEntity> GetAPIRnDFormData(long pidfId, string APIurl);
        Task<DBOperation> AddUpdateAPIRnD(PIDFAPIRnDFormEntity _oAPIRnD);
        Task<PIDFAPICharterFormEntity> GetAPICharterFormData(long pidfId,short IsCharter);
        Task<DBOperation> AddUpdateAPICharter(PIDFAPICharterFormEntity _oAPICharter);
        Task<PIDFAPICharterFormEntity> GetAPICharterSummaryFormData(long pidfId);
        Task<APIIPDEntity> GetIPDByPIDF(long pidfId);
    }	
}
