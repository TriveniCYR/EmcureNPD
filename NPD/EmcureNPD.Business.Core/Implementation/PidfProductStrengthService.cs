using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class PidfProductStrengthService : IPidfProductStrengthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IExceptionService _ExceptionService;
        private IRepository<PidfproductStrength> _repository { get; set; }

        public PidfProductStrengthService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IExceptionService exceptionService)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<PidfproductStrength>();
            _ExceptionService = exceptionService;
        }

        public async Task<List<PidfProductStregthEntity>> GetAll()
        {
            return _mapperFactory.GetList<PidfproductStrength, PidfProductStregthEntity>(await _repository.GetAllAsync());
        }

        public async Task<PidfProductStregthEntity> GetById(int id)
        {
            return _mapperFactory.Get<PidfproductStrength, PidfProductStregthEntity>(await _repository.GetAsync(id));
        }

        public async Task<List<PidfProductStregthEntity>> GetStrengthByPIDFId(long pidfid)
        {
            var objpidf = await _repository.GetAllAsync();
            var objpidfId = objpidf.Where(x => x.Pidfid == pidfid).ToList();
            return _mapperFactory.GetList<PidfproductStrength, PidfProductStregthEntity>(objpidfId);
        }
        public async Task<dynamic> GetStrengthByPIDFAnddBuId(long pidfid,long buid)
        {
            try
            {
                SqlParameter[] osqlParameter = {
                new SqlParameter("@PIDFId", pidfid),
                new SqlParameter("@Buid", buid)
            };

                DataSet dsStrength = await _repository.GetDataSetBySP("ProcGetStrengthByPIDFAnddBuId", System.Data.CommandType.StoredProcedure, osqlParameter);
                return dsStrength;
            }
            catch (Exception ex)
            {
                await _ExceptionService.LogException(ex);
                return null;
            }
        }
    }
}