using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? Subject { get; set; }
        public string? Message1 { get; set; }
        /// <summary>
        /// 0- message. 
        /// 1- request. 
        /// 2- account issue.
        /// 3- technical issue.
        /// </summary>
        public int TypeId { get; set; }

        public virtual User Customer { get; set; } = null!;
    }
}
