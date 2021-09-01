using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHolder : MonoBehaviour
{
    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    private GameObject playerPosition;
    private Renderer rdr;
    [SerializeField] private LayerMask indicatorLayer;

    private void Awake()
    {
        rdr = GetComponent<Renderer>();
        playerPosition = GameObject.Find("Player");
    }

    private void Update()
    {
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