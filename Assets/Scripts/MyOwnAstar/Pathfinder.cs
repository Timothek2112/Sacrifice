using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

namespace MyOwnAstar
{
    internal class Pathfinder : MonoBehaviour
    {
        public GameObject origin;
        public LayerMask obstacles;
        [SerializeField] float _gridSize = 1.0f;
        Vector2 target;
        public float gridSize { get { return _gridSize;  } set { _gridSize = value; } }
        [SerializeField] Graph graph;

        public Pathfinder(GameObject origin, LayerMask obstacles)
        {
            this.origin = origin;
            this.obstacles = obstacles;
        }

        public List<Vector2> FindWayTo(Vector2 target)
        {
            this.target = target;
            graph = new Graph(origin.transform.position);
            SortedSet<Point> whitelist = new SortedSet<Point>();
            List<Point> blacklist = new List<Point>();
            whitelist.Add(graph.first);

            for (int i = 0; i < 100; i++)
            {
                Point cur = whitelist.Min;
                List<Point> newPoints = ExtendOnLayerFrom(cur);
                blacklist.Add(cur);
                whitelist.Remove(cur);

                foreach(var point in newPoints)
                {
                    if (blacklist.Contains(point))
                        continue;
                    whitelist.Add(point);
                }

                if ((target - cur.position).magnitude < 5) return WayBack(cur);
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

        public List<Point> ExtendOnLayerFrom(Point origin)
        {
            List<Point> newPoints = new List<Point>();
            for(int x = -1; x <= 1; x++)
            {
                for(int y = -1; y <= 1; y++)
                {
                    var newPoint = ProcessPositionExtending(origin, x, y);
                    if (newPoint != null) newPoints.Add(newPoint);
                }
            }
            return newPoints;
        }

        private Point ProcessPositionExtending(Point origin, float x, float y)
        {
            if (x == 0 && y == 0) return null;
            var newPos = ExtendOnGridSize(origin.position, x, y);
            var existingPoint = graph.Find(newPos);
            if (existingPoint != null)
            {
                RecalculatePointOrigin(existingPoint, origin);
                return null;
            }
            if(Physics2D.Raycast(origin.position, new Vector2(x, y), (origin.position - newPos).magnitude, obstacles))
            {
                var newPoint = CreateNewPoint(newPos, origin);
                return newPoint;
            }
            return null;
        }

        private Vector2 ExtendOnGridSize(Vector2 origin, float x, float y)
        {
            float newX = origin.x + x * gridSize;
            float newY = origin.y + y * gridSize;
            return new Vector2(newX, newY);
        }

        private void RecalculatePointOrigin(Point rec, Point newOrigin)
        {
            float newWayLength = newOrigin.wayLength + (rec.position - newOrigin.position).magnitude;
            if (rec.fullNumber > rec.euristicLength + newWayLength)
            {
                rec.euristicLength = (target - rec.position).magnitude;
                rec.wayLength = newOrigin.wayLength + (rec.position - newOrigin.position).magnitude;
                rec.from = newOrigin;
            }
        }

        private Point CreateNewPoint(Vector2 pos, Point origin)
        {
            Point newPoint = new Point(pos);
            graph.Add(newPoint, origin);
            newPoint.euristicLength = (target - newPoint.position).magnitude;
            newPoint.wayLength = origin.wayLength + (newPoint.position - origin.position).magnitude;
            newPoint.from = origin;
            return newPoint;
        }

        Vector2 GetClosestNode(Vector2 target)
        {
            return new Vector2(Mathf.Round(target.x / gridSize) * gridSize, Mathf.Round(target.y / gridSize) * gridSize);
        }
    }
}

