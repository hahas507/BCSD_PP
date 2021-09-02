using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : Status
{
    [Range(0, 1)]
    [SerializeField] private float defendCycle;

    [Range(0, 100)]
    [SerializeField] private float randomPositionX, randomPositionZ;

    [SerializeField] private float repositionCycle;
    [SerializeField] private float repositionSpeed;

    private Vector3 randomPosition;

    private bool isLerping;

    private RaycastHit hitInfo;
    [SerializeField] private LayerMask bulletLayer;

    private Vector3 funnelPosition, tarPosition, funnelAngle, targetDirection;

    [Range(0, 100)]
    [SerializeField] private float viewRadius;

    private LineRenderer lineRenderer;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(LookAtBullet());
        StartCoroutine(RandomPositioning());
    }

    private void Update()
    {
        Debug.Log(currentHP);
    }

    public override void GetDamage(float _damage)
    {
        base.GetDamage(_damage);
        if (currentHP <= 0)
        {
            isDead = true;
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    private float GetDegree(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.z - _from.z, _to.x - _from.x) * 180 / Mathf.PI;
    }

    private void LookAt()
    {
        float turretX = transform.position.x;
        float turretZ = transform.position.z;

        float hitInfoX = hitInfo.transform.position.x;
        float hitInfoZ = hitInfo.transform.position.z;

        funnelPosition = new Vector3(turretX, 0f, turretZ);
        tarPosition = new Vector3(hitInfoX, 0f, hitInfoZ);

        funnelAngle = new Vector3(0f, GetDegree(funnelPosition, tarPosition), 0f);
        transform.eulerAngles = -funnelAngle;
    }

    private IEnumerator RandomPositioning()
    {
        while (true)
        {
            yield return new WaitForSeconds(repositionCycle);
            isLerping = true;
            randomPosition = new Vector3(UnityEngine.Random.Range(-randomPositionX, randomPositionX), transform.localPosition.y, UnityEngine.Random.Range(-randomPositionZ, randomPositionZ));

            while (isLerping)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, randomPosition, Time.deltaTime * repositionSpeed);
                if (Vector3.Distance(transform.localPosition, randomPosition) <= 3)
                {
                    isLerping = false;
                }
                yield return null;
            }
        }
    }

    private IEnumerator LookAtBullet()
    {
        while (true)
        {
            Collider[] target = Physics.OverlapSphere(transform.position, viewRadius, bulletLayer);

            for (int i = 0; i < target.Length; i++)
            {
                Transform targetTransform = target[i].transform;

                if (targetTransform.tag == "PlayerBullet")
                {
                    targetDirection = (targetTransform.position - transform.position).normalized;

                    if (Physics.Raycast(transform.position, targetDirection, out hitInfo, viewRadius, bulletLayer))
                    {
                        if (hitInfo.transform.tag == "PlayerBullet" && hitInfo.transform)
                        {
                            if (hitInfo.transform != null)
                            {
                                LookAt();
                                lineRenderer.enabled = true;
                                lineRenderer.SetPosition(0, transform.position);
                                lineRenderer.SetPosition(1, hitInfo.transform.position);
                                Destroy(hitInfo.transform.gameObject);
                            }
                        }
                    }
                }
            }
            yield return new WaitForSeconds(defendCycle);
            lineRenderer.enabled = false;
        }
    }
}