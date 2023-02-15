using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class PIDFHeaderAndDetails: Tbl_PIDF_Header
    {
        public IList<Tbl_PIDF_CountryDetails> objPIDF_CountryDetails { get; set; }

        public IList<UploadedFileModel> uploadedfilesdetails { get; set; }
    }   
}
