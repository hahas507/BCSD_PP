using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        float height = Camera.main.orthographicSize;
        float width = Camera.main.aspect * height;
        Vector3 screenEdge = new Vector3(width, 0, height);
        Debug.DrawLine(transform.position, transform.position + screenEdge, Color.red);

        Debug.Log(width + ", " + height);
    }
}