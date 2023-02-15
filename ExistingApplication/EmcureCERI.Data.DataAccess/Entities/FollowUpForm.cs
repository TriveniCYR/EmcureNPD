using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class FollowUpForm
    {
        public FollowUpForm()
        {
            PatientFollowUpForm = new HashSet<PatientFollowUpForm>();
        }

        public int Id { get; set; }
        public int? Fufquest1 { get; set; }
        public int? Fufquest2 { get; set; }
        public int? Fufquest3 { get; set; }
        public int? Fufquest4 { get; set; }
        public int? Fufquest5 { get; set; }
        public int? Fufquest6 { get; set; }
        public int? Fufquest7 { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }

        public Fufquestionnaire1 Fufquest1Navigation { get; set; }
        public Fufquestionnaire2 Fufquest2Navigation { get; set; }
        public Fufquestionnaire3 Fufquest3Navigation { get; set; }
        public Fufquestionnaire4 Fufquest4Navigation { get; set; }
        public Fufquestionnaire5 Fufquest5Navigation { get; set; }
        public Fufquestionnaire6 Fufquest6Navigation { get; set; }
        public Fufquestionnaire7 Fufquest7Navigation { get; set; }
        public ICollection<PatientFollowUpForm> PatientFollowUpForm { get; set; }
    }
}
