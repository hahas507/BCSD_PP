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

    private void Update()
    {
        MoveToNextWaypoint();
    }

    public override void GetDamage(float _damage)
    {
        base.GetDamage(_damage);
        if (currentHP <= 0)
        {
            isDead = true;
            BattleSceneManager.isMainTargetDefeated = true;
            gameObject.SetActive(false);
            //Boss defeat event;
        }
    }

    private void MoveToNextWaypoint()
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
        StartCoroutine(SpawnJakoBLUE(JakoSpawnRate));
    }

    private IEnumerator SpawnJakoBLUE(float spawnRate)
    {
        YieldInstruction wait = new WaitForSeconds(spawnRate);
        yield return wait;
        for (int i = 0; i < spawnHowMany; i++)
        {
            Instantiate(JakoBLUE, transform.position + new Vector3(20, 6, 0), Quaternion.identity);
            yield return wait;
            Instantiate(JakoBLUE, transform.position + new Vector3(-20, 6, 0), Quaternion.identity);
            yield return wait;
        }
    }
}