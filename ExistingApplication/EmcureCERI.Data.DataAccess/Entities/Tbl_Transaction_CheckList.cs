using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Transaction_CheckList
    {
       [Key]
        public int TransactionID { get; set; }
        public int DRFID { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
