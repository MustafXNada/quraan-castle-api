using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Video
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Desc { get; set; }
        public int TeacherId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsFeatured { get; set; }
        public int? Order { get; set; }

        public virtual Mentor Teacher { get; set; } = null!;
    }
}
