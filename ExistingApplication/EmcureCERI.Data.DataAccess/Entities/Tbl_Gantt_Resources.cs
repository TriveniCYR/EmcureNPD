using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Gantt_Resources
    {
        public int Id { get; set; }       
        public string Name { get; set; }        
        public int? ParentId { get; set; }
    }
}
