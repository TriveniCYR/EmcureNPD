using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class SMTPDetailsModel
    {
        public int Id { get; set; }
        public string AliasName { get; set; }
        public string HostName { get; set; }
        public string FromMail { get; set; }
        public string FromPassword { get; set; }
        public bool IsEnableSSL { get; set; }
        public int PortNumber { get; set; }
        public bool IsMailStatus { get; set; }
        public bool IsDefaultCredentials { get; set; }
        public bool IsWithoutPassword { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
