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
    [SerializeField] private GameObject[] Jako;
    [SerializeField] private float JakoSpawnRate;
    [SerializeField] private float spawnHowMany;
    private Warship theWarship;

    private int JakoLength;

    public float CURRENTSPEED
    {
        get { return navMesh.speed; }
        set { navMesh.speed = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        capCol = GetComponent<CapsuleCollider>();
        theWarship = GetComponent<Warship>();
        capCol.enabled = false;
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(waypoints[0].position);
        BattleSceneManager.isMainTargetDefeated = false;
    }

    private void Update()
    {
        MoveToNextWaypoint();
        PhaseCheck();
    }

    private void PhaseCheck()
    {
        JakoLength = 0;
        if (currentHP <= thisHP * (2f / 3f))
        {
            JakoLength = 1;
            if (currentHP <= thisHP * (1f / 3f))
            {
                JakoLength = 2;
            }
        }
    }

    public override void GetDamage(float _damage)
    {
        base.GetDamage(_damage);
        if (currentHP <= 0)
        {
            isDead = true;
            BattleSceneManager.isMainTargetDefeated = true;
            GameManager.Stage01Cleared = true;
            navMesh.enabled = false;
            theWarship.enabled = false;
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
        StartCoroutine(SpawnJako(JakoSpawnRate));
    }

    private IEnumerator SpawnJako(float spawnRate)
    {
        YieldInstruction wait = new WaitForSeconds(spawnRate);
        yield return wait;
        for (int i = 0; i < spawnHowMany; i++)
        {
            Instantiate(Jako[UnityEngine.Random.Range(0, JakoLength + 1)], transform.position + new Vector3(20, 6, 0), Quaternion.identity);
            yield return wait;
            Instantiate(Jako[UnityEngine.Random.Range(0, JakoLength + 1)], transform.position + new Vector3(-20, 6, 0), Quaternion.identity);
            yield return wait;
        }
    }
}