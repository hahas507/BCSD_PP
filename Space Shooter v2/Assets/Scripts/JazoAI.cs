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

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackCycle)
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