using System;
using System.Collections.Generic;

#nullable disable

namespace EmcureNPD.Data.DataAccess.Entity
{
    public partial class PidfFinanceBatchSizeCoating
    {
        public int PidffinaceBatchSizeCoatingId { get; set; }
        public int PidffinaceId { get; set; }
        public int? BusinessUnitId { get; set; }
        public double? Batchsize { get; set; }
        public double? Yield { get; set; }
        public double? Batchoutput { get; set; }
        public double? ApiCad { get; set; }
        public double? ExcipientsCad { get; set; }
        public double? PmCad { get; set; }
        public double? CcpcCad { get; set; }
        public double? FreightCad { get; set; }
        public double? EmcureCogsPack { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? Skus { get; set; }
        public double? PakeSize { get; set; }
        public double? BrandPrice { get; set; }
        public double? GenericListprice { get; set; }
        public double? EstMat2016By12units { get; set; }
        public double? EstMat2020By12units { get; set; }
        public double? Cagrover2016By12estMatunits { get; set; }
        public double? Marketinpacks { get; set; }
        public double? BatchsizeinLtrTabs { get; set; }
        public double? NetRealisation { get; set; }

        public virtual PidfFinance Pidffinace { get; set; }
    }
}
