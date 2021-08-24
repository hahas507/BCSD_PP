using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warship : Status
{
    private NavMeshAgent navMesh;
    [SerializeField] private Transform[] waypoints;
    private int currentWaypoint;
    private CapsuleCollider capCol;
    private bool isStopped = false;

    public float CURRENTSPEED
    {
        get { return navMesh.speed; }
        set { navMesh.speed = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        capCol = GetComponent<CapsuleCollider>();
        capCol.enabled = false;
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(waypoints[0].position);
    }

    private void Update()
    {
        MoveToNextWaypoint();
        Debug.Log(isStopped);
    }

    private void MoveToNextWaypoint()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 1 && !isStopped)
        {
            StartCoroutine(SetDestination());
        }
    }

    private IEnumerator SetDestination()
    {
        if (!isStopped)
        {
            isStopped = true;
            yield return new WaitForSeconds(3);
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navMesh.SetDestination(waypoints[currentWaypoint].position);

            isStopped = false;
        }
    }
}