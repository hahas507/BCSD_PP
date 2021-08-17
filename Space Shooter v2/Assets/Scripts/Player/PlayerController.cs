using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Status
{
    [SerializeField] private float boostSpeed;
    [SerializeField] private float playerSpeed;
    private float applySpeed;

    [SerializeField] private GameObject bulletPrefab;
    private float timer;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireAngle;

    [SerializeField] private Transform targetPosition; //Look at the cursor.
    [SerializeField] private Transform shootingHand;
    private Vector3 armAngle;
    private Vector3 randomAngle;

    [SerializeField] private GameObject boostEffectPrefab;
    [SerializeField] private float boostStartSpeed;

    [SerializeField] private float immortalTime;
    private float immortalTimer;

    //private bool isMeleeAttack;
    private bool isAttacked = false;

    private Rigidbody myRig;
    private Animator myAnim;
    [SerializeField] private BoxCollider boxCol;

    private Sabor theSabor;
    private FireIndicator theFireIndicator;

    private PlayerController thePlayer;

    private void Start()
    {
        thePlayer = GetComponent<PlayerController>();
        myRig = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider>();
        theSabor = FindObjectOfType<Sabor>();
        theFireIndicator = FindObjectOfType<FireIndicator>();
    }

    private void Update()
    {
        Shoot();
        //MeleeAttack();
    }

    private void FixedUpdate()
    {
        LookAt();
        TryMove(); // Character movement & boost
    }

    public override void GetDamage(int _damage)
    {
        if (!isDead)
        {
            base.GetDamage(_damage);

            StartCoroutine(ImmortalFor(immortalTime));
            if (currentHP <= 0 && !isDead)
            {
                thePlayer.enabled = false;
                isDead = true;
            }
        }
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

        if (armAngle.y > 90 || armAngle.y < -90)
        {
            armAngle.x += 180;
        }

        randomAngle = new Vector3(0f, UnityEngine.Random.Range(GetDegree(myPosition, tarPosition) - fireAngle, GetDegree(myPosition, tarPosition) + fireAngle), 0f);

        transform.eulerAngles = -armAngle;
    }

    private void TryMove()
    {
        applySpeed = playerSpeed;
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 playerMove = new Vector3(xInput, 0f, zInput);

        float currentMoveSpeedX = myRig.velocity.x;
        float currentMoveSpeedY = myRig.velocity.z;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            applySpeed = boostSpeed;
            myRig.AddForce(playerMove * applySpeed, ForceMode.VelocityChange);
            if (Mathf.Abs(currentMoveSpeedX) > boostStartSpeed || Mathf.Abs(currentMoveSpeedY) > boostStartSpeed)
            {
                var clone = Instantiate(boostEffectPrefab, transform.position, Quaternion.LookRotation(-transform.right));
                Destroy(clone, 3f);

                myAnim.SetTrigger("Booster");
            }
        }

        myRig.AddForce(playerMove * applySpeed, ForceMode.VelocityChange);
    }

    private void Shoot()
    {
        //theFireIndicator.FireAngle(fireAngle);

        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                myAnim.SetTrigger("Attack_Gun");
                var clone = Instantiate(bulletPrefab, shootingHand.position, Quaternion.Euler(-randomAngle));
                Destroy(clone, 3f);
                timer = 0f;
            }
        }
    }

    private IEnumerator ImmortalFor(float _time)
    {
        YieldInstruction wait = new WaitForSeconds(_time);
        isAttacked = true;
        while (isAttacked)
        {
            boxCol.enabled = false;
            immortalTimer += Time.deltaTime; //시간 측정
            if (immortalTimer > _time)
            {
                isAttacked = false;
                boxCol.enabled = true;
                immortalTimer = 0;
                yield return wait;
            }
            yield return null;
        }
    }

    //private void MeleeAttack()
    //{
    //    isMeleeAttack = false;
    //    if (Input.GetButtonDown("Fire2"))
    //    {
    //        isMeleeAttack = true;

    //        myAnim.SetTrigger("Attack_Melee");
    //    }
    //}
}