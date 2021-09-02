using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MechaTrooper : Status
{
    private NavMeshAgent navMesh;

    protected override void Awake()
    {
        base.Awake();
        navMesh = GetComponent<NavMeshAgent>();
        BattleSceneManager.isMainTargetDefeated = false;
    }

    public override void GetDamage(float _damage)
    {
        base.GetDamage(_damage);
        if (currentHP <= 0)
        {
            isDead = true;
            BattleSceneManager.isMainTargetDefeated = true;
            GameManager.Stage02Cleared = true;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }
}