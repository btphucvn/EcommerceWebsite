using System;
using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public partial class Transactstatus
    {
        public Transactstatus()
        {
            Orders = new HashSet<Order>();
        }

        public int TransactStatusId { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
