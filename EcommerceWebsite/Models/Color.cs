using System;
using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public partial class Color
    {
        public Color()
        {
            ProductSizeColors = new HashSet<ProductSizeColor>();
        }

        public int ColorId { get; set; }
        public string? ColorCode { get; set; }

        public virtual ICollection<ProductSizeColor> ProductSizeColors { get; set; }
    }
}
