using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JakoYELLOW : JakoParent
{
    private float timer;

    [SerializeField] private int magazine;
    [SerializeField] private GameObject ZacoBullet;
    [SerializeField] private float attackCycle;
    [SerializeField] private float fireRate;

    private void Start()
    {
        agent.SetDestination(Waypoints[UnityEngine.Random.Range(0, Waypoints.Length)].position);
    }

    protected override void Update()
    {
        if (!isDead)
        {
            RedAttack();

            repathTimer += Time.deltaTime;
            StartCoroutine(RandomPathFind());
        }
    }

    private void RedAttack()
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
            var clone = Instantiate(ZacoBullet, transform.position, Quaternion.Euler(-FindTarget(target)));
            Destroy(clone, 1.3f);
            yield return new WaitForSeconds(fireRate);
        }
    }

    private float GetDegree(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.z - _from.z, _to.x - _from.x) * 180 / Mathf.PI;
    }

    private Vector3 FindTarget(Transform _fireAt)
    {
        float myX = transform.position.x; //Horizontal
        float myZ = transform.position.z; //Vertical

        float tarX = _fireAt.position.x;
        float tarZ = _fireAt.position.z;

        Vector3 myPosition = new Vector3(myX, 0f, myZ);
        Vector3 tarPosition = new Vector3(tarX, 0f, tarZ);

        return new Vector3(0f, GetDegree(myPosition, tarPosition), 0f);
    }
}