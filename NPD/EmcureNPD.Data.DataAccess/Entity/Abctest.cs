using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class Abctest
    {
        public int? CurrentPageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string SearchText { get; set; }
    }
}
