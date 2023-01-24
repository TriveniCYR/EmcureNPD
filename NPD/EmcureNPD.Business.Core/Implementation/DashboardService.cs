using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class DashboardService : IDashboardService
    {             
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IMasterOralService _oralService;
        private readonly IMasterUnitofMeasurementService _unitofMeasurementService;
        private readonly IMasterDosageFormService _dosageFormService;
        private readonly IMasterPackagingTypeService _packagingTypeService;
        private readonly IMasterBusinessUnitService _businessUnitService;
        private readonly IMasterCountryService _countryService;
        private readonly IMasterAPISourcingService _APISourcingService;
        private readonly IPidfApiDetailsService _PidfApiDetailsService;
        private readonly IPidfProductStrengthService _pidfProductStrengthService;
        private readonly IMasterAuditLogService _auditLogService;

        private IRepository<Pidf> _repository { get; set; }

        protected readonly DbContext dbContext;
        protected readonly DbSet<MasterBusinessUnitEntity> dbSet;
        private IRepository<Pidfapidetail> _pidfApiRepository { get; set; }
        private IRepository<PidfproductStrength> _pidfProductStrength { get; set; }
        //Market Extension & In House

        public DashboardService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterOralService oralService, IMasterUnitofMeasurementService unitofMeasurementService, IMasterDosageFormService dosageFormService, IMasterPackagingTypeService packagingTypeService, IMasterBusinessUnitService businessUnitService, IMasterCountryService countryService, IMasterAPISourcingService masterAPISourcingService, IPidfApiDetailsService pidfApiDetailsService, IPidfProductStrengthService pidfProductStrengthService, IMasterAuditLogService auditLogService, DbContext Context)
        {
            _unitOfWork = unitOfWork;
            dbContext = Context ?? throw new ArgumentNullException(nameof(Context));
            dbSet = dbContext.Set<MasterBusinessUnitEntity>();
            _mapperFactory = mapperFactory;
            _oralService = oralService;
            _unitofMeasurementService = unitofMeasurementService;
            _dosageFormService = dosageFormService;
            _packagingTypeService = packagingTypeService;
            _businessUnitService = businessUnitService;
            _countryService = countryService;
            _APISourcingService = masterAPISourcingService;
            _PidfApiDetailsService = pidfApiDetailsService;
            _pidfProductStrengthService = pidfProductStrengthService;
            _repository = _unitOfWork.GetRepository<Pidf>();
            _pidfApiRepository = unitOfWork.GetRepository<Pidfapidetail>();
            _pidfProductStrength = unitOfWork.GetRepository<PidfproductStrength>();
            _auditLogService = auditLogService;
        }

        public async Task<dynamic> FillDropdown()
        {           
                dynamic DropdownObjects = new ExpandoObject();

                DropdownObjects.MasterBusinessUnits = _businessUnitService.GetAll().Result.Where(xx => xx.IsActive).ToList();

                return DropdownObjects;           
        }

        public async Task<dynamic> GetPIDFByYearAndBusinessUnit(int id,string years)
        {
            dynamic DropdownObjects = new ExpandoObject();
            var splitYear = years.Split('-');
            string FinancialYearStart = splitYear[0];
            string FinancialYearEnd = splitYear[1];

            SqlParameter[] osqlParameter = {
               
                new SqlParameter("@BusinessUnitId",id),
                new SqlParameter("@FinancialYearStart",FinancialYearStart),
                new SqlParameter("@FinancialYearEnd",FinancialYearEnd)
               
                   
                   
            };
           
            var PIDFList = await _repository.GetBySP("GetPIDFByYearAndBusinessUnit", System.Data.CommandType.StoredProcedure, osqlParameter);
            DropdownObjects.PIDFList = PIDFList.DataTableToList<PIDFStatusCountModel>();
            return DropdownObjects;
        }
    }
}
