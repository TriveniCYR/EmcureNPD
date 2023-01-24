using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterDium
    {
        public MasterDium()
        {
            Pidfs = new HashSet<Pidf>();
        }

        public int Diaid { get; set; }
        public string Dianame { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<Pidf> Pidfs { get; set; }
    }
}
