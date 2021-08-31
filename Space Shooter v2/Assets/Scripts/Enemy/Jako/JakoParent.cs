using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class JakoParent : Status
{
    [SerializeField] protected GameObject deathParticlePrefab;

    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected NavMeshAgent agent;

    protected Transform target; //Player
    [SerializeField] protected Transform[] Waypoints; //Around Player

    [SerializeField] protected float repathTime;
    protected float repathTimer; //경로 타이머

    protected override void Awake()
    {
        base.Awake();
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (Waypoints.Length != 0)
        {
            Waypoints[0] = GameObject.Find("/Player/Mecha01/Waypoints/Waypoint").transform;
            Waypoints[1] = GameObject.Find("/Player/Mecha01/Waypoints/Waypoint (1)").transform;
            Waypoints[2] = GameObject.Find("/Player/Mecha01/Waypoints/Waypoint (2)").transform;
            Waypoints[3] = GameObject.Find("/Player/Mecha01/Waypoints/Waypoint (3)").transform;
            Waypoints[4] = GameObject.Find("/Player/Mecha01/Waypoints/Waypoint (4)").transform;
            Waypoints[5] = GameObject.Find("/Player/Mecha01/Waypoints/Waypoint (5)").transform;
        }
    }

    protected abstract void Update();

    public override void GetDamage(float _damage)
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

    protected IEnumerator RandomPathFind()
    {
        YieldInstruction wait = new WaitForSeconds(2);
        while (repathTimer >= repathTime)
        {
            agent.SetDestination(Waypoints[UnityEngine.Random.Range(0, Waypoints.Length)].position);
            repathTimer = 0;
            yield return wait;
        }
    }

    protected IEnumerator PathFind()
    {
        YieldInstruction wait = new WaitForSeconds(2);
        while (repathTimer >= repathTime)
        {
            agent.SetDestination(target.position);
            repathTimer = 0;
            yield return wait;
        }
    }
}