using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Status
{
    [SerializeField] private Transform targetPosition; //Look at the cursor.
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private float boostSpeed;
    [SerializeField] private float playerSpeed;
    private float applySpeed;

    private float timer;
    [SerializeField] private Transform shootingHand;
    private Vector3 armAngle;
    private Vector3 randomAngle;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireAngle;

    [SerializeField] private int shotgunBulletCount;[Tooltip("한번에 발사할 총알 수")]
    [SerializeField] private float shotgunFireRate;

    private RaycastHit hitInfo;
    [SerializeField] private float lazerMaxDistance;
    [SerializeField] private float lazerDamage;
    [SerializeField] private float maxLazerGauge; private float currentLazerGauge;

    private Vector3 myPosition; private Vector3 tarPosition;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject boostEffectPrefab;
    [SerializeField] private GameObject LazerEffectPrefab;

    [SerializeField] private float boostStartSpeed;[Tooltip("부스터를 발동할 수 있는 최소 속력")]
    [SerializeField] private int maxBoosterCount; private int currentBoosterLeft;

    private bool isBoosterRecovering = false;
    private bool canLazerRecover = false;
    private bool isLazerOnFire = false;

    [SerializeField] private float immortalTime; private float immortalTimer; //무적 타이머
    private bool isAttacked = false;

    [SerializeField] private GameObject UI;

    private Rigidbody myRig;
    private Animator myAnim;
    [SerializeField] private CapsuleCollider capCol;
    private PlayerController thePlayer;

    //private Sabor theSabor;
    //private bool isMeleeAttack;

    #region Status

    public float CURRENTHP
    {
        get { return currentHP; }
        private set { currentHP = value; }
    }

    public float BEAMGAUGE
    {
        get { return currentLazerGauge; }
        private set { currentLazerGauge = value; }
    }

    public int BOOSTERGAUGE
    {
        get { return currentBoosterLeft; }
        private set { currentBoosterLeft = value; }
    }

    #endregion Status

    protected override void Awake()
    {
        base.Awake();
        currentBoosterLeft = maxBoosterCount;
        currentLazerGauge = maxLazerGauge;
    }

    private void Start()
    {
        thePlayer = GetComponent<PlayerController>();
        myRig = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        capCol = GetComponent<CapsuleCollider>();
        //theSabor = FindObjectOfType<Sabor>();
    }

    private void FixedUpdate()
    {
        LookAt();
        TryMove(); // Character movement & boost
        DetectWeaponSelect();
        UIfollow();
        LazerRecharge();
    }

    public enum WEAPONS
    {
        RIFLE,
        SHOTGUN,
        LAZER,
        EMPTY,
    }

    public WEAPONS weapon;

    private void DetectWeaponSelect()
    {
        int currentWeapon = (int)weapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    //Missile();
        //}
        //else if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    //MeleeAttack();
        //}
        weapon = (WEAPONS)currentWeapon;

        switch (weapon)
        {
            case WEAPONS.RIFLE:
                ShootRifle();
                break;

            case WEAPONS.SHOTGUN:
                ShootShotgun();
                break;

            case WEAPONS.LAZER:
                ShootLazer();
                break;

            case WEAPONS.EMPTY:
                break;

            default:
                break;
        }
    }

    public override void GetDamage(float _damage)
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

        myPosition = new Vector3(myX, 0f, myZ);
        tarPosition = new Vector3(tarX, 0f, tarZ);

        armAngle = new Vector3(0f, GetDegree(myPosition, tarPosition), 0f);

        if (armAngle.y > 90 || armAngle.y < -90)
        {
            armAngle.x += 180;
        }

        transform.eulerAngles = -armAngle;
    }

    #region Move+Booster

    private void TryMove()
    {
        applySpeed = playerSpeed;
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 playerMove = new Vector3(xInput, 0f, zInput);

        float currentMoveSpeedX = myRig.velocity.x;
        float currentMoveSpeedY = myRig.velocity.z;

        if (Input.GetKeyDown(KeyCode.Space) && currentBoosterLeft > 0)
        {
            applySpeed = boostSpeed;

            myRig.AddForce(playerMove * applySpeed, ForceMode.VelocityChange);
            if (Mathf.Abs(currentMoveSpeedX) > boostStartSpeed || Mathf.Abs(currentMoveSpeedY) > boostStartSpeed)
            {
                currentBoosterLeft -= 1;
                var clone = Instantiate(boostEffectPrefab, transform.position, Quaternion.LookRotation(-transform.right));
                Destroy(clone, 3f);

                myAnim.SetTrigger("Booster");
                if (!isBoosterRecovering)
                {
                    isBoosterRecovering = true;
                    StartCoroutine(BoosterRecoverCoroutine());
                }
            }
        }

        myRig.AddForce(playerMove * applySpeed, ForceMode.VelocityChange);
    }

    private IEnumerator BoosterRecoverCoroutine()
    {
        YieldInstruction wait = new WaitForSeconds(1.75f);
        while (isBoosterRecovering)
        {
            yield return wait; //n초 쉬고

            currentBoosterLeft += 1;
            if (currentBoosterLeft >= maxBoosterCount)
            {
                currentBoosterLeft = maxBoosterCount;
                isBoosterRecovering = false;
            }
        }
    }

    #endregion Move+Booster

    //RIFLE 발사
    private void ShootRifle()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                isLazerOnFire = false;
                myAnim.SetTrigger("Attack_Gun");
                randomAngle = new Vector3(0f, UnityEngine.Random.Range(GetDegree(myPosition, tarPosition) - fireAngle, GetDegree(myPosition, tarPosition) + fireAngle), 0f);
                var clone = Instantiate(bulletPrefab, shootingHand.position + (transform.forward * 2.2f), Quaternion.Euler(-randomAngle));
                Destroy(clone, 3f);
                timer = 0f;
            }
        }
    }

    private void ShootShotgun()
    {
        timer += Time.deltaTime;
        if (timer >= shotgunFireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                isLazerOnFire = false;
                myAnim.SetTrigger("Attack_Gun");

                for (int i = 0; i < shotgunBulletCount; i++)
                {
                    randomAngle = new Vector3(0f, UnityEngine.Random.Range(GetDegree(myPosition, tarPosition) - fireAngle * 2, GetDegree(myPosition, tarPosition) + fireAngle * 2), 0f);
                    var clone = Instantiate(bulletPrefab, shootingHand.position + (transform.forward * 2.2f), Quaternion.Euler(-randomAngle));
                    Destroy(clone, 1f);
                }

                timer = 0f;
            }
        }
    }

    #region Lazer

    private void ShootLazer()
    {
        isLazerOnFire = false;
        if (Input.GetButton("Fire1") && currentLazerGauge > 0)
        {
            isLazerOnFire = true;
            canLazerRecover = false;
            currentLazerGauge -= .3f;
            myAnim.SetTrigger("Attack_Gun");
            if (Physics.Raycast(shootingHand.transform.position, shootingHand.transform.forward, out hitInfo, lazerMaxDistance, attackLayer))
            {
                Vector3 lazerAngle = new Vector3(0f, GetDegree(myPosition, hitInfo.transform.position), 0f);
                GameObject clone = Instantiate(LazerEffectPrefab, hitInfo.point, Quaternion.Euler(-lazerAngle));
                hitInfo.transform.gameObject.GetComponent<Status>().GetDamage(lazerDamage);
                Destroy(clone, 1.1f);
            }
        }
    }

    private void LazerRecharge()
    {
        if (currentLazerGauge < maxLazerGauge && !isLazerOnFire && !canLazerRecover)
        {
            canLazerRecover = true;
            StartCoroutine(LazerGaugeRecoverCoroutine());
        }
    }

    private IEnumerator LazerGaugeRecoverCoroutine()
    {
        yield return new WaitForSeconds(2);

        while (canLazerRecover && !isLazerOnFire)
        {
            currentLazerGauge += 1f;
            if (currentLazerGauge >= maxLazerGauge)
            {
                canLazerRecover = false;
                currentLazerGauge = maxLazerGauge;
            }
            yield return new WaitForSeconds(.05f);
        }
    }

    #endregion Lazer

    // RED에게 피격시 잠시 무적
    private IEnumerator ImmortalFor(float _time)
    {
        YieldInstruction wait = new WaitForSeconds(_time);
        isAttacked = true;
        while (isAttacked)
        {
            capCol.enabled = false;
            immortalTimer += Time.deltaTime; //시간 측정
            if (immortalTimer > _time)
            {
                isAttacked = false;
                capCol.enabled = true;
                immortalTimer = 0;
                yield return wait;
            }
            yield return null;
        }
    }

    private void UIfollow()
    {
        Vector3 UIposition = Camera.main.WorldToScreenPoint(transform.position);
        UI.transform.position = UIposition;
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