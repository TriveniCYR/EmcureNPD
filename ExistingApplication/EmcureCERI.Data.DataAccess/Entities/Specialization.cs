using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class Specialization
    {
        public int Id { get; set; }
        public string NameGb { get; set; }
        public string NameDe { get; set; }
        public string NameEs { get; set; }
        public string NameFr { get; set; }
        public string NameBe { get; set; }
        public bool IsEnabled { get; set; }
    }
}
