using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MechaTrooper : Status
{
    [SerializeField] private Transform[] waypoints;
    private NavMeshAgent navMesh;

    protected override void Awake()
    {
        base.Awake();
        navMesh = GetComponent<NavMeshAgent>();
    }

    public override void GetDamage(float _damage)
    {
        base.GetDamage(_damage);
    }

    private void Start()
    {
    }

    private void Update()
    {
    }
}