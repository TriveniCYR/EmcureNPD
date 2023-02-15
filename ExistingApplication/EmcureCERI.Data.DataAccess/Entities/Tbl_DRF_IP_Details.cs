using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
   public class Tbl_DRF_IP_Details
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Markets { get; set; }
        public string NumbersOfApprovedANDA { get; set; }
        public string PatentStatus { get; set; }
    public string LegalStatus { get; set; }
    public string IPDComments { get; set; }
    public string NumbersOfApprovedGeneric { get; set; }
    public string TypeOfFiling { get; set; }
    public string CostofLitigation { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int InitializationId { get; set; }
    }
}
