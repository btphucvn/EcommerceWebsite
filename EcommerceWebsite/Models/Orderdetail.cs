using System;
using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public partial class Orderdetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? OrderNumber { get; set; }
        public int? Amount { get; set; }
        public int? Discount { get; set; }
        public int? TotalMoney { get; set; }
        public DateTime? CreateDate { get; set; }
        public double? Price { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
