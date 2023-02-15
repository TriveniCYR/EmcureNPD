using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Models
{
    public class common
    {
        public int StatusID { get; set; }
        public int InitializationID { get; set; }
        public int CountryID { get; set; }
        //public List<clsCountry> CountryList { get; set; }
        public List<clsMolecule> MoleculeList { get; set; }
        public List<clsStatus> StatusList { get; set; }
    }
    public class IndexPageModel
    {
        public common CommonVModel = new common();


    }
    public class clsMolecule
    {
        public int InitializationID { get; set; }
        public string GenericName { get; set; }
    }


    public class clsStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }

    public class CheckList
    {
        public int DRFID { get; set; }
        public string CheckedIDList { get; set; }
    }

    
}
