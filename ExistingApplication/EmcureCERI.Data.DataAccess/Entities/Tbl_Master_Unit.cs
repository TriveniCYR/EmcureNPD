﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public class Tbl_Master_Unit
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
