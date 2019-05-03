using System;
using System.Collections.Generic;

namespace SportsTest.Models
{
    public partial class Memders
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int UserType { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
