﻿using EmcureNPD.Resource.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public partial class PidfPbfAnalyticalEntity
    {
        public long AnalyticalPIDFID { get; set; }
        public string PIDFNO { get; set; }
        public int AnalyticalBusinessUnitId { get; set; }
        public string ProjectName { get; set; }

        public string SAPProjectProjectCode { get; set; }

        public string ImprintingEmbossingCodes { get; set; }

        public string TotalExpenses { get; set; }

        public string ProjectComplexity { get; set; }

        public int? AnalyticalProductTypeId { get; set; }
        public int StrengthId { get; set; }

        public int AnalyticalFormulationGLId { get; set; }
        public int AnalyticalAnalyticalGLId { get; set; }
        public string AnalyticalLicence { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }

        public int StatusId { get; set; }

        public int LastStatusId { get; set; }
        public string SaveType { get; set; }
        public int? LogInId { get; set; }
        public List<PidfPbfRnDExicipientProtoypeEntity> ExicipientProtoypeEntities { get; set; }
        public List<PidfProductStregthEntity> ProductStrength { get; set; }
    }
    
}