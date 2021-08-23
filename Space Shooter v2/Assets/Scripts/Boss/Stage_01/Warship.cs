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
    }

    private void MoveToNextWaypoint()
    {
        if (navMesh.remainingDistance <= navMesh.stoppingDistance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navMesh.SetDestination(waypoints[currentWaypoint].position);
        }
    }
}