using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyOwnAstar
{
    [Serializable]
    internal class Graph
    {
        public Point first;
        List<Point> points = new List<Point>();
        public int Count = 0;

        public Graph(Vector2 first)
        {
            this.first = new Point(first);
            Add(this.first, new List<Point>());
        }

        public void Add(Vector2 point, Point connectedTo)
        {
            Point p = new Point(point);
            if (!Contains(connectedTo))
                return;
            Count++;
            connectedTo.Connect(p);
            points.Add(p);
        }

        public void Add(Point point, Point connectedTo)
        {
            if (!Contains(connectedTo))
                return;
            Count++;
            connectedTo.Connect(point);
            points.Add(point);
        }

        public void Add(Point point, List<Point> connectedTo)
        {
            points.Add(point);
            foreach(var conn in connectedTo)
            {
                if (!Contains(conn))
                    continue;
                point.Connect(conn);
            }
            Count++;
        }

        public void Add(Vector2 point, List<Point> connectedTo)
        {

            Point p = new Point(point);
            foreach(var conn in connectedTo)
            {
                if (!Contains(conn))
                    continue;
                p.Connect(conn);
            }
            Count++;
            points.Add(p);
        }

        public Point Find(Vector2 pos)
        {
            return points.FirstOrDefault(p => p == new Point(pos));  
        }

        public bool Contains(Point point)
        {
            return points.Contains(point);
        }

    }
}
