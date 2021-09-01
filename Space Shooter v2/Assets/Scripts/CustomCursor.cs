using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    private Vector3 targetPos;
    private float size;
    [SerializeField] private float resizeTo;//���콺 Ŀ�� ũ��

    [SerializeField] private float maxDistance;//���콺 Ŀ���� �÷��̾ �Ѱ� �Ÿ�

    [SerializeField] private Transform playerPosition; //�÷��̾� ��ġ

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