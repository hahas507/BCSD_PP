using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    private Vector3 targetPos;
    private float size;
    [SerializeField] private float reSizeTo;//���콺 Ŀ�� ũ��

    [SerializeField] private float maxDistance;//���콺 Ŀ���� �÷��̾ �Ѱ� �Ÿ�

    [SerializeField] private Transform playerDis;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 playerPos = playerDis.localPosition;

        size = Camera.main.orthographicSize;
        transform.localScale = Vector3.one * size * reSizeTo;
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 offset = targetPos - playerPos;

        //transform.position = targetPos;

        transform.position = playerPos + Vector3.ClampMagnitude(offset, maxDistance);
    }
}