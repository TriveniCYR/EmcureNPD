using System;
using System.Collections.Generic;

namespace EmcureCERI.Business.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int? QuesId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
