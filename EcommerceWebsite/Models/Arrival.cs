using System;
using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public partial class Arrival
    {
        public int ProductId { get; set; }
        public int? Sort { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
