using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyOwnAstar
{
    internal class Point : IComparable<Point>   
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
        public Vector2 position = Vector2.zero;
        public List<Point> connectedTo = new List<Point>();
        public Point from;

        public Point(Vector2 point)
        {
            this.position = point;
        }

        public void Connect(Point p)
        {
            if(!connectedTo.Contains(p))
            {
                connectedTo.Add(p);
            }
            if (!p.connectedTo.Contains(this))
            {
                p.Connect(this);
            }
        }

        public void Connect(List<Point> connectedTo)
        {
            foreach(var p in connectedTo)
            {
                Connect(p);
            }
        }

        public int CompareTo(Point other)
        {
            if (fullNumber > other.fullNumber)
                return 1;
            else if (fullNumber < other.fullNumber)
                return -1;
            else
                return 0;
        }

        public static bool operator ==(Point a, Point b)
        {
            if (a is null && b is null) return true;
            if (a is null && !(b is null)) return false;
            if (b is null && !(a is null)) return false;
            if (a.position.x > b.position.x - 0.1f && a.position.y > b.position.y - 0.1f && a.position.x < b.position.x + 0.1f && a.position.y < b.position.y + 0.1f)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Point a, Point b)
        {
            if (a is null && b is null) return false;
            if (a is null && !(b is null)) return true;
            if (b is null && !(a is null)) return true;
            if (a.position.x > b.position.x - 0.1f && a.position.y > b.position.y - 0.1f && a.position.x < b.position.x + 0.1f && a.position.y < b.position.y + 0.1f)
            {
                return false;
            }
            return true;
        }
    }
}
