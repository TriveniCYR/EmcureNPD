using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Resource;
using EmcureNPD.Utility.Audit;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Helpers;
using EmcureNPD.Utility.Utility;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Implementation {
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
                MasterException objException = new MasterException
                {
                    Message = exception.Message,
                    Source = exception.Source,
                    InnerException = Convert.ToString(exception.InnerException),
                    StrackTrace = Convert.ToString(exception.StackTrace),
                    CreatedDate = DateTime.Now,
                    CreatedBy = _helper.GetLoggedInUser().UserId
                };
               
                _repository.AddAsync(objException);

                await _unitOfWork.SaveChangesAsync();
                if (objException.ExceptionId == 0)
                    return DBOperation.Error;
                return DBOperation.Success;
            }
            catch (Exception ex)
            {
                return DBOperation.Error;
            }            
        }
    }
}
