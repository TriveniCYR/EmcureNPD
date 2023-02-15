using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class CountryDailingCode
    {
        public int id { get; set; }
        public string iso { get; set; }
        public string name { get; set; }
        public string nicename { get; set; }
        public string iso3 { get; set; }
        public int numcode { get; set; }
        public int phonecode { get; set; }
    }
}
