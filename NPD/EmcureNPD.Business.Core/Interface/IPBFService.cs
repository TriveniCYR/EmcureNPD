﻿using EmcureNPD.Business.Models;
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
        Task<DBOperation> AddUpdatePBFDetailsAnalytical(PidfPbfFormEntity pbfEntity);
        Task<PidfPbfFormEntity> GetPbfFormDetailsAnalytical(long pidfId, int buid, int strengthid);
        
        Task<dynamic> FillDropdown();
		Task<PidfPbfEntity> GetPbfFormData(long pidfId, int buid, int? strengthid);
		Task<DBOperation> AddUpdatePBF(PidfPbfEntity pbfEntity);		
		Task<PidfPbfAnalyticalEntity> GetPBFAnalyticalReadonlyData(long pidfid);

        // ---------------------------PBFDetails----------------------------       
        Task<DBOperation> AddUpdatePBFDetails(PidfPbfFormEntity pbfEntity);
        //Task<PidfPbfFormEntity> GetPbfFormDetails(long pidfId, int buid, int? strengthid);
		// ---------------------------PBFClinicalDetails----------------------------
		Task<DBOperation> AddUpdatePBFClinicalDetails(PIDFPBFClinicalFormEntity pbfClinicalEntity);
		Task<PIDFPBFClinicalFormEntity> GetPbfClinicalFormDetails(long pidfId, int buid, long? strengthid);

	}
}
