using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Models.DataModel
{
    public class PrescriverObject
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class AdminObject
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class PatientObject
    {
        public string FullName { get; set; }
    }

    public class PatientPrescriverObject
    {
        public string PatientFullName { get; set; }
        public string PrescriberFullName { get; set; }
        public string PrescriberEmail { get; set; }
    }

}
