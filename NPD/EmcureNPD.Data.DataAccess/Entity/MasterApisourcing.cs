using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterApisourcing
    {
        public MasterApisourcing()
        {
            Pidfapidetails = new HashSet<Pidfapidetail>();
        }

        public int ApisourcingId { get; set; }
        public string ApisourcingName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public virtual ICollection<Pidfapidetail> Pidfapidetails { get; set; }
    }
}
