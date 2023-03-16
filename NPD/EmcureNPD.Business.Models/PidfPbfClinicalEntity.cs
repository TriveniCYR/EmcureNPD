using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PidfPbfClinicalEntity
    {
        public long PBFGeneralID { get; set; }        
        public long PIDFID { get; set; }
        public int BusinessUnitId { get; set; }
        public int StrengthId { get; set; }
        public List<PidfPbfClinicalPilotBioFastingEntity> pidfpbfClinicalpilotBioFastingEntity { get; set; }
        public List<PidfPbfClinicalPilotBioFedEntity> pidfpbfClinicalPilotBioFedEntity { get; set; }
        public List<PidfPbfClinicalPivotalBioFastingEntity> pidfpbfClinicalPivotalBioFastingEntity { get; set; }
        public List<PidfPbfClinicalPivotalBioFedEntity> pidfpbfClinicalPivotalBioFedEntity { get; set; }
        public PidfPbfClinicalCostEntity pidfPbfClinicalCost { get; set; }       
    }
}
