using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Resource;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation
{
    public class ExceptionService : IExceptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private IRepository<MasterException> _repository { get; set; }
        private readonly IHelper _helper;

        public ExceptionService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IStringLocalizer<Errors> stringLocalizerError,
                                 Microsoft.Extensions.Configuration.IConfiguration _configuration, IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterException>();
            configuration = _configuration;
            _helper = helper;
        }

        public async Task<DBOperation> LogException(Exception exception)
        {
            try
            {

                SqlParameter[] osqlParameter = {
                new SqlParameter("@Message", exception.Message),
                new SqlParameter("@Source", exception.Source),
                new SqlParameter("@InnerException", Convert.ToString(exception.InnerException)),
                new SqlParameter("@StackTrace", Convert.ToString(exception.StackTrace)),
                new SqlParameter("@CreatedBy", _helper.GetLoggedInUser().UserId)
            };
                DataTable dtOptions = await _repository.GetBySP("stp_npd_InsertException", System.Data.CommandType.StoredProcedure, osqlParameter);

                var result = Convert.ToBoolean(dtOptions.Rows[0][0]);
                if (result)
                    return DBOperation.Success;
                return DBOperation.Error;
            }
            catch (Exception ex)
            {
                return DBOperation.Error;
            }
        }
    }
}