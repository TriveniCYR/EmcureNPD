using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class PidfApiDetailsService : IPidfApiDetailsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private IRepository<Pidfapidetail> _repository { get; set; }

        public PidfApiDetailsService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<Pidfapidetail>();
        }

        public async Task<List<PidfApiDetailEntity>> GetAll()
        {
            return _mapperFactory.GetList<Pidfapidetail, PidfApiDetailEntity>(await _repository.GetAllAsync());
        }

        public async Task<PidfApiDetailEntity> GetById(int id)
        {
            return _mapperFactory.Get<Pidfapidetail, PidfApiDetailEntity>(await _repository.GetAsync(id));
        }
    }
}
