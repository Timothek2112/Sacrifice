using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyOwnAstar
{
    internal class Pathfinder
    {
        GameObject origin;
        LayerMask obstacles;
        float gridSize = 1;
        Graph graph;

        public Pathfinder(GameObject origin, LayerMask obstacles)
        {
            this.origin = origin;
            this.obstacles = obstacles;
        }

        public List<Vector2> FindWayTo(Vector2 target)
        {
            graph = new Graph(origin.transform.position);
            SortedSet<Point> whitelist = new SortedSet<Point>();
            List<Point> blacklist = new List<Point>();
            whitelist.Add(graph.first);
            while(whitelist.Count > 0)
            {
                var current = whitelist.Min;
                if (blacklist.Contains(current))
                    continue;
                ExtendOnLayerFrom(current);
                foreach(var p in whitelist.Min.connectedTo)
                {
                    float newWayLength = Vector2.Distance(current.position, p.position);
                    float newEuristicLength = Vector2.Distance(current.position, target);
                    if(p.from == null || p.fullNumber > newEuristicLength + newWayLength)
                    {
                        p.euristicLength = newEuristicLength;
                        p.wayLength = newWayLength;
                        p.from = current;
                    }
                    if(Vector2.Distance(p.position, GetClosestNode(target)) <= 10)
                    {
                        return WayBack(p);
                    }
                    if(!blacklist.Contains(p))
                        whitelist.Add(p);
                }
                whitelist.Remove(current);
                blacklist.Add(current);
            }
            return null;
        }

        public List<Vector2> WayBack(Point node)
        {
            List<Vector2> way = new List<Vector2>();
            while(node.from != null)
            {
                way.Add(node.from.position);
                node = node.from;
            }
            return way;
        }

        public void ExtendOnLayerFrom(Point origin)
        {
            Vector2 pos = GetClosestNode(origin.position);
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0) continue;

                    Vector2 dir = new Vector2(i, j) * gridSize;
                    if (!Physics2D.Linecast(pos, pos + dir, obstacles))
                    {
                        Vector2 closestNode = GetClosestNode(pos + dir);
                        var point = graph.Find(closestNode);
                        if(point != null)
                        {
                            if (origin.connectedTo.Contains(point))
                                continue;
                            origin.Connect(point);
                        }
                        else
                        {
                            point = new Point(closestNode);
                            origin.Connect(point);
                        }
                    }
                }
            }
        }

        Vector2 GetClosestNode(Vector2 target)
        {
            return new Vector2(Mathf.Round(target.x / gridSize) * gridSize, Mathf.Round(target.y / gridSize) * gridSize);
        }
    }
}

