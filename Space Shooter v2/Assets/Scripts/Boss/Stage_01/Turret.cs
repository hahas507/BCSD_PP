using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Status
{
    [SerializeField] private GameObject waypointForMechaTrooper;
    [SerializeField] private GameObject turret;
    [SerializeField] private float viewRadius;

    [Range(0, 360)]
    [SerializeField] private float viewAngle;

    [SerializeField] private float fireAngle;
    [SerializeField] private float magazine;
    [SerializeField] private float fireRate;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask targetMask;
    private Vector3 targetDirection;

    private RaycastHit hitInfo;
    private Vector3 turretPosition;
    private Vector3 tarPosition;
    private Vector3 turretAngle;
    private float turretTimer;
    [SerializeField] private float turretShootCycle;

    private Turret theTurret;

    protected override void Awake()
    {
        base.Awake();
        theTurret = GetComponent<Turret>();
    }

    private void Update()
    {
        FindrTarget();
    }

    public override void GetDamage(float _damage)
    {
        base.GetDamage(_damage);
        if (currentHP <= 0)
        {
            waypointForMechaTrooper.SetActive(true);
            //DestroyAnimationNeeded;
            theTurret.enabled = false;
        }
    }

    private void FindrTarget()
    {
        turretTimer += Time.deltaTime;

        Collider[] target = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < target.Length; i++)
        {
            Transform targetTransform = target[i].transform;

            if (targetTransform.tag == "Player")
            {
                targetDirection = (targetTransform.position - transform.position).normalized;
                float lookAngle = Vector3.Angle(transform.forward, targetDirection);
                if (lookAngle < viewAngle / 2)
                {
                    if (Physics.Raycast(transform.position, targetDirection, out hitInfo, viewRadius, targetMask))
                    {
                        if (hitInfo.transform.tag == "Player")
                        {
                            LookAt();
                            if (turretTimer >= turretShootCycle)
                            {
                                turretTimer = 0;
                                StartCoroutine(Shoot(fireRate));
                            }
                        }
                    }
                }
            }
        }
    }

    private float GetDegree(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.z - _from.z, _to.x - _from.x) * 180 / Mathf.PI;
    }

    private void LookAt()
    {
        float turretX = turret.transform.position.x;
        float turretZ = turret.transform.position.z;

        float hitInfoX = hitInfo.transform.position.x;
        float hitInfoZ = hitInfo.transform.position.z;

        turretPosition = new Vector3(turretX, 0f, turretZ);
        tarPosition = new Vector3(hitInfoX, 0f, hitInfoZ);

        turretAngle = new Vector3(0f, GetDegree(turretPosition, tarPosition), 0f);
        turret.transform.eulerAngles = -turretAngle;
    }

    private IEnumerator Shoot(float FR)
    {
        for (int i = 0; i < magazine; i++)
        {
            float randomAngle = UnityEngine.Random.Range(turretAngle.y - fireAngle, turretAngle.y + fireAngle);
            GameObject clone = Instantiate(bulletPrefab, turret.transform.position, Quaternion.Euler(0f, -randomAngle, 0f));
            Destroy(clone, 1.5f);

            yield return new WaitForSeconds(FR);
        }
    }
}