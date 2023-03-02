using System;

namespace EmcureNPD.Web.Models {
    public class SendReminderModel {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PIDFNO { get; set; }
        public string MoleculeName { get; set; }
        public DateTime? RejectedDateTime { get; set; }
    }
}
