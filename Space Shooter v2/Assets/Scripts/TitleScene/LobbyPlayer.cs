using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour
{
    private Animator ani;

    [SerializeField] private float speed;
    private bool isWalking = false;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        TryMove();
    }

    private void TryMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(inputX, 0f, inputZ);
        playerMovement.Normalize();

        transform.Translate(playerMovement * speed * Time.deltaTime, Space.World);

        if (playerMovement != Vector3.zero)
        {
            isWalking = true;
            transform.forward = playerMovement;
            ani.SetBool("IsWalking", isWalking);
        }
        else if (playerMovement == Vector3.zero)
        {
            isWalking = false;
            ani.SetBool("IsWalking", isWalking);
        }
    }
}