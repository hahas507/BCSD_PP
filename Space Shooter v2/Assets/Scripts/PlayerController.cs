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
    [SerializeField] private float fireAngle;

    [SerializeField] private Transform targetPosition; //Look at the cursor.
    [SerializeField] private Transform shootingArm; //ÃÑÀ» ¹ß»çÇÏ´Â ÆÈ.
    [SerializeField] private Transform shootingHand;
    [SerializeField] private float maxShootingAngle;
    private Vector3 armAngle;
    private Vector3 randomAngle;

    private bool isMeleeAttack;

    private Rigidbody myRig;
    private Animator myAnim;
    private Sabor sabor;

    private void Start()
    {
        myRig = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        sabor = GetComponent<Sabor>();
    }

    private void Update()
    {
        Shoot();
        MeleeAttack();
    }

    private void FixedUpdate()
    {
        LookAt();
        Move(); // Character movement & boost
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

        armAngle = new Vector3(0f, GetDegree(myPosition, tarPosition), 0f);
        randomAngle = new Vector3(0f, UnityEngine.Random.Range(GetDegree(myPosition, tarPosition) - fireAngle, GetDegree(myPosition, tarPosition) + fireAngle), 0f);

        transform.eulerAngles = -armAngle;
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

        myRig.AddForce(playerMove * applySpeed);
    }

    private void Shoot()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                myAnim.SetTrigger("Attack_Gun");
                float randomAngleY = UnityEngine.Random.Range(armAngle.y - 1, armAngle.y + 1);
                var clone = Instantiate(bulletPrefab, shootingHand.position, Quaternion.Euler(-randomAngle));
                Destroy(clone, 3f);
                timer = 0f;
            }
        }
    }

    private void MeleeAttack()
    {
        isMeleeAttack = false;
        if (Input.GetButtonDown("Fire2"))
        {
            isMeleeAttack = true;

            myAnim.SetTrigger("Attack_Melee");
        }
    }
}