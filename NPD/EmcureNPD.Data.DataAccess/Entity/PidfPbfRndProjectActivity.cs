using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfPbfRndProjectActivity
    {
        public PidfPbfRndProjectActivity()
        {
            PidfPbfRndProjectEstimations = new HashSet<PidfPbfRndProjectEstimation>();
        }

        public int ProjectActivitiesId { get; set; }
        public string ProjectActivityName { get; set; }

        public virtual ICollection<PidfPbfRndProjectEstimation> PidfPbfRndProjectEstimations { get; set; }
    }
}
