using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Adminlogins = new HashSet<Adminlogin>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Uuid { get; set; } = null!;

        public virtual ICollection<Adminlogin> Adminlogins { get; set; }
    }
}
