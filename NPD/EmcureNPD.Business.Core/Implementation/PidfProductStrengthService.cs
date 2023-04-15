using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class PidfProductStrengthService : IPidfProductStrengthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<PidfproductStrength> _repository { get; set; }

        public PidfProductStrengthService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<PidfproductStrength>();
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
    }
}