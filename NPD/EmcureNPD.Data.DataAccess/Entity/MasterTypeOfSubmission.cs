using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterTypeOfSubmission
    {
        public int Id { get; set; }
        public string TypeOfSubmission { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? MinEOP { get; set; }
        public int? MaxEOP { get; set; }
    }
}
