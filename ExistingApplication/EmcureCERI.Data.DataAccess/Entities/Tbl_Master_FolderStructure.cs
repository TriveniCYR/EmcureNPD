using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Master_FolderStructure
    {
        public int Id { get; set; }
        public int DossierTemplateID { get; set; }
        public int CountryID { get; set; }
        public string FolderName { get; set; }
        public int ParentFolderID { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string FolderPath { get; set; }
    }
}
