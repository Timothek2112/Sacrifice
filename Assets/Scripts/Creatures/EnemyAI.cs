using Aoiti.Pathfinding;
using Assets.Scripts.Creatures.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    Transform playerPos;
    Rigidbody2D _rb;
    public float speed;
    [SerializeField] List<Vector2> path = new List<Vector2>();
    [SerializeField] float gridSize = 1;
    [SerializeField] LayerMask obstacles;
    Pathfinder<Vector2> pathfinder;
    [SerializeField] bool snapToGrid = false;
    [SerializeField] bool searchShortcut = false;
    bool pathNeedUpdate = true;
    float timeBeforeUpdate = 0.2f;
    IState currentState;

    private void Awake()
    {
        EnterState(new Idle(this));
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
        pathfinder = new Pathfinder<Vector2>(GetDistance, GetNeighbourNodes, 1000);
    }

    private void FixedUpdate()
    {
        currentState.Process();
    }

    IEnumerator UpdatePath()
    {
        pathNeedUpdate = false;
        GetMoveCommand(playerPos.position);
        yield return new WaitForSeconds(timeBeforeUpdate);
        pathNeedUpdate = true;
    }

    public void ChasePlayerProcess()
    {
        if (pathNeedUpdate)
        {
            StartCoroutine(UpdatePath());
        }
        if (path.Count > 0)
        {
            Vector2 dirToPlayer = (path[0] - (Vector2)transform.position).normalized;
            _rb.velocity = new Vector2(dirToPlayer.x * speed, dirToPlayer.y * speed);
            if (((Vector2)transform.position - path[0]).sqrMagnitude < speed * speed)
            {
                path.RemoveAt(0);
            }
        }
    }

    Dictionary<Vector2, float> GetNeighbourNodes(Vector2 pos)
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j) * gridSize;
                if (!Physics2D.Linecast(pos, pos + dir, obstacles))
                {
                    neighbours.Add(GetClosestNode(pos + dir), dir.magnitude);
                }
            }

        }
        return neighbours;
    }

    float GetDistance(Vector2 A, Vector2 B)
    {
        return (A - B).sqrMagnitude; //Uses square magnitude to lessen the CPU time.
    }

    Vector2 GetClosestNode(Vector2 target)
    {
        return new Vector2(Mathf.Round(target.x / gridSize) * gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    void GetMoveCommand(Vector2 target)
    {
        Vector2 closestNode = GetClosestNode(transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
        {
            Debug.Log("a");
            if (searchShortcut && path.Count > 0)
                this.path = ShortenPath(path);
            else
            {
                this.path = new List<Vector2>(path);
                if (!snapToGrid) this.path.Add(target);
            }

        }

    }

    List<Vector2> ShortenPath(List<Vector2> path)
    {
        List<Vector2> newPath = new List<Vector2>();

        for (int i = 0; i < path.Count; i++)
        {
            newPath.Add(path[i]);
            for (int j = path.Count - 1; j > i; j--)
            {
                if (!Physics2D.Linecast(path[i], path[j], obstacles))
                {

                    i = j;
                    break;
                }
            }
            newPath.Add(path[i]);
        }
        newPath.Add(path[path.Count - 1]);
        return newPath;
    }

    public void EnterState(IState state)
    {
        if(currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void Jump(Vector2 direction, float force)
    {

    }
}
