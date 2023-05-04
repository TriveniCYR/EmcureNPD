using EmcureNPD.Utility.Utility;
using JsonDiffPatchDotNet;
using Newtonsoft.Json;
using ObjectsComparer;
using System;
using System.Collections.Generic;

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
            //var anotherJsononj = JsonConvert.SerializeObject(differences);
            //string comparison = string.Empty;
            //bool strDiff = DeepCompare(Old, New,out comparison);
            return JsonConvert.SerializeObject(auditlog);
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
}