using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class User
    {
        public User()
        {
            Attends = new HashSet<Attend>();
            Messages = new HashSet<Message>();
            Rates = new HashSet<Rate>();
            Reviews = new HashSet<Review>();
            Subscriptions = new HashSet<Subscription>();
            Trialsessions = new HashSet<Trialsession>();
            Userlogins = new HashSet<Userlogin>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Uuid { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsSubscriber { get; set; }
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        /// 0 women
        /// 1 man
        /// </summary>
        public bool Gender { get; set; }

        public virtual ICollection<Attend> Attends { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        public virtual ICollection<Trialsession> Trialsessions { get; set; }
        public virtual ICollection<Userlogin> Userlogins { get; set; }
    }
}
