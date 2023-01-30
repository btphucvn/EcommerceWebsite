using System;
using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public partial class Attribute
    {
        public Attribute()
        {
            Attributesprices = new HashSet<Attributesprice>();
        }

        public int AttributeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Attributesprice> Attributesprices { get; set; }
    }
}
