using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Master_SubTask
    {
        [Key]
        public int SubTaskID { get; set; }        
        public int? TaskID { get; set; }     
        public string TaskName { get; set; }
        public string SubTaskName { get; set; }        
        public bool IsActive { get; set; }        
        public int? CreatedBy { get; set; }        
        public DateTime? CreatedDate { get; set; }        
        public int? ModifiedBy { get; set; }         
        public DateTime? ModifiedDate { get; set; }
    }
}
