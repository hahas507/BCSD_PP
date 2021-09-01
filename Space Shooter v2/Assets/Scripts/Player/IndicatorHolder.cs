using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHolder : MonoBehaviour
{
    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    [SerializeField] private GameObject playerPosition;
    private Renderer rdr;
    [SerializeField] private LayerMask indicatorLayer;

    private void Awake()
    {
        rdr = GetComponent<Renderer>();
    }

    private void Update()
    {
        Debug.Log(rdr.isVisible);
        if (rdr.isVisible == false)
        {
            if (indicator.activeSelf == false)
            {
                indicator.SetActive(true);
            }

            Vector3 direction = (playerPosition.transform.position - transform.position);
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(transform.position, direction);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, indicatorLayer))
            {
                Debug.Log(hitInfo.point);
                indicator.transform.position = hitInfo.point;
            }
        }
        else
        {
            if (indicator.activeSelf == true)
            {
                indicator.SetActive(false);
            }
        }
    }
}