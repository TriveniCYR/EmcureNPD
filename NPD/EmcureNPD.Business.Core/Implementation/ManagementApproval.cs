using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class ManagementApproval : IManagementApproval
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<ProjectsModel> _repository { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IMasterAuditLogService _auditLogService;
        private readonly IExceptionService _ExceptionService;
        private IRepository<PidfPbfRnDMaster> _pidfPbfRnDMasterRepository { get; set; }
        public ManagementApproval(IUnitOfWork unitOfWork, IConfiguration configuration, IMasterAuditLogService auditLogService, IExceptionService exceptionService)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<ProjectsModel>();
            _configuration = configuration;
            _ExceptionService = exceptionService;
        }

        public async Task<dynamic> GetProjectNameAndStrength(int Pidfid = 0, int Buid = 0)
        {
            try
            {
                SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFID",Pidfid),
                new SqlParameter("@BUID",Buid)
            };

                DataSet dsPidfFinance = await _repository.GetDataSetBySP("ProcGetProjectNameAndStrength", System.Data.CommandType.StoredProcedure, osqlParameter);
                return dsPidfFinance;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return null;
            }
        }

       
    }
}