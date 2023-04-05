﻿using EmcureNPD.Business.Core.Interface;
using EmcureNPD.Business.Core.ModelMapper;
using EmcureNPD.Business.Models;
using EmcureNPD.Data.DataAccess.Core.Repositories;
using EmcureNPD.Data.DataAccess.Core.UnitOfWork;
using EmcureNPD.Data.DataAccess.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;
using EmcureNPD.Utility.Utility;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Dynamic;
using Microsoft.Extensions.Localization;
using EmcureNPD.Resource;
using EmcureNPD.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc;
using EmcureNPD.Utility.Helpers;
using System.IO;
using System.IO.Pipes;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EmcureNPD.Business.Core.ServiceImplementations
{
    public class MasterUserService : IMasterUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperFactory _mapperFactory;
        private readonly IStringLocalizer<Errors> _stringLocalizerError;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private IRepository<MasterUser> _repository { get; set; }
        private IRepository<MasterUserBusinessUnitMapping> _masterUserBusinessUnitMappingrepository { get; set; }
        private IRepository<MasterUserRegionMapping> _masterUserRegionMappingrepository { get; set; }
        private IRepository<MasterUserCountryMapping> _masterUserCountryMappingrepository { get; set; }
        private IRepository<MasterUserDepartmentMapping> _masterUserDepartmentMappingrepository { get; set; }
        private IRepository<MasterBusinessUnit> _businessUnitRepository { get; set; }
        private readonly IMasterAuditLogService _auditLogService;

        public MasterUserService(IUnitOfWork unitOfWork, IMapperFactory mapperFactory, IStringLocalizer<Errors> stringLocalizerError,
                                 IMasterAuditLogService auditLogService,
                                 Microsoft.Extensions.Configuration.IConfiguration _configuration)
        {
            _unitOfWork = unitOfWork;
            _mapperFactory = mapperFactory;
            _repository = _unitOfWork.GetRepository<MasterUser>();
            _businessUnitRepository = _unitOfWork.GetRepository<MasterBusinessUnit>();
            _masterUserBusinessUnitMappingrepository = _unitOfWork.GetRepository<MasterUserBusinessUnitMapping>();
            _masterUserRegionMappingrepository = _unitOfWork.GetRepository<MasterUserRegionMapping>();
            _masterUserCountryMappingrepository = _unitOfWork.GetRepository<MasterUserCountryMapping>();
            _masterUserDepartmentMappingrepository = _unitOfWork.GetRepository<MasterUserDepartmentMapping>();
            configuration = _configuration;
            _auditLogService = auditLogService;
        }

        public async Task<UserSessionEntity> Login(LoginViewModel oLogin)
        {
            //UserSessionEntity oUser = new UserSessionEntity();

            //if (oLogin.Email.ToLower() == "admin@neosoft.com" && oLogin.Password == "12345")
            //{
            //    oUser.UserId = 1;
            //    oUser.Email = oLogin.Email;
            //    oUser.FullName = "NeoSOFT";
            //}
            //return oUser;

            UserSessionEntity oUser = null;

            SqlParameter[] osqlParameter = {              
                //new SqlParameter("@BusinessUnitId", oLogin.BusinessUnitId),
                new SqlParameter("@Email", oLogin.Email),
                new SqlParameter("@Password", Utility.Utility.UtilityHelper.GenerateSHA256String(oLogin.Password))
            };

            var UserList = await _repository.GetBySP("stp_npd_VerifyUserLogin", System.Data.CommandType.StoredProcedure, osqlParameter);

            if (UserList != null && UserList.Rows.Count > 0)
            {
                oUser = new UserSessionEntity();
                oUser.FullName = Convert.ToString(UserList.Rows[0]["FullName"]);
                oUser.UserId = Convert.ToInt32(UserList.Rows[0]["UserId"]);
                oUser.Email = Convert.ToString(UserList.Rows[0]["EmailAddress"]);
                oUser.RoleId = Convert.ToInt32(UserList.Rows[0]["RoleId"]);
                oUser.IsManagement = Convert.ToBoolean(UserList.Rows[0]["IsManagement"]);
                oUser.AssignedBusinessUnit = Convert.ToString(UserList.Rows[0]["AssignedBusinessUnit"]);
            }
            return oUser;
        }

        public async Task<DataTableResponseModel> GetAll(DataTableAjaxPostModel model)
        {
            string ColumnName = (model.order.Count > 0 ? model.columns[model.order[0].column].data : string.Empty);
            string SortDir = (model.order.Count > 0 ? model.order[0].dir : string.Empty);

            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", 0),
                new SqlParameter("@CurrentPageNumber", model.start),
                    new SqlParameter("@PageSize", model.length),
                    new SqlParameter("@SortColumn", ColumnName),
                    new SqlParameter("@SortDirection", SortDir),
                    new SqlParameter("@SearchText", model.search.value)
            };

            var UserList = await _repository.GetBySP("stp_npd_GetUserList", System.Data.CommandType.StoredProcedure, osqlParameter);

            var TotalRecord = (UserList != null && UserList.Rows.Count > 0 ? Convert.ToInt32(UserList.Rows[0]["TotalRecord"]) : 0);
            var TotalCount = (UserList != null && UserList.Rows.Count > 0 ? Convert.ToInt32(UserList.Rows[0]["TotalCount"]) : 0);

            DataTableResponseModel oDataTableResponseModel = new DataTableResponseModel(model.draw, TotalRecord, TotalCount, UserList.DataTableToList<MasterUserEntity>());

            return oDataTableResponseModel;
        }

        public async Task<dynamic> GetUserDropdown()
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", 0)
            };

            var UserDropdown = await _repository.GetDataSetBySP("stp_npd_GetUserDropdown", System.Data.CommandType.StoredProcedure, osqlParameter);

            dynamic DropdownObjects = new ExpandoObject();
            if (UserDropdown != null)
            {
                if (UserDropdown.Tables[0] != null && UserDropdown.Tables[0].Rows.Count > 0)
                {
                    DropdownObjects.BusinessUnit = UserDropdown.Tables[0].DataTableToList<MasterBusinessUnitEntity>();
                }
                if (UserDropdown.Tables[1] != null && UserDropdown.Tables[1].Rows.Count > 0)
                {
                    DropdownObjects.Role = UserDropdown.Tables[1].DataTableToList<MasterRoleEntity>();
                }
            }

            return DropdownObjects;
        }
        public async Task<dynamic> GetRegionByBusinessUnit(string BusinessUnitIds)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", 0),
                new SqlParameter("@BusinessUnitIds", BusinessUnitIds)
            };

            var UserRegionDropdown = await _repository.GetDataSetBySP("stp_npd_GetRegionByBusinessUnit", System.Data.CommandType.StoredProcedure, osqlParameter);

            dynamic DropdownObjects = new ExpandoObject();
            if (UserRegionDropdown != null)
            {
                if (UserRegionDropdown.Tables[0] != null && UserRegionDropdown.Tables[0].Rows.Count > 0)
                {
                    DropdownObjects.Region = UserRegionDropdown.Tables[0].DataTableToList<MasterRegion>();
                }
            }

            return DropdownObjects;

        }
        public async Task<dynamic> GetCountryByRegion(string RegionIds)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", 0),
                new SqlParameter("@RegionIds", RegionIds)
            };

            var UserCountryDropdown = await _repository.GetDataSetBySP("stp_npd_GetCountryByRegion", System.Data.CommandType.StoredProcedure, osqlParameter);

            dynamic DropdownObjects = new ExpandoObject();
            if (UserCountryDropdown != null)
            {
                if (UserCountryDropdown.Tables[0] != null && UserCountryDropdown.Tables[0].Rows.Count > 0)
                {
                    DropdownObjects.Country = UserCountryDropdown.Tables[0].DataTableToList<MasterCountry>();
                }
            }

            return DropdownObjects;

        }


        public async Task<dynamic> GetDepartmentCountryByBusinessUnit(int BusinessUnitId)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", 0),
                new SqlParameter("@BusinessUnitId", BusinessUnitId)
            };

            var UserDropdown = await _repository.GetDataSetBySP("stp_npd_GetDepartmentCountryByBusinessUnit", System.Data.CommandType.StoredProcedure, osqlParameter);

            dynamic DropdownObjects = new ExpandoObject();
            if (UserDropdown != null)
            {
                if (UserDropdown.Tables[0] != null && UserDropdown.Tables[0].Rows.Count > 0)
                {
                    DropdownObjects.Department = UserDropdown.Tables[0].DataTableToList<MasterDepartmentEntity>();
                }
                if (UserDropdown.Tables[1] != null && UserDropdown.Tables[1].Rows.Count > 0)
                {
                    DropdownObjects.Country = UserDropdown.Tables[1].DataTableToList<MasterCountryEntity>();
                }
            }

            return DropdownObjects;
        }

        public async Task<MasterUserEntity> GetById(int id)
        {
            var _userEntity = new MasterUserEntity();
            _userEntity = _mapperFactory.Get<MasterUser, MasterUserEntity>(await _repository.GetAsync(id));

            _userEntity.BusinessUnitIds = string.Join(',', _masterUserBusinessUnitMappingrepository.GetAll().Where(x => x.UserId == id).Select(x => x.BusinessUnitId));
            _userEntity.RegionIds = string.Join(',', _masterUserRegionMappingrepository.GetAll().Where(x => x.UserId == id).Select(x => x.RegionId));
            _userEntity.CountryIds = string.Join(',', _masterUserCountryMappingrepository.GetAll().Where(x => x.UserId == id).Select(x => x.CountryId));
            _userEntity.DepartmentIds = string.Join(',', _masterUserDepartmentMappingrepository.GetAll().Where(x => x.UserId == id).Select(x => x.DepartmentId));

            return _userEntity;
        }

        public async Task<DBOperation> AddUpdateUser(MasterUserEntity entityUser)
        {
            MasterUser objUser;
            var LoggedUserId = entityUser.LoggedUserId;
            if (entityUser.UserId > 0)
            {
                objUser = _repository.Get(entityUser.UserId);
                var OldObjUser = objUser;
                if (objUser != null)
                {
                    objUser.FullName = entityUser.FullName;
                    objUser.MobileNumber = entityUser.MobileNumber;
                    objUser.RoleId = entityUser.RoleId;
                    objUser.Address = entityUser.Address;
                    objUser.IsActive = entityUser.IsActive;
                    objUser.IsManagement = entityUser.IsManagement;
                    objUser.Apiuser  = entityUser.APIUser;
                    objUser.FormulationGl = entityUser.FormulationGL;
                    objUser.AnalyticalGl = entityUser.AnalyticalGL;
                    objUser.DesignationName = entityUser.DesignationName;
                    objUser.ModifyBy = LoggedUserId;
                    objUser.ModifyDate = DateTime.Now;

                    SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", entityUser.UserId)
            };
                    var result = await _repository.GetDataSetBySP("stp_npd_RemoveAllMultipleMappingofMasterUser", System.Data.CommandType.StoredProcedure, osqlParameter);
                     objUser = FillMappingData(entityUser, objUser);
                    _repository.UpdateAsync(objUser);
                  //  var isSuccess = await _auditLogService.CreateAuditLog<MasterUser>(Utility.Audit.AuditActionType.Update,
                  //Utility.Enums.ModuleEnum.UserManagement, OldObjUser, objUser, 0);

                }
                else
                {
                    return DBOperation.NotFound;
                }
            }
            else
            {
                objUser = _mapperFactory.Get<MasterUserEntity, MasterUser>(entityUser);
                objUser = FillMappingData(entityUser, objUser);
                objUser.CreatedBy = LoggedUserId;
                objUser.CreatedDate = DateTime.Now;
                _repository.AddAsync(objUser);
               // SendUserCreateMail(entityUser, LoggedUserId);
            }
            await _unitOfWork.SaveChangesAsync();

            if (objUser.UserId == 0)
                return DBOperation.Error;

            return DBOperation.Success;
        }
        public void SendUserCreateMail(MasterUserEntity entityUser, int LoggedUserId)
        {
            try
            {
                EmailHelper email = new EmailHelper();
                //need web URL  // string WebURL = configuration.GetSection("Apiconfig").GetSection("EmcureNPDWebUrl").Value;
                string strHtml = System.IO.File.ReadAllText(@"wwwroot\Uploads\HTMLTemplates\UserCreated.html");
                var LoggedInUserDetails = _repository.Get(LoggedUserId);
                if (LoggedInUserDetails != null)
                {
                    strHtml = strHtml.Replace("{CreatedByFullName}", LoggedInUserDetails.FullName);
                    strHtml = strHtml.Replace("{CreatedByEmail}", LoggedInUserDetails.EmailAddress);
                }
                strHtml = strHtml.Replace("{FullName}", entityUser.FullName);
                strHtml = strHtml.Replace("{Email}", entityUser.EmailAddress);
                strHtml = strHtml.Replace("{Password}", entityUser.StringPassword);
                // strHtml = strHtml.Replace("{ApplicationLoginURL}", WebURL);
                email.SendMail(entityUser.EmailAddress, string.Empty, "Emcure NPD - User Created", strHtml);
            }
            catch (Exception ex) { }
        }
        private MasterUser FillMappingData(MasterUserEntity entityUser, MasterUser objUser)
        {
            if (entityUser.BusinessUnitId != null)
            {
                foreach (var BUId in entityUser.BusinessUnitId)
                {
                    objUser.MasterUserBusinessUnitMappings.Add(new MasterUserBusinessUnitMapping
                    {
                        BusinessUnitId = BUId,
                        UserId = objUser.UserId,
                        CreatedDate = DateTime.Now,
                    });
                }
            }
            if (entityUser.RegionId != null)
            {
                foreach (var regionId in entityUser.RegionId)
                {
                    objUser.MasterUserRegionMappings.Add(new MasterUserRegionMapping
                    {
                        RegionId = regionId,
                        UserId = objUser.UserId,
                        CreatedDate = DateTime.Now,
                    });
                }
            }
            if (entityUser.DepartmentId != null)
            {
                foreach (var departmentId in entityUser.DepartmentId)
                {
                    objUser.MasterUserDepartmentMappings.Add(new MasterUserDepartmentMapping
                    {
                        DepartmentId = departmentId,
                        UserId = objUser.UserId,
                        CreatedDate = DateTime.Now,
                    });
                }
            }
            if (entityUser.CountryId != null)
            {
                foreach (var countryId in entityUser.CountryId)
                {
                    objUser.MasterUserCountryMappings.Add(new MasterUserCountryMapping
                    {
                        CountryId = countryId,
                        UserId = objUser.UserId,
                        CreatedDate = DateTime.Now,
                    });
                }
            }
            return objUser;
        }

        public async Task<DBOperation> DeleteUser(int id)
        {
            var entityUser = _repository.Get(x => x.UserId == id);

            if (entityUser == null)
                return DBOperation.NotFound;

            _repository.Remove(entityUser);

            await _unitOfWork.SaveChangesAsync();

            return DBOperation.Success;
        }
        public async Task<DBOperation> ChangeUserPassword(MasterUserChangePasswordEntity oUser)
        {
            var entityUser = _repository.Get(x => x.UserId == oUser.UserId);
            if (entityUser == null)
                return DBOperation.NotFound;

            //entityUser.Password = UtilityHelper.Decreypt(entityUser.Password);

            if (entityUser.Password == UtilityHelper.GenerateSHA256String(oUser.Oldpassword))
            {
                entityUser.Password = UtilityHelper.GenerateSHA256String(oUser.Password);
                _repository.UpdateAsync(entityUser);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                return DBOperation.Error;
            }
            return DBOperation.Success;
        }
        public async Task<bool> CheckEmailAddressExists(string emailAddress)
        {
            var isExists = await _repository.GetAllQuery().AnyAsync(x => x.EmailAddress.ToLower() == emailAddress.ToLower());
            return isExists;
        }
        public async Task<bool> IsTokenValid(string token)
        {
            var isExists = await _repository.GetAllQuery().AnyAsync(x => x.ForgotPasswordToken == token);
            return isExists;
        }
        //public Task<bool> IsTokenExpired(string emailAddress)
        //{
        //    var isExists = _repository.GetAllQuery().Where(x => x.EmailAddress.ToLower() == emailAddress.ToLower()).FirstOrDefault();
        //    if (isExists != null) return false;
        //    else return true;
        //}

        public async Task<List<MasterBusinessUnitEntity>> GetAll()
        {
            return _mapperFactory.GetList<MasterBusinessUnit, MasterBusinessUnitEntity>(await _businessUnitRepository.GetAllAsync());
        }

        public async Task<DBOperation> ForgotPassword(string emailAddress)
        {
            EmailHelper email = new EmailHelper();
            string baseURL = configuration.GetSection("Apiconfig").GetSection("EmcureNPDWebUrl").Value;
            var entityUser = _repository.Get(x => x.EmailAddress == emailAddress);
            if (entityUser == null)
                return DBOperation.NotFound;

            entityUser.ForgotPasswordToken = UtilityHelper.GenerateSHA256String(entityUser.UserId.ToString());
            entityUser.ForgotPasswordDateTime = DateTime.Now;
            _repository.UpdateAsync(entityUser);
            await _unitOfWork.SaveChangesAsync();

            string strURL = baseURL + @"/Account/ResetPassword?userToken=" + entityUser.ForgotPasswordToken;
            string strHtml = System.IO.File.ReadAllText(@"wwwroot\Uploads\HTMLTemplates\ForgotPassword.html");
            strHtml = strHtml.Replace("ValidateURL", strURL);
            strHtml = strHtml.Replace("ValidDateTime", entityUser.ForgotPasswordDateTime.Value.AddHours(1).ToString());
            strHtml = strHtml.Replace("Name", entityUser.FullName);
            email.SendMail(entityUser.EmailAddress, string.Empty, "Emcure NPD - Forgot Password", strHtml);
            return DBOperation.Success;
        }
        public async Task<string> ResetPassword(MasterUserResetPasswordEntity resetPasswordentity)
        {
            var entityUser = _repository.Get(x => x.ForgotPasswordToken == resetPasswordentity.ForgotPasswordToken);
            if (entityUser == null)
                return "TokenNotFound";

            if (entityUser.ForgotPasswordDateTime.Value.AddHours(1) < DateTime.Now)
            {
                return "TokenExpired";
            }
            entityUser.Password = UtilityHelper.GenerateSHA256String(resetPasswordentity.Password);
            entityUser.ForgotPasswordToken = string.Empty;
            entityUser.ForgotPasswordDateTime = null;
            _repository.UpdateAsync(entityUser);
            await _unitOfWork.SaveChangesAsync();
            return "ResetSuccessfully";
        }

        public async Task<List<MasterBusinessUnitEntity>> GetBusinessUNitByUserId(int userid)
        {
            SqlParameter[] osqlParameter = {
                new SqlParameter("@UserId", userid)
            };

            var dbresult = await _repository.GetDataSetBySP("stp_npd_GetBusinessUnitByUserId", System.Data.CommandType.StoredProcedure, osqlParameter);

            dynamic _BUObjects = new ExpandoObject();
            if (dbresult != null)
            {
                if (dbresult.Tables[0] != null && dbresult.Tables[0].Rows.Count > 0)
                {
                    _BUObjects = dbresult.Tables[0].DataTableToList<MasterBusinessUnitEntity>();
                }
            }
            return _BUObjects;
        }
    }
}
