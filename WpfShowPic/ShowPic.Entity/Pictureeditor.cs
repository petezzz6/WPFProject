using System;
using System.Collections.Generic;

namespace ShowPic.Entity
{
    public partial class Pictureeditor
    {
        public int Id { get; set; }
        public string? Role { get; set; }
        public string? Name { get; set; }
        public string Password { get; set; } = null!;
    }
}
