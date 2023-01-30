﻿using System;
using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public partial class Product
    {
        public Product()
        {
            Attributesprices = new HashSet<Attributesprice>();
            Orderdetails = new HashSet<Orderdetail>();
            ProductSizeColors = new HashSet<ProductSizeColor>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ShortDesc { get; set; }
        public string? Description { get; set; }
        public int? CatId { get; set; }
        public double? Price { get; set; }
        public double? Discount { get; set; }
        public string? Thumb { get; set; }
        public string? Video { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool BestSellers { get; set; }
        public bool HomeFlag { get; set; }
        public bool Active { get; set; }
        public string? Tags { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }
        public string? MetaDesc { get; set; }
        public string? MetaKey { get; set; }
        public int? UnitsInStock { get; set; }

        public virtual Category? Cat { get; set; }
        public virtual ICollection<Attributesprice> Attributesprices { get; set; }
        public virtual ICollection<Orderdetail> Orderdetails { get; set; }
        public virtual ICollection<ProductSizeColor> ProductSizeColors { get; set; }
    }
}
