using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour
{
    private float size;
    [SerializeField] private float resizeTo;

    private void Update()
    {
        size = Camera.main.orthographicSize;
        transform.localScale = Vector3.one * size * resizeTo;
    }
}