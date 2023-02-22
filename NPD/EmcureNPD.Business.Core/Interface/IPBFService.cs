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
	public interface IPBFService
	{
		Task<dynamic> FillDropdown();
		Task<PidfPbfEntity> GetPbfFormData(long pidfId, int buid, int? strengthid);
		Task<DBOperation> AddUpdatePBF(PidfPbfEntity pbfEntity);
		
		//------------Start------API_IPD_Details_Form_Entity--------------------------
        Task<DBOperation> AddUpdateAPIIPD(IFormCollection entityPIDF,string _webrootPath);

		Task<PIDFAPIIPDFormEntity> GetAPIIPDFormData(long pidfId, string _webrootPath);
        //------------End------API_IPD_Details_Form_Entity--------------------------
        Task<PIDFAPIRnDFormEntity> GetAPIRnDFormData(long pidfId, string _webrootPath);
		Task<DBOperation> AddUpdateAPIRnD(PIDFAPIRnDFormEntity _oAPIRnD);
		Task<PidfPbfAnalyticalEntity> GetPBFAnalyticalReadonlyData(long pidfid);
	}	
}
