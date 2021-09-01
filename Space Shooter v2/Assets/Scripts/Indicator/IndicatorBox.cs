using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBox : MonoBehaviour
{
    private float width;
    private float height;
    private Vector3 size;
    private float camPositionY;
    private Vector3 setPosition;

    private void Update()
    {
        height = Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        size = new Vector3(1.8f * width, 1.8f * height, 100f);

        camPositionY = Camera.main.transform.position.y;
        setPosition = new Vector3(transform.position.x, -camPositionY / 30, transform.position.z);

        gameObject.transform.localScale = size;
        gameObject.transform.position = setPosition;
    }
}