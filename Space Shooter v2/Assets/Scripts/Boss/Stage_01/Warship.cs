using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Warship : Status
{
    private NavMeshAgent navMesh;
    [SerializeField] private Transform[] waypoints;
    private int currentWaypoint;

    protected override void Awake()
    {
        base.Awake();
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(waypoints[0].position);
        StartCoroutine(MoveToNextWaypoint());
    }

    private void Update()
    {
        if (navMesh.remainingDistance <= navMesh.stoppingDistance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navMesh.SetDestination(waypoints[currentWaypoint].position);
        }

        Debug.Log(currentWaypoint);
    }

    private IEnumerator MoveToNextWaypoint()
    {
    }
}