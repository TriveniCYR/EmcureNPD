using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public  class MasterNationApprovalEntity
    {
        public int NationApprovalId { get; set; }
        public int? MinEOP { get; set; }
        public int? MaxEOP { get; set; }
        public List<CountryDetails> CountryDetails { get; set; }

    }

    public class CountryDetails
    {
        public int CountryId { get; set; }  
        public string CountryName { get; set; } 
    }
}
