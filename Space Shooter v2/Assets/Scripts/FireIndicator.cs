using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireIndicator : MonoBehaviour
{
    [SerializeField] private Transform[] indicators;

    public void FireAngle(float _angle)
    {
        indicators[0].localEulerAngles = new Vector3(0f, _angle, 0f);
        indicators[1].localEulerAngles = new Vector3(0f, -_angle, 0f);
    }
}