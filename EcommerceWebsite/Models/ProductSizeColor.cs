using System;
using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public partial class ProductSizeColor
    {
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        public int? Inventory { get; set; }

        public virtual Color Color { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Size Size { get; set; } = null!;
    }
}
