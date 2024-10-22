using System;
using System.Collections.Generic;

namespace Final.Models
{
    public partial class Story
    {
        public int Id { get; set; }
        public string Letter { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? Language { get; set; }
    }
}
