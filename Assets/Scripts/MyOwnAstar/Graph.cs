using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyOwnAstar
{
    internal class Graph
    {
        public Point first;
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
        }

        public void Add(Point point, Point connectedTo)
        {
            if (!Contains(connectedTo))
                return;
            Count++;
            connectedTo.Connect(point);
        }

        public void Add(Point point, List<Point> connectedTo)
        {
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
        }

        public Point Find(Vector2 pos)
        {
            List<Point> list = new List<Point>();
            Queue<Point> toSeek = new Queue<Point>();
            toSeek.Enqueue(first);
            while (toSeek.Count > 0)
            {
                Point p = toSeek.Dequeue();
                list.Add(p);
                if (p.position == pos)
                    return p;
                foreach (var connected in p.connectedTo)
                {
                    if (list.Contains(connected)) continue;
                    toSeek.Enqueue(connected);
                }
            }
            return null;
        }

        public bool Contains(Point point)
        {
            List<Point> list = new List<Point>();
            Queue<Point> toSeek = new Queue<Point>();
            toSeek.Enqueue(first);
            while(toSeek.Count > 0)
            {
                Point p = toSeek.Dequeue();
                list.Add(p);
                if (p == point || p.position == point.position)
                    return true;
                foreach(var connected in p.connectedTo)
                {
                    if(list.Contains(connected)) continue;
                    toSeek.Enqueue(connected);
                }
            }
            return false;
        }

    }
}
