﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    
    public class PIDFAPIRnDFormEntity
    {
        public long PIDFAPIRnDFormID { get; set; }        
        public string Pidfid { get; set; }
        public string SaveType { get; set; }
        public string ProjectName { get; set; }
        public string BusinessUnitId { get; set; }
        public string MarketDetailsFileName { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
        public string DrugsCategory { get; set; }
        public string ProductStrength { get; set; }
        public int LoggedInUserId { get; set; }
        public PIDFormEntity IPEvalution { get; set; }
        public List<PIDF_IPD_PatentDetailsEntity> IPD_PatentDetailsList { get; set; }
        public PIDFCommercialEntity _commercialFormEntity { get; set; }
        [Required]
        public string IsModelValid { get; set; }
        /// <summary>
        /// //Formulation Quantity
        /// </summary>
        public string Development { get; set; }
        public string ScaleUp { get; set; }
        public string Exhibit { get; set; }
        public string PlantQC { get; set; }
        public string Total { get; set; }

        public int MarketID { get; set; }
        public string SponsorBusinessPartner { get; set; }
        public string APIMarketPrice { get; set; }
        public string APITargetRMC_CCPC { get; set; }
        
    }
}