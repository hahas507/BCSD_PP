using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private float size;
    [SerializeField] private float resizeTo;
    private Transform targetPosition;
    private Vector3 myPosition; private Vector3 tarPosition;
    private Vector3 lookDirection;

    private void Awake()
    {
        targetPosition = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        LookAt();
        Resize();
    }

    private void Resize()
    {
        size = Camera.main.orthographicSize;
        transform.localScale = Vector3.one * size * resizeTo;
    }

    private float GetDegree(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.z - _from.z, _to.x - _from.x) * 180 / Mathf.PI;
    }

    private void LookAt()
    {
        float myX = transform.position.x; //Horizontal
        float myZ = transform.position.z; //Vertical

        float tarX = targetPosition.position.x;
        float tarZ = targetPosition.position.z;

        myPosition = new Vector3(myX, 0f, myZ);
        tarPosition = new Vector3(tarX, 0f, tarZ);

        lookDirection = new Vector3(0f, GetDegree(myPosition, tarPosition), 0f);

        transform.eulerAngles = -lookDirection;
    }
}