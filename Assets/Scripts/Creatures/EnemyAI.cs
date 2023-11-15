using Assets.Scripts.Creatures.States;
using MyOwnAstar;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
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
    [SerializeField] Pathfinder pathfinder;
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
        pathfinder.origin = gameObject;
        pathfinder.obstacles = obstacles;
        pathfinder.gridSize = gridSize;
    }

    private void FixedUpdate()
    {
        currentState.Process();
    }

    public void ChasePlayerProcess()
    {
        path = pathfinder.FindWayTo(playerPos.position);
        if (path == null) return;
        if (path.Count == 0) return;

        _rb.velocity = (path[0] - (Vector2)gameObject.transform.position).normalized * speed;
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
