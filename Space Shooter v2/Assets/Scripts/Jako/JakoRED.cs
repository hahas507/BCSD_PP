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
    private float timer; //쿨타임 타이머
    [SerializeField] private float bashForThisSecond;
    [SerializeField] private float bashCoolTime;

    protected override void Update()
    {
        base.Update();
        BashDetect();
    }

    private void Start()
    {
        agent.speed = 0;
        agent.angularSpeed = 0;
    }

    private void BashDetect()
    {
        timer += Time.deltaTime;

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
                            if (timer >= bashCoolTime)
                            {
                                StartCoroutine(StartBash(bashForThisSecond));
                            }
                        }
                    }
                }
            }
        }
        agent.speed = normalSpeed;
        agent.angularSpeed = normalAngularSpeed;
    }

    private IEnumerator StartBash(float _bash)
    {
        agent.speed = bashSpeed;
        agent.angularSpeed = 0;

        yield return new WaitForSeconds(_bash;)
    }
}