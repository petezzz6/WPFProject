using System;
using System.Collections.Generic;

namespace ShowPic.Entity
{
    public partial class Pictureentity
    {
        public int PicId { get; set; }
        public string? PicName { get; set; }
        public string? PicTag { get; set; }
        public string? PicDescription { get; set; }
        public sbyte? PicRate { get; set; }
        public byte[]? PicData { get; set; }
    }
}
