using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHolder : MonoBehaviour
{
    private GameObject[] Jako; // 씬 상의 모든 Jako
    public List<Transform> JakoPositions = new List<Transform>();
    private GameObject mainTarget;
    private float size;
    [SerializeField] private float resizeTo;

    private void Update()
    {
        FindTargets();
        Resize();
    }

    private void FindTargets()
    {
        mainTarget = GameObject.FindGameObjectWithTag("MainTarget");
        Jako = GameObject.FindGameObjectsWithTag("Jako");
    }

    private void Resize()
    {
        //크기 조정
        size = Camera.main.orthographicSize;
        transform.localScale = Vector3.one * size * resizeTo;
    }
}