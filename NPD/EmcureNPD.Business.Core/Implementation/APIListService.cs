using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class APIListService : IAPIListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Pidf> _repository { get; set; }

        //Market Extension & In House

        public APIListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Pidf>();
        }

        public async Task<DataTableResponseModel> GetAllAPIList(DataTableAjaxPostModel model)
        {
            string ColumnName = (model.order.Count > 0 ? model.columns[model.order[0].column].data : string.Empty);
            string SortDir = (model.order.Count > 0 ? model.order[0].dir : string.Empty);

            SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFID", 0),
                new SqlParameter("@CurrentPageNumber", model.start),
                    new SqlParameter("@PageSize", model.length),
                    new SqlParameter("@SortColumn", ColumnName),
                    new SqlParameter("@SortDirection", SortDir),
                    new SqlParameter("@SearchText", model.search.value)
            };

            var APIListList = await _repository.GetBySP("stp_npd_GetAPIList", System.Data.CommandType.StoredProcedure, osqlParameter);

            var TotalRecord = (APIListList != null && APIListList.Rows.Count > 0 ? Convert.ToInt32(APIListList.Rows[0]["TotalRecord"]) : 0);
            var TotalCount = (APIListList != null && APIListList.Rows.Count > 0 ? Convert.ToInt32(APIListList.Rows[0]["TotalCount"]) : 0);

            DataTableResponseModel oDataTableResponseModel = new DataTableResponseModel(model.draw, TotalRecord, TotalCount, APIListList.DataTableToList<APIListEntity>());

            return oDataTableResponseModel;
        }
    }
}
