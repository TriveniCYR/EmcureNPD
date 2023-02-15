using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IUploadService
    {
        int TruncateProductMasterBulkData(string typeData);        
        bool ProductMasterDataDataUpload(DataSet productDataSet, int UserID);
    }
}
