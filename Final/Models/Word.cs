using System;
using System.Collections.Generic;

namespace Final.Models
{
    public partial class Word
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public string Definition { get; set; } = null!;
        public string Letter { get; set; } = null!;
    }
}
