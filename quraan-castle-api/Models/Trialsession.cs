using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Trialsession
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        /// <summary>
        /// 0 - pending\n1 - schedulaed
        /// </summary>
        public string StatusId { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual User Customer { get; set; } = null!;
    }
}
