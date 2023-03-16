using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PIDFPBFClinicalFormEntity
    {
        public long Pidfpbfid { get; set; }
        public long Pidfid { get; set; }
        public int BusinessUnitId { get; set; }
        public long? StrengthId { get; set; }
        public int? StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ModifyBy { get; set; }
        public string SaveSubmitType { get; set; }
        public string BusinessUnitsByUser { get; set; }
        public PBFFormEntity PbfFormEntities { get; set; }
        public List<MasterBusinessUnitEntity> MasterBusinessUnitEntities { get; set; }
        public List<PidfProductStregthEntity> MasterStrengthEntities { get; set; }
        public PidfPbfClinicalEntity PidfPbfClinicals { get; set; }
    }
}
