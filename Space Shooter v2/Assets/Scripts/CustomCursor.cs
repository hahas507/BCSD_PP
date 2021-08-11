using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    private Vector3 targetPos;
    private float size;
    [SerializeField] private float reSizeTo;

    private void Start()
    {
        //Cursor.visible = false;
    }

    private void Update()
    {
        size = Camera.main.orthographicSize;
        transform.localScale = Vector3.one * size * reSizeTo;
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = targetPos;
    }
}