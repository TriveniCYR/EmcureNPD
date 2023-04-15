﻿using EmcureNPD.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EmcureNPD.Utility.Enums.GeneralEnum;

namespace EmcureNPD.Business.Core.Interface
{
    public interface IMasterUserService
    {
        Task<UserSessionEntity> Login(LoginViewModel oLogin);

        Task<DataTableResponseModel> GetAll(DataTableAjaxPostModel model);

        Task<MasterUserEntity> GetById(int id);

        Task<DBOperation> AddUpdateUser(MasterUserEntity entityUser);

        Task<DBOperation> DeleteUser(int id);

        Task<DBOperation> ChangeUserPassword(MasterUserChangePasswordEntity entityUser);

        Task<dynamic> GetUserDropdown();

        Task<dynamic> GetDepartmentCountryByBusinessUnit(int BusinessUnitId);

        Task<bool> CheckEmailAddressExists(string emailAddress);

        Task<List<MasterBusinessUnitEntity>> GetAll();

        Task<dynamic> GetRegionByBusinessUnit(string BusinessUnitId);

        Task<dynamic> GetCountryByRegion(string RegionIds);

        Task<DBOperation> ForgotPassword(string emailAddress);

        Task<string> ResetPassword(MasterUserResetPasswordEntity entity);

        Task<List<MasterBusinessUnitEntity>> GetBusinessUNitByUserId(int userid);

        Task<bool> IsTokenValid(string token);

        SMTPEntityViewModel GetSMTPConfiguration();
    }
}