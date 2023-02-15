using EmcureCERI.Data.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmcureCERI.Business.Contract.ServiceContracts
{
    public interface IDRFManufacturing
    {
        int insertDRFManufacturing(Tbl_DRF_Manufacturing entity);
        int updateDRFManufacturing(DRFManufHeaderAndDetails entity);

        IList<Tbl_DRF_Manufacturing> GetManufacturingFilledDetails(int InitializationID);
        int insertDRFManufacturingAPISite(List<Tbl_DRF_Manufacturing_APISite> entity);
        IList<Tbl_DRF_Manufacturing_APISite> GetManufacturingAPIListDetails(int InitializationID);
    }
}
