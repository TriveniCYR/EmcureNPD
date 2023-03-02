using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models {
    public class AutoUpdatePIDFStatusModel {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PIDFNO { get; set; }
        public string MoleculeName { get; set; }
        public string PIDFStatus { get; set; }
    }
}
