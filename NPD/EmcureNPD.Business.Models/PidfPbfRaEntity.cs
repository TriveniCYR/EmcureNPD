using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PidfPbfRaEntity
    {
        public long Pidfpbfraid { get; set; }
        public long Pidfid { get; set; }
        public long Pbfid { get; set; }
        public int CountryIdBuId { get; set; }
        public DateTime? PivotalBatchManufactured { get; set; }
        public DateTime? LastDataFromRnD { get; set; }
        public DateTime? BefinalReport { get; set; }
        public int BuId { get; set; }
        public int? TypeOfSubmissionId { get; set; }
        public DateTime? DossierReadyDate { get; set; }
        public DateTime? EarliestSubmissionDexcl { get; set; }
        public DateTime? EarliestLaunchDexcl { get; set; }
        public DateTime? LasDateToRegulatory { get; set; } 
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class RaCalculatedDates
    {

        public DateTime? EarliestSubmissionDate { get; set; }
        public DateTime?EarliestLaunchDate { get; set; }
        public DateTime?LastDateToRegulatory { get; set; }
        public int PIDFId { get; set; }
        public int BusinessUnitId { get; set; }
        public int CountryId { get; set; }
        public int TypeOfSubmissionId { get; set; }
        public DateTime? DossierReadyDate { get; set; }
        public DateTime? PivotalBatchManufactured { get; set; }
        public DateTime? LastDataFromRnD { get; set; }
        public DateTime? BEFinalReport { get; set; }
       // public List<RaCalculatedDates> RaCalculatedDatesList { get; set; }
    }
}
