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
    [SerializeField] private GameObject JakoBLUE;
    [SerializeField] private float JakoSpawnRate;
    [SerializeField] private float spawnHowMany;

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

    private void Start()
    {
    }

    private void Update()
    {
        MoveToNextWaypoint();
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
            StartCoroutine(SpawnJakoBLUE(JakoSpawnRate));
            yield return new WaitForSeconds(3);
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navMesh.SetDestination(waypoints[currentWaypoint].position);

            isStopped = false;
        }
    }

    private IEnumerator SpawnJakoBLUE(float spawnRate)
    {
        YieldInstruction wait = new WaitForSeconds(spawnRate);
        yield return wait;
        for (int i = 0; i < spawnHowMany; i++)
        {
            Instantiate(JakoBLUE, transform.position + (transform.right * 5), Quaternion.Euler(transform.forward));
            yield return wait;
        }
    }
}