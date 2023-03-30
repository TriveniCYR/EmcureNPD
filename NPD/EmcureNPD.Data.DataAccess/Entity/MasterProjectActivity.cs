using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class MasterProjectActivity
    {
        public MasterProjectActivity()
        {
            PidfPbfRnDManPowerCosts = new HashSet<PidfPbfRnDManPowerCost>();
        }

        public int ProjectActivitiesId { get; set; }
        public string ProjectActivitiesName { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<PidfPbfRnDManPowerCost> PidfPbfRnDManPowerCosts { get; set; }
    }
}
