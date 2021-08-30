using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JakoBLUE : JakoParent
{
    [SerializeField] private GameObject ZacoBullet;
    private float timer;
    [SerializeField] private float attackCycle;
    [SerializeField] private int magazine;
    [SerializeField] private float fireRate;
    private JakoBLUE theBLUE;

    private void Start()
    {
        agent.enabled = false;
        rigid.AddForce(transform.forward * 100, ForceMode.Impulse);

        Invoke("StartNav", 1.5f);
    }

    private void StartNav()
    {
        agent.enabled = true;
    }

    protected override void Update()
    {
        if (!isDead && agent.enabled && !BattleSceneManager.isMainTargetDefeated)
        {
            BlueAttack();

            repathTimer += Time.deltaTime;
            StartCoroutine(PathFind());
        }
    }

    private void BlueAttack()
    {
        timer += Time.deltaTime;
        if (timer >= attackCycle && !isDead)
        {
            StartCoroutine(AttackCoroutine());
            timer = 0;
        }
    }

    private IEnumerator AttackCoroutine()
    {
        for (int i = 0; i < magazine; i++)
        {
            var clone = Instantiate(ZacoBullet, transform.position, Quaternion.LookRotation(-transform.right));
            Destroy(clone, 1.3f);
            yield return new WaitForSeconds(fireRate);
        }
    }
}