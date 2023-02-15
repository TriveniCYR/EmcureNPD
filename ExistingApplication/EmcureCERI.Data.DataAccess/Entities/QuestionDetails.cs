using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class QuestionDetails
    {
        public int Id { get; set; }
        public string QuestionDe { get; set; }
        public string QuestionGb { get; set; }
        public string QuestionEs { get; set; }
        public string QuestionFr { get; set; }
        public string QuestionBe { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
