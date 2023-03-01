using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Fufquestionnaire6
    {
        public Fufquestionnaire6()
        {
            FollowUpFormMaster = new HashSet<FollowUpFormMaster>();
        }

        public int Id { get; set; }
        public string Heading { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public bool IsFulFill { get; set; }
        public bool? Medications { get; set; }
        public string M1medication { get; set; }
        public string M1reason { get; set; }
        public string M1indication { get; set; }
        public string M1dosageUnits { get; set; }
        public string M1frequency { get; set; }
        public string M1route { get; set; }
        public DateTime? M1startDate { get; set; }
        public DateTime? M1stopDate { get; set; }
        public bool? M1ongoing { get; set; }
        public string M2medication { get; set; }
        public string M2reason { get; set; }
        public string M2indication { get; set; }
        public string M2dosageUnits { get; set; }
        public string M2frequency { get; set; }
        public string M2route { get; set; }
        public DateTime? M2startDate { get; set; }
        public DateTime? M2stopDate { get; set; }
        public bool? M2ongoing { get; set; }
        public string M3medication { get; set; }
        public string M3reason { get; set; }
        public string M3indication { get; set; }
        public string M3dosageUnits { get; set; }
        public string M3frequency { get; set; }
        public string M3route { get; set; }
        public DateTime? M3startDate { get; set; }
        public DateTime? M3stopDate { get; set; }
        public bool? M3ongoing { get; set; }
        public string M4medication { get; set; }
        public string M4reason { get; set; }
        public string M4indication { get; set; }
        public string M4dosageUnits { get; set; }
        public string M4frequency { get; set; }
        public string M4route { get; set; }
        public DateTime? M4startDate { get; set; }
        public DateTime? M4stopDate { get; set; }
        public bool? M4ongoing { get; set; }
        public string M5medication { get; set; }
        public string M5reason { get; set; }
        public string M5indication { get; set; }
        public string M5dosageUnits { get; set; }
        public string M5frequency { get; set; }
        public string M5route { get; set; }
        public DateTime? M5startDate { get; set; }
        public DateTime? M5stopDate { get; set; }
        public bool? M5ongoing { get; set; }
        public string M6medication { get; set; }
        public string M6reason { get; set; }
        public string M6indication { get; set; }
        public string M6dosageUnits { get; set; }
        public string M6frequency { get; set; }
        public string M6route { get; set; }
        public DateTime? M6startDate { get; set; }
        public DateTime? M6stopDate { get; set; }
        public bool? M6ongoing { get; set; }
        public string M7medication { get; set; }
        public string M7reason { get; set; }
        public string M7indication { get; set; }
        public string M7dosageUnits { get; set; }
        public string M7frequency { get; set; }
        public string M7route { get; set; }
        public DateTime? M7startDate { get; set; }
        public DateTime? M7stopDate { get; set; }
        public bool? M7ongoing { get; set; }
        public string M8medication { get; set; }
        public string M8reason { get; set; }
        public string M8indication { get; set; }
        public string M8dosageUnits { get; set; }
        public string M8frequency { get; set; }
        public string M8route { get; set; }
        public DateTime? M8startDate { get; set; }
        public DateTime? M8stopDate { get; set; }
        public bool? M8ongoing { get; set; }
        public string M9medication { get; set; }
        public string M9reason { get; set; }
        public string M9indication { get; set; }
        public string M9dosageUnits { get; set; }
        public string M9frequency { get; set; }
        public string M9route { get; set; }
        public DateTime? M9startDate { get; set; }
        public DateTime? M9stopDate { get; set; }
        public bool? M9ongoing { get; set; }
        public string M10medication { get; set; }
        public string M10reason { get; set; }
        public string M10indication { get; set; }
        public string M10dosageUnits { get; set; }
        public string M10frequency { get; set; }
        public string M10route { get; set; }
        public DateTime? M10startDate { get; set; }
        public DateTime? M10stopDate { get; set; }
        public bool? M10ongoing { get; set; }
        public string Comments { get; set; }
        public DateTime? Doa { get; set; }

        public ICollection<FollowUpFormMaster> FollowUpFormMaster { get; set; }
    }
}
