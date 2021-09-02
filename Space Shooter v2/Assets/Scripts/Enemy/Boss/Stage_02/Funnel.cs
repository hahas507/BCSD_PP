using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funnel : MonoBehaviour
{
    [Range(0, 60)]
    [SerializeField] private float maximumAngle;

    [SerializeField] private float shakeCycle;

    [Range(0, 50)]
    [SerializeField] private float randomPositionX, randomPositionZ;

    [SerializeField] private float repositionCycle;
    [SerializeField] private float repositionSpeed;

    private Vector3 randomPosition;

    private bool isLerping;

    private void Start()
    {
        StartCoroutine(ShakeHead());
        StartCoroutine(RandomPositioning());
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
                    Debug.Log("true");
                    isLerping = false;
                }
                yield return null;
            }
        }
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