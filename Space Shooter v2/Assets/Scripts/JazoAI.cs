using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JazoAI : MonoBehaviour
{
    [SerializeField] private GameObject ZacoBullet;
    private float timer;
    [SerializeField] private float attackCycle;
    [SerializeField] private float magazine;
    [SerializeField] private float fireRate;

    [SerializeField] private float bashAngle;
    [SerializeField] private float bashRange;
    [SerializeField] private LayerMask targetMask;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackCycle)
        {
            StartCoroutine(AttackCoroutine());
            timer = 0;
        }

        BashRange();
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

    private Vector3 Boundary(float _angle)
    {
        _angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }

    private void BashRange()
    {
        Vector3 leftBoundary = Boundary(-bashAngle * 0.5f);
        Vector3 rightBoundary = Boundary(bashAngle * 0.5f);

        Collider[] _target = Physics.OverlapSphere(transform.position, bashRange, targetMask);
    }
}