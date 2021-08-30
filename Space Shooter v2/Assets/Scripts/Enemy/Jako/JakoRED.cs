using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JakoRED : JakoParent
{
    [SerializeField] private float bashAngle;
    [SerializeField] private float bashRange;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float bashSpeed;

    private float bashTimer; //쿨타임 타이머

    [SerializeField] private float bashForThisSecond;
    [SerializeField] private float bashCoolTime;

    [SerializeField] private float normalSpeed; //navMesh speed
    [SerializeField] private float normalAngularSpeed;

    [SerializeField] private int explosionDamage;

    private bool isExploding = false;

    private void Start()
    {
        agent.SetDestination(target.position);
    }

    protected override void Update()
    {
        if (!isDead && !BattleSceneManager.isMainTargetDefeated)
        {
            BashDetect();

            repathTimer += Time.deltaTime;
            StartCoroutine(PathFind());
        }
    }

    private void BashDetect()
    {
        bashTimer += Time.deltaTime;

        Collider[] _target = Physics.OverlapSphere(transform.position, bashRange, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTransfrom = _target[i].transform;

            if (_targetTransfrom.tag == "Player")
            {
                Vector3 _direction = (_targetTransfrom.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);

                if (_angle < bashAngle * .5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.localPosition, _direction, out _hit, bashRange))
                    {
                        if (_hit.transform.tag == "Player")
                        {
                            if (bashTimer >= bashCoolTime)
                            {
                                bashTimer = 0;
                                StartCoroutine(StartBash(bashForThisSecond));
                            }
                        }
                    }
                }
            }
        }
    }

    private IEnumerator StartBash(float _bash)
    {
        YieldInstruction wait = new WaitForSeconds(_bash);
        bashTimer += Time.deltaTime;
        while (bashTimer <= bashForThisSecond)
        {
            agent.speed = bashSpeed;
            yield return wait;
        }

        agent.speed = normalSpeed;
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && !isDead && !isExploding)
        {
            isDead = true;
            isExploding = true;

            rigid.isKinematic = false;

            if (isDead && isExploding)
            {
                agent.enabled = false;
                if (isExploding)
                {
                    other.GetComponent<Status>().GetDamage(explosionDamage);
                    StartCoroutine(JakoDeathCoroutine());
                }
            }
        }
    }
}