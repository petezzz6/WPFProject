using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowPic.Utils
{
    public class Page<T>
    {
        public int count { get; set; }
        public List<T> items { get; set; }
    }
}
