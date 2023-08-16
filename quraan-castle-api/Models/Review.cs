using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Mentor Teacher { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
