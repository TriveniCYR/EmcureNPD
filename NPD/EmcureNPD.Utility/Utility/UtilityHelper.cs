using EmcureNPD.Business.Models;
using EmcureNPD.Utility.Enums;
using EmcureNPD.Utility.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace EmcureNPD.Utility.Utility
{
    public class UtilityHelper
    {
        public static UserSessionEntity GetUserFromClaims(IEnumerable<System.Security.Claims.Claim> _claim)
        {
            UserSessionEntity oUserLoggedInModel = new UserSessionEntity();

            foreach (var item in _claim)
            {
                foreach (var prop in oUserLoggedInModel.GetType().GetProperties())
                {
                    if (prop.Name.ToLower() == item.Type.ToLower())
                    {
                        if (prop.PropertyType == typeof(System.Int32) || prop.PropertyType == typeof(System.Int64) || prop.PropertyType == typeof(System.UInt16))
                        {
                            prop.SetValue(oUserLoggedInModel, (!string.IsNullOrEmpty(item.Value) ? Convert.ToInt32(item.Value) : 0));
                        }
                        else if (prop.PropertyType == typeof(System.String))
                        {
                            prop.SetValue(oUserLoggedInModel, item.Value);
                        }
                    }
                }
            }
            return oUserLoggedInModel;
        }
        public static string Encrypt(string password)
        {
            try
            {
                if (password == null) throw new ArgumentNullException("plainText");

                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string Decreypt(string encodedData)
        {
            if (encodedData == null) throw new ArgumentNullException("plainText");
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }

        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        static Dictionary<int, object> _dict;
        public static bool AddModuleRole<T>(int key, T value) where T : class
        {
            if (_dict == null)
            {
                _dict = new Dictionary<int, object>();                
            }
            object val;
            _dict.TryGetValue(key, out val);
            if(val!=null)
            {
                 _dict.Remove(key);
            }
            return _dict.TryAdd(key, value);
            
        }

        public static T GetModuleRole<T>(int key) where T : class
        {
            if (_dict != null)
            {
                object val;
                _dict.TryGetValue(key, out val);
                return val as T;
            }
            return null as T;
            
        }
        public static bool RemoveModuleRole(int key)
        {
            if (_dict != null)
            {
                return _dict.Remove(key);
            }
            else
            {
                return false;
            }
        }
      
        public static bool GetMenuAccess(int ModuleId, int RoleId, int SubModuleId = 0)
        {
            //IEnumerable<RolePermissionModel> objVal = GetModuleRole<IEnumerable<RolePermissionModel>>(RoleId);

            //if (objVal != null)
            //{
            //    var _permissionObject = objVal.Where(o => o.MainModuleId == ModuleId).FirstOrDefault();
            //    if (_permissionObject != null && _permissionObject.RoleId > 0)
            //    {
            //        if (_permissionObject.View || _permissionObject.Edit || _permissionObject.Delete || _permissionObject.Add || _permissionObject.Approve)
            //        {
            //            return true;
            //        }
            //    }
            //}
            return true;
        }

        public static bool GetAccess(int ModuleId, int RoleId, int PermissionType, int SubModuleId = 0)
        {
            IEnumerable<RolePermissionModel> objVal = GetModuleRole<IEnumerable<RolePermissionModel>>(RoleId);
            if (objVal != null)
            {
                var _permissionObject = objVal.Where(o => o.MainModuleId == ModuleId).FirstOrDefault();
                if (_permissionObject != null && _permissionObject.RoleId > 0)
                {
                    //switch (PermissionType)
                    //{
                    //    case:  PermissionEnum.Add
                    //    return _permissionObject.Add;
                    //default:
                    //        break;
                    //}
                    return true;
                }
            }
            return false;
        }

        public static RolePermissionModel GetCntrActionAccess(string controllerNm, int loginRoleId)
        {            
            RolePermissionModel objList = new RolePermissionModel();
            IEnumerable<RolePermissionModel> obj = UtilityHelper.GetModuleRole<dynamic>(loginRoleId);
            if (obj != null)
            {
                objList = obj.Where(o => o.ControlName != null && o.ControlName.Trim() == controllerNm).FirstOrDefault();
                
            }
            return objList;

        }

        public static RolePermissionModel GetCntrActionAccess(int ModuleId, int loginRoleId, int SubModuleId = 0)
        {
            RolePermissionModel objList = new RolePermissionModel();
            IEnumerable<RolePermissionModel> obj = UtilityHelper.GetModuleRole<dynamic>(loginRoleId);
            if (obj != null)
            {
                objList = obj.Where(o => o.ControlName != null && o.MainModuleId == ModuleId && (o.SubModuleId == SubModuleId || SubModuleId == 0)).FirstOrDefault();

            }
            return objList;
        }
    }
}