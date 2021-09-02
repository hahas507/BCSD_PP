using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    [Range(0, 60)]
    [SerializeField] private float maximumAngle;

    [SerializeField] private float shakeCycle;
    private Vector3 randomPosition;

    private void Start()
    {
    }

    private void Update()
    {
        LookAtRandomAngle();
    }

    private void LookAtRandomAngle()
    {
        StopCoroutine(ShakeHead());
        StartCoroutine(ShakeHead());
    }

    private IEnumerator ShakeHead()
    {
        while (true)
        {
            transform.localEulerAngles = new Vector3(0f, Mathf.Clamp(UnityEngine.Random.Range(-maximumAngle, maximumAngle), -maximumAngle, maximumAngle), 0f);
            yield return new WaitForSeconds(shakeCycle);
        }
    }
}