using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamNav : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private int currentWaypoint;
    [SerializeField] private Transform[] waypoints;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        navMesh.Warp(waypoints[0].position);
    }

    private void Update()
    {
        if (navMesh.remainingDistance <= navMesh.stoppingDistance)
        {
            StartCoroutine(SetDestination());
        }
    }

    private IEnumerator SetDestination()
    {
        currentWaypoint++;
        if (currentWaypoint == waypoints.Length)
        {
            currentWaypoint = 0;
        }
        navMesh.SetDestination(waypoints[currentWaypoint].position);
        navMesh.isStopped = true;
        yield return new WaitForSeconds(3);
        navMesh.isStopped = false;
    }
}