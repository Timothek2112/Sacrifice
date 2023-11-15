using Aoiti.Pathfinding;
using Assets.Scripts.Creatures.States;
using MyOwnAstar;
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
    Pathfinder pathfinder;
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
        pathfinder = new Pathfinder(gameObject, obstacles);
    }

    private void FixedUpdate()
    {
        currentState.Process();
    }

    IEnumerator UpdatePath()
    {
        pathNeedUpdate = false;
        path = pathfinder.FindWayTo(playerPos.position);
        yield return new WaitForSeconds(timeBeforeUpdate);
        pathNeedUpdate = true;
    }

    public void ChasePlayerProcess()
    {
        /*if (pathNeedUpdate)
        {
            StartCoroutine(UpdatePath());
        }*/
        StartCoroutine(UpdatePath());
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
