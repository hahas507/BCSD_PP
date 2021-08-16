using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JakoParent : Status
{
    [SerializeField] protected GameObject deathParticlePrefab;
    [SerializeField] protected GameObject hitParticlePrefab;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Transform target;

    [SerializeField] protected float normalSpeed; //navMesh speed
    [SerializeField] protected float normalAngularSpeed;

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            agent.SetDestination(target.position);
        }
    }

    public override void GetDamage(int _damage)
    {
        base.GetDamage(_damage);
        if (currentHP <= 0 && !isDead)
        {
            rigid.isKinematic = false;
            isDead = true;
            if (isDead)
            {
                agent.enabled = false;
                StartCoroutine(JakoDeathCoroutine());
            }
        }
    }

    protected IEnumerator JakoDeathCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            rigid.AddForce(transform.forward * 30, ForceMode.VelocityChange);
            var particleClone = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            Destroy(particleClone, 2f);
            yield return new WaitForSeconds(.2f);
        }
        Destroy(gameObject, .5f);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bullet")
        {
            var clone = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
            Destroy(clone, 1);
        }
    }
}