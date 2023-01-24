using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterOral
    {
        public MasterOral()
        {
            Pidfs = new HashSet<Pidf>();
        }

        public int OralId { get; set; }
        public string OralName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<Pidf> Pidfs { get; set; }
    }
}
