using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Userlogin
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
