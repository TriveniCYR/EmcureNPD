using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_PIDF_UploadFileDetails
    {
        [Key]
        public int ID { get; set; }
        public int PIDFID { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }

    }
}
