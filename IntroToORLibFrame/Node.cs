using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToORLibFrame
{
    public class Node
    {
        public int TopOrder { get; set; }
        // Topological Order
        public List<Activity> ForwardStar;

        public List<Activity> ReverseStar;

        public double EET { get; set; }
        public double LET { get; set; }

        public Node(int topOrder)
        {
            TopOrder = topOrder;

            ForwardStar = new List<Activity>();

            ReverseStar = new List<Activity>();
        }
    }
}
