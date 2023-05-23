using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour, IDieListener
{
    [SerializeField] private Rotater rotater;
    [SerializeField] private float speed;
    private float reachedPointDistance = 0.5f;
    private List<Vector3> pathPoint = new();
    private bool hasPath;
    private int wayPointCounter = 0;
    private GameObject currentTarget;
    private Seeker seeker;
    private Rigidbody2D rb2D;
    private Animator animator;
    private Coroutine followCoroutine;
    public Action ReachedEndPointPath { get; set; }
    public bool HasPath { get => hasPath; }

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rotater = GetComponent<Rotater>();
    }
    private void Update()
    {
        animator.SetBool("IsMoving", hasPath);

        if (hasPath)
        {
            if (wayPointCounter < pathPoint.Count)
            {
                if (Vector2.Distance(rb2D.position, pathPoint[wayPointCounter]) > reachedPointDistance)
                {
                    Vector2 direction = ((Vector2)pathPoint[wayPointCounter] - rb2D.position).normalized;
                    rb2D.MovePosition(rb2D.position + direction * speed * Time.deltaTime);
                    rotater.Rotate((direction.x < 0), this);
                }
                else
                {
                    wayPointCounter++;
                }
            }
            else
            {
                OnPathCompleted();
            }
        }
    }
    public void Move(Vector2 targetPosition)
    {
        ResetPath();
        seeker.StartPath(transform.position, targetPosition, OnPathCalculated);
    }
    public void Move(GameObject target)
    {
        if (followCoroutine == null)
        {
            followCoroutine = StartCoroutine(FollowTarget(target));
        }
    }
    public void MoveToRandomPosition()
    {
        ResetPath();
        float randomXCoordinate = UnityEngine.Random.Range(-AstarPath.active.data.gridGraph.width / 2, AstarPath.active.data.gridGraph.width / 2);
        float randomYCoordinate = UnityEngine.Random.Range(-AstarPath.active.data.gridGraph.depth / 2, AstarPath.active.data.gridGraph.depth / 2);
        Vector2 randomPoint = new Vector2(randomXCoordinate, randomYCoordinate);
        Move(randomPoint);
    }
    private IEnumerator FollowTarget(GameObject target)
    {
        while (true)
        {
            if (transform.position.x > target.transform.position.x)
            {
                if (target.transform.rotation.y == 0)
                {
                    seeker.StartPath(transform.position, target.transform.position + target.transform.right, OnPathCalculated);
                }
                else
                {
                    seeker.StartPath(transform.position, target.transform.position - target.transform.right, OnPathCalculated);
                }
            }
            else
            {
                if (target.transform.rotation.y == 0)
                {
                    seeker.StartPath(transform.position, target.transform.position - target.transform.right, OnPathCalculated);
                }
                else
                {
                    seeker.StartPath(transform.position, target.transform.position + target.transform.right, OnPathCalculated);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void ResetPath()
    {
        hasPath = false;
        if (followCoroutine != null)
        {
            StopCoroutine(followCoroutine);
        }
        followCoroutine = null;
        pathPoint.Clear();
        wayPointCounter = 0;
    }
    private void OnPathCalculated(Path path)
    {
        wayPointCounter = 0;
        pathPoint = path.vectorPath;
        hasPath = true;
    }
    private void OnPathCompleted()
    {
        hasPath = false;
        ReachedEndPointPath?.Invoke();
    }

    public void OnDie()
    {
        ResetPath();
    }
}
