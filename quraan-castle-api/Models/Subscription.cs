using System;
using System.Collections.Generic;

namespace quraan_castle_api.Models
{
    public partial class Subscription
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PlanId { get; set; }
        public int? StatusId { get; set; }
        public float Cost { get; set; }
        public string? Currency { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Trn { get; set; }
        public string? ToAccount { get; set; }
        public string? Error { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual Plan Plan { get; set; } = null!;
    }
}
