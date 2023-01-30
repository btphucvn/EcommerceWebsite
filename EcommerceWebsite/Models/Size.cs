using System;
using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public partial class Size
    {
        public Size()
        {
            ProductSizeColors = new HashSet<ProductSizeColor>();
        }

        public int SizeId { get; set; }
        public string? SizeName { get; set; }

        public virtual ICollection<ProductSizeColor> ProductSizeColors { get; set; }
    }
}
