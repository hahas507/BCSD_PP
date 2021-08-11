using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // character movement
    [SerializeField] private float playerSpeed;

    [SerializeField] private float boostSpeed;
    private float applySpeed;

    [SerializeField] private GameObject bulletPrefab;

    private Rigidbody myRig;

    private void Start()
    {
        myRig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move(); // Character movement & boost
        Shoot();
    }

    private void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 playerMove = new Vector3(xInput, 0f, zInput);
        applySpeed = playerSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            applySpeed = boostSpeed;
        }

        myRig.AddForce(playerMove * applySpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void Shoot()
    {
        if (Input.GetButton("Fire1"))
        {
            //var clone = Instantiate(bulletPrefab, );
        }
    }
}