using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorHolder : MonoBehaviour
{
    private GameObject[] Jako; // �� ���� ��� Jako
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
        //ũ�� ����
        size = Camera.main.orthographicSize;
        transform.localScale = Vector3.one * size * resizeTo;
    }
}