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
        public int? Batchsize { get; set; }
        public int? Yield { get; set; }
        public int? Batchoutput { get; set; }
        public int? ApiCad { get; set; }
        public int? ExcipientsCad { get; set; }
        public int? PmCad { get; set; }
        public int? CcpcCad { get; set; }
        public int? FreightCad { get; set; }
        public int? EmcureCogsPack { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public long? Skus { get; set; }
        public int? PakeSize { get; set; }
        public double? BrandPrice { get; set; }
        public double? GenericListprice { get; set; }
        public double? EstMat2016By12units { get; set; }
        public double? EstMat2020By12units { get; set; }
        public double? Cagrover2016By12estMatunits { get; set; }
        public double? Marketinpacks { get; set; }
        public double? BatchsizeinLtrTabs { get; set; }

        public virtual PidfFinance Pidffinace { get; set; }
    }
}
