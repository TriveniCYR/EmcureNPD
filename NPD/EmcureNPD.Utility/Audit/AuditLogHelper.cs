using DiffMatchPatch;
using EmcureNPD.Utility.Utility;
using JsonDiffPatchDotNet;
using Newtonsoft.Json;
using ObjectsComparer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EmcureNPD.Utility.Audit
{
    public static class AuditLogHelper
    {
        public static string ToAuditLog<TResult>(this TResult Old, TResult New) where TResult : new()
        {
            var compare = new ObjectsComparer.Comparer<TResult>();
            List<AuditLog> auditlog = new();
            compare.Compare(Old, New, out IEnumerable<Difference> differences);

            foreach (var diff in differences)
            {
                if (!diff.MemberPath.Contains("CreatedDate") && !diff.MemberPath.Contains("ModifyDate"))
                {
                    auditlog.Add(new AuditLog { OldValue = diff.Value1, NewValue = diff.Value2, PropertyName = diff.MemberPath, DisplayName = New.TryGetDisplayName<TResult>(diff.MemberPath) });
                }
            }
            bool ISmatched = false;
            ObjectCompare.CompareNew( Old,  New, out ISmatched);
            //var anotherJsononj = JsonConvert.SerializeObject(differences);
            var obj = ObjectCompare.auditlogList;
            ObjectCompare.auditlogList = null;
            return JsonConvert.SerializeObject(obj);
        }

        public static bool DeepCompare(object Old_obj, object New_Obj, out string comparison)
        {
            var diffObj = new JsonDiffPatch();
            comparison = string.Empty;
            if (ReferenceEquals(Old_obj, New_Obj)) return true;
            if ((Old_obj == null) || (New_Obj == null)) return false;
            if (Old_obj.GetType() != New_Obj.GetType()) return false;

            var Old_objJson = JsonConvert.SerializeObject(Old_obj);
            var New_ObjJson = JsonConvert.SerializeObject(New_Obj);
            var result = diffObj.Diff(Old_objJson, New_ObjJson);
            comparison = result;
            return result == null;
        }

        public static List<AuditLog> ToAuditLogs<TResult>(this TResult Old, TResult New) where TResult : new()
        {
            var compare = new ObjectsComparer.Comparer<TResult>();
            List<AuditLog> auditlog = new();
            compare.Compare(Old, New, out IEnumerable<Difference> differences);

            foreach (var diff in differences)
            {
                auditlog.Add(new AuditLog { OldValue = diff.Value1, NewValue = diff.Value2, PropertyName = diff.MemberPath, DisplayName = New.TryGetDisplayName<TResult>(diff.MemberPath) });
            }

            return auditlog;
        }
    }


    public static class ObjectCompare
    {
       public static List<AuditLog> auditlogList = new List<AuditLog>();
        //public static List<AuditLog> GetAudtiLogs()
        //{
        //    return auditlogList;
        //}

        public static bool CompareList(dynamic object1, dynamic object2)
        {
           

            bool match = false;
            List<dynamic> e1 = new List<dynamic>();
            e1.AddRange(object1);
            List<dynamic> e2 = new List<dynamic>();
            e2.AddRange(object2);
            int countFirst, countSecond;
            countFirst = e1.Count;
            countSecond = e2.Count;
            if (countFirst == countSecond)
            {
                countFirst = e1.Count - 1;
                while (countFirst > -1)
                {
                    match = false;
                    countSecond = e2.Count - 1;
                    while (countSecond > -1)
                    {
                        CompareNew(e1[countFirst], e2[countSecond], out match);
                        if (match)
                        {
                            e2.Remove(e2[countSecond]);
                            countSecond = -1;
                        }
                        if (match == false && countSecond == 0)
                        {
                            return false;
                        }
                        countSecond--;
                    }
                    countFirst--;
                }
            }
            else
            {
                return false;
            }
            return match;
        }
        public static void CompareNew(object e1, object e2, out bool match)
        {
            bool flag = true;
            match = flag;
            foreach (PropertyInfo propObj1 in e1.GetType().GetProperties())
            {                
                var propObj2 = e2.GetType().GetProperty(propObj1.Name);            
                var E1_val = propObj1.GetValue(e1, null);
                var E2_val = propObj2.GetValue(e2, null);
                
                if (propObj1.PropertyType.Name.Equals("ICollection`1") || propObj1.PropertyType.Name.Equals("List`1"))     //if (propObj1.PropertyType.Name.Equals("List`1"))
                {
                    List<dynamic> objList1 = new List<object>() {
                    propObj1.GetValue(e1, null)
                };
                    List<dynamic> objList2 = new List<object>() {
                    propObj2.GetValue(e2, null)
                };
                    if (objList1[0] !=null && objList2[0] != null)
                    {
                        if (!CompareList(objList1[0], objList2[0]))
                        {
                            match = false;
                        }

                    }
                    
                }     
                else if (!(Convert.ToString(E1_val).Equals(Convert.ToString(E2_val))))
                {
                    auditlogList.Add(new AuditLog { OldValue = Convert.ToString(E1_val), NewValue = Convert.ToString(E2_val), PropertyName = propObj1.Name, DisplayName = propObj1.Name });
                     flag = false;
                    match = flag;
                }
            }
           var Comareobj = auditlogList;            
        }
    }
}