using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float boostSpeed;
    [SerializeField] private float playerSpeed;
    private float applySpeed;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform fireHolder;
    private float timer;
    [SerializeField] private float fireRate;

    [SerializeField] private Transform targetPosition; //Look at the cursor.
    private Vector3 angle;

    private Rigidbody myRig;

    private void Start()
    {
        myRig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        LookAt();
        Move(); // Character movement & boost
        Shoot();
    }

    private float GetDegree(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.z - _from.z, _to.x - _from.x) * 180 / Mathf.PI;
    }

    private void LookAt()
    {
        float myX = transform.position.x; //Horizontal
        float myZ = transform.position.z; //Vertical

        float tarX = targetPosition.position.x;
        float tarZ = targetPosition.position.z;

        Vector3 myPosition = new Vector3(myX, 0f, myZ);
        Vector3 tarPosition = new Vector3(tarX, 0f, tarZ);

        angle = new Vector3(0f, GetDegree(myPosition, tarPosition), 0f);

        transform.localEulerAngles = -angle;
    }

    private void Move()
    {
        applySpeed = playerSpeed;
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 playerMove = new Vector3(xInput, 0f, zInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            applySpeed = boostSpeed;
        }

        myRig.AddForce(playerMove * applySpeed * Time.deltaTime, ForceMode.VelocityChange);
        Debug.Log(applySpeed);
    }

    private void Shoot()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                var clone = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(-angle));
                Destroy(clone, 3f);
                timer = 0f;
            }
        }
    }
}