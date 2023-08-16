using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Adminlogin
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; } = null!;
    }
}
