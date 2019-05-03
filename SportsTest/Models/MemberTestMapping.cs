using System;
using System.Collections.Generic;

namespace SportsTest.Models
{
    public partial class MemberTestMapping
    {
        public int MemberTestId { get; set; }
        public int? MemberId { get; set; }
        public int? TestDetailsId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Decimal Distance { get; set; }
    }
}
