using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour
{
    private Animator ani;
    private Rigidbody rig;

    [SerializeField] private float speed;
    [SerializeField] private float maxVelocity;
    [SerializeField] private LayerMask layerMask;
    private bool isWalking = false;
    private Vector3 playerMovement;
    private Menu theMenu;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        TryMove();
        DetectMenu();
    }

    private void TryMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        playerMovement = new Vector3(inputX, 0f, inputZ);
        playerMovement.Normalize();

        rig.AddForce(playerMovement * speed, ForceMode.VelocityChange);
        if (playerMovement != Vector3.zero)
        {
            isWalking = true;
            transform.forward = playerMovement;

            ani.SetBool("IsWalking", isWalking);
            if (rig.velocity.magnitude > maxVelocity)
            {
                rig.velocity = Vector3.ClampMagnitude(rig.velocity, maxVelocity);
            }
        }
        else if (playerMovement == Vector3.zero)
        {
            isWalking = false;
            rig.velocity = Vector3.zero;
            ani.SetBool("IsWalking", isWalking);
        }
    }

    private void DetectMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(rig.position + Vector3.up, transform.forward, out hitInfo, 10, layerMask))
            {
                if (hitInfo.collider.tag == "Menu")
                {
                    GameManager.isMenuOpen = true;
                }
            }
        }
    }
}