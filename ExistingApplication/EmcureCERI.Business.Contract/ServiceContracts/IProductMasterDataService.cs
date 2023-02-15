using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IProductMasterDataService
    {
        IList<Tbl_Master_ProductData> GetAllProductMaster(Int64 ProductID, string strAction);
        Tbl_Master_ProductData GetProductMasterDetails(Tbl_Master_ProductData tbl_Master_ProductData);
        Tbl_Master_ProductData CheckProductMasterDetails(Tbl_Master_ProductData tbl_Master_ProductData);
        int InsertProductMaster(Tbl_Master_ProductData tbl_Master_ProductData);
        int UpdateProductMaster(Tbl_Master_ProductData tbl_Master_ProductData);
        int DeleteProductMaster(Int64 ProductID);
    }
}
