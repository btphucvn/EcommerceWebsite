using System;
using System.Collections.Generic;

namespace EcommerceWebsite.Models
{
    public partial class Slide
    {
        public int SlideId { get; set; }
        public string? Thumb { get; set; }
        public string? Link { get; set; }
        public int? Sort { get; set; }
    }
}
