using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Attend
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SectionId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual Section Section { get; set; } = null!;
    }
}
