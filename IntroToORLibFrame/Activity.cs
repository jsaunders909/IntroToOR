using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToORLibFrame
{
    public class Activity
    {
        public readonly int from;
        public readonly int to;
        public readonly double duration;
        public readonly string name;

        public double TotalFloat;

        public Activity(string name, int from, int to, double duration)
        {
            this.name = name;
            this.from = from;
            this.to = to;
            this.duration = duration;
        }
    }
}
