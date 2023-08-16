using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Plan
    {
        public Plan()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Desc { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsRunning { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
