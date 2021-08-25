using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLight : MonoBehaviour
{
    [SerializeField] private float lightSpinSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up * lightSpinSpeed * Time.deltaTime);
    }
}