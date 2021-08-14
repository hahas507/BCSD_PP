using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCam : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private float camY;

    private void Start()
    {
    }

    private void Update()
    {
        float setCamPosX = playerPos.position.x;
        float setCamPosZ = playerPos.position.z;
        Vector3 setCamTo = new Vector3(setCamPosX, camY, setCamPosZ);

        transform.position = setCamTo;
    }
}