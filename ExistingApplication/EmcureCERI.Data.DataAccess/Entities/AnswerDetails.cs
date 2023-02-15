using System;
using System.Collections.Generic;

namespace EmcureCERI.Data.DataAccess.Entities
{
    public partial class AnswerDetails
    {
        public int Id { get; set; }
        public int? QuestId { get; set; }
        public int? PrescriberId { get; set; }
        public string Answer { get; set; }
    }
}
