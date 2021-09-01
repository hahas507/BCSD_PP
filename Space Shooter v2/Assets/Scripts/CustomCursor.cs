using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    private Vector3 targetPos;
    private float size;
    [SerializeField] private float resizeTo;//마우스 커서 크기

    [SerializeField] private float maxDistance;//마우스 커서와 플레이어간 한계 거리

    [SerializeField] private Transform playerPosition; //플레이어 위치

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        size = Camera.main.orthographicSize;
        transform.localScale = Vector3.one * size * resizeTo;
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float playerPosX = playerPosition.localPosition.x;
        float playerPosZ = playerPosition.localPosition.z;
        float mousePosX = targetPos.x;
        float mousePosY = targetPos.y;

        Vector3 playerPos = new Vector3(playerPosX, 0f, playerPosZ);
        Vector3 mousePos = new Vector3(mousePosX, 0f, mousePosY);

        Vector3 offset = targetPos - playerPos;

        transform.position = playerPos + Vector3.ClampMagnitude(offset, maxDistance);
    }
}