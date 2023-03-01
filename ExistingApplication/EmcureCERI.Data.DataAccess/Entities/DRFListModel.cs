using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class DRFListModel
    {
        public int SrNo { get; set; }
        public int InitializationID { get; set; }
        public string DRFNo { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string GenericName { get; set; }        
        public string Pharmaceutical_Form { get; set; }
        public string Strength_Name { get; set; }                
        public string Status { get; set; }
        public string IsActive { get; set; }
    }
}
