using System;
using System.Collections.Generic;
using System.Text;

namespace ow_gen_lib.Models
{
    public abstract class Entry : Entity
    {
        public int PointCost { get; set; } = 0;
        public bool Active { get; set; } = false;
        public bool Selected { get; set; } = false;
        public Uri Url { get; set; } = new Uri("https://tow.whfb.app/bad_link");
    }
}

