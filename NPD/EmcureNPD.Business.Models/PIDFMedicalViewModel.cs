using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class PIDFMedicalViewModel
    {

        public long PidfmedicalId { get; set; }
        public long Pidfid { get; set; }
        [Required]
        public int MedicalOpinion { get; set; }
        [Required]
        public string Remark { get; set; }
        public DateTime CreatedDate { get; set; }

        public long PidfmedicalFileId { get; set; }
        public string[] FileName { get; set; }
        public int CreatedBy { get; set; }

        [NotMapped]
        public List<IFormFile> File { get; set; }

        public string FileAllowedExtension { get; set; }
        public int FileSize { get; set; }
        public string ErrorMessage { get; set; }

    }
}
