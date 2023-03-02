using System;

namespace EmcureNPD.Web.Models {
    public class EmailModel {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PIDFNumber { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
