using EmcureNPD.Utility.Utility;
using Newtonsoft.Json;
using ObjectsComparer;
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

            return JsonConvert.SerializeObject(auditlog);
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
