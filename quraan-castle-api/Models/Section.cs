using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Section
    {
        public Section()
        {
            Attends = new HashSet<Attend>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public string Subject { get; set; } = null!;
        public int TeacherId { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? Time { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public virtual Mentor Teacher { get; set; } = null!;
        public virtual ICollection<Attend> Attends { get; set; }
    }
}
