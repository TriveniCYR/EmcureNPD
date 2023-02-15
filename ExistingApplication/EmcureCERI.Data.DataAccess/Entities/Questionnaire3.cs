using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Questionnaire3
    {
        public Questionnaire3()
        {
            BaselineDataMaster = new HashSet<BaselineDataMaster>();
        }

        public int Id { get; set; }
        public string Heading { get; set; }
        public string Indication { get; set; }
        public string Dose { get; set; }
        public string Frequency { get; set; }
        public string Route { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public bool IsFulFill { get; set; }

        public ICollection<BaselineDataMaster> BaselineDataMaster { get; set; }
    }
}
