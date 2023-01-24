using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class APIIPDDetailsFormEntity
    {        
        public long Pidfid { get; set; }
        public string ProjectName { get; set; }
        public string MarketDetailsNewPortCGIDetails { get; set; }
        public string ProductType { get; set; }
        public string DrugsCategory { get; set; }
        public string ProductStrength { get; set; }

        public PIDFormEntity IPEvalution { get; set; }
        public List<PIDF_IPD_PatentDetailsEntity> IPD_PatentDetailsList { get; set; }

        /// <summary>
        /// //Formulation Quantity
        /// </summary>
        public string FormulationQuantity { get; set; }
        public string Development { get; set; }
        public string ScaleUp { get; set; }
        public string Exhibit { get; set; }
        public string PlantQC { get; set; }
        public string Total { get; set; }

    }
}
