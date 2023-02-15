﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Master_Incoterms
    {
        public int Id { get; set; }
        public string Incoterms { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public Nullable<DateTime> ModifyDate { get; set; }
    }
}
