namespace EmcureNPD.Utility.Audit
{
    public class AuditLog
    {
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string DisplayName { get; set; }
    }
}