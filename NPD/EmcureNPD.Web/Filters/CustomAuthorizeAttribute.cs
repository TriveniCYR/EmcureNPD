using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Models;
using EmcureNPD.Utility.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureNPD.Web.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                //string token = string.Empty;
                //context.HttpContext.Request.Cookies.TryGetValue("EmcureNPDToken", out token);

                var user = context.HttpContext.User;

                if (!user.Identity.IsAuthenticated)
                    context.Result = new RedirectResult("~/Account/Login");
                else
                {
                    UserSessionEntity oUserLoggedInModel = UtilityHelper.GetUserFromClaims(user.Claims);
                    oUserLoggedInModel.UserToken = string.Empty;

                    context.HttpContext.Session.SetString(UserHelper.LoggedInUserEmailAddress, oUserLoggedInModel.Email);
                    context.HttpContext.Session.SetString(UserHelper.LoggedInUserName, oUserLoggedInModel.FullName);

                    //context.HttpContext.Session.SetInt32(UserHelper.LoggedInUserId, oUserLoggedInModel.UserId);
                    IsUserAuthorized(context);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void IsUserAuthorized(AuthorizationFilterContext context)
        {
            try
            {
                if (context.HttpContext.Session.GetString(UserHelper.LoggedInRoleId) != null)
                {
                    int rolId = (int)context.HttpContext.Session.GetInt32(UserHelper.LoggedInRoleId);
                    if (rolId > 0)
                    {
                        IEnumerable<RolePermissionModel> obj = UtilityHelper.GetModuleRole<dynamic>((Convert.ToInt32(rolId)));

                        var controllerName = context.RouteData.Values["controller"];
                        var action = context.RouteData.Values["action"];
                        if (obj != null)
                        {
                            RolePermissionModel objList = obj.Where(o => o.ControlName !=null && o.ControlName.Trim() == Convert.ToString(controllerName).Trim()).FirstOrDefault();
                            if (objList == null && Convert.ToString(controllerName) != "Home")
                                context.Result = new RedirectResult("~/Home/AccessRestriction");
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}