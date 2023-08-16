using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Mentor
    {
        public Mentor()
        {
            Rates = new HashSet<Rate>();
            Reviews = new HashSet<Review>();
            Sections = new HashSet<Section>();
            Videos = new HashSet<Video>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? Country { get; set; }
        public string? Nationality { get; set; }
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        /// 0- women\n1- man
        /// </summary>
        public bool Gender { get; set; }
        public bool IsFeatured { get; set; }
        public int? Order { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
