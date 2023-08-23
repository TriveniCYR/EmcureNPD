using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class MasterTypeOfSubmissionEntity
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
