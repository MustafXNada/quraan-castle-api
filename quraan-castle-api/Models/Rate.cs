using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Rate
    {
        public int Id { get; set; }
        /// <summary>
        /// from 0 : 5 
        /// </summary>
        public int Rate1 { get; set; }
        public int UserId { get; set; }
        public int TeacherId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Mentor Teacher { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
