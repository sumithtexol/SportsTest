using System;
using System.Collections.Generic;

namespace SportsTest.Models
{
    public partial class TestDetails
    {
        public int TestDetailsId { get; set; }
        public int? TestId { get; set; }
        public int? NoOfParticipants { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
