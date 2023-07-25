using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PidfPbfGeneralRndEntity
    {
        public long PbfRndDetailsId { get; set; }
        public long PidfId { get; set; }
        public long PbfId { get; set; }
        public string RndResponsiblePerson { get; set; }
        public DateTime? TypeOfDevelopmentDate { get; set; }
        public DateTime? PivotalBatchesManufacturedCompleted { get; set; }
        public DateTime? StabilityResultsDayZero { get; set; }
        public DateTime? StabilityResultsThreeMonth { get; set; }
        public DateTime? StabilityResultsSixMonth { get; set; }
        public bool? NonStandardProduct { get; set; }
        public string Pivotals { get; set; }
        public string BatchSizes { get; set; }
        public long? NoMofBatchesPerStrength { get; set; }
        public DateTime?SiteTransferDate { get; set; }
        public DateTime?ApiOrderedDate { get; set; }
        public DateTime?ApiReceivedDate { get; set; }
        public DateTime?FinalFormulationApproved { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? CreatedBy { get; set; }
    }
   
}
