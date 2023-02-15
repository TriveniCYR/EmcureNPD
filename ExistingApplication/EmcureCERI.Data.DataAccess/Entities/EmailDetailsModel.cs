using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class EmailDetailsModel
    {
        public int Id { get; set; }
        public int NotificationID { get; set; }
        public string ToList { get; set; }
        public string CCList { get; set; }
        public string BCCList { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
