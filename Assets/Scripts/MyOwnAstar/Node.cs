using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnAstar
{
    [Serializable]
    internal class Node : IComparable<Node>
    {
        public float fullNumber
        {
            get
            {
                return wayLength + euristicLength;
            }
        }
        public float wayLength = 0;
        public float euristicLength = 0;
        public Point point;
        public Node from;

        public Node(Point point, Node from)
        {
            this.point = point;
            this.from = from;
        }

        public Node(Point point)
        {
            this.point = point;
        }

        public Node(Point point, Node from, float wayLength, float euristicLength)
        {
            this.point = point;
            this.from = from;
            this.wayLength = wayLength;
            this.euristicLength = euristicLength;
        }

        public int CompareTo(Node other)
        {
            if (fullNumber > other.fullNumber)
                return 1;
            else if (fullNumber < other.fullNumber)
                return -1;
            else
                return 0;
        }
    }
}
