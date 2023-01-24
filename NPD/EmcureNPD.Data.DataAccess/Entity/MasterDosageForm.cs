using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterDosageForm
    {
        public MasterDosageForm()
        {
            Pidfs = new HashSet<Pidf>();
        }

        public int DosageFormId { get; set; }
        public string DosageFormName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<Pidf> Pidfs { get; set; }
    }
}
