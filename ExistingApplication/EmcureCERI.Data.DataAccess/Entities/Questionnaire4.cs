using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Questionnaire4
    {
        public Questionnaire4()
        {
            BaselineDataMaster = new HashSet<BaselineDataMaster>();
        }

        public int Id { get; set; }
        public string Heading { get; set; }
        public string Statement { get; set; }
        public bool ConfirmBdf { get; set; }
        public DateTime? Doa { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public bool IsFulFill { get; set; }

        public ICollection<BaselineDataMaster> BaselineDataMaster { get; set; }
    }
}
