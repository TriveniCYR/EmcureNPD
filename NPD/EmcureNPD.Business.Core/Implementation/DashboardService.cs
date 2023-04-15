using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using EmcureNPD.Utility.Utility;
using System.Data.SqlClient;
using System.Dynamic;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IMasterBusinessUnitService _businessUnitService;
        private readonly IPIDFService _pidfService;
        private IRepository<Pidf> _repository { get; set; }
        private readonly IHelper _helper;

        public DashboardService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IMasterBusinessUnitService businessUnitService, IPIDFService pidfService, IHelper helper)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _businessUnitService = businessUnitService;
            _pidfService = pidfService;
            _repository = _unitOfWork.GetRepository<Pidf>();
            _helper = helper;
        }

        public async Task<dynamic> FillDropdown()
        {
            dynamic DropdownObjects = new ExpandoObject();
            DropdownObjects.MasterBusinessUnits = await _pidfService.GetBusinessUNitByUserId(_helper.GetLoggedInUser().UserId);
            return DropdownObjects;
        }

        public async Task<dynamic> GetPIDFByYearAndBusinessUnit(int id, string fromDate, string toDate)
        {
            dynamic DropdownObjects = new ExpandoObject();
            //var splitYear = years.Split('-');
            string FinancialYearStart = fromDate;
            string FinancialYearEnd = toDate;
            SqlParameter[] osqlParameter = {
                new SqlParameter("@BusinessUnitId",id),
                new SqlParameter("@FinancialYearStart",FinancialYearStart),
                new SqlParameter("@FinancialYearEnd",FinancialYearEnd),
                new SqlParameter("@UserId",_helper.GetLoggedInUser().UserId),
            };

            var PIDFList = await _repository.GetBySP("GetPIDFByYearAndBusinessUnit", System.Data.CommandType.StoredProcedure, osqlParameter);
            DropdownObjects.PIDFList = PIDFList.DataTableToList<PIDFStatusCountModel>();
            return DropdownObjects;
        }
    }
}