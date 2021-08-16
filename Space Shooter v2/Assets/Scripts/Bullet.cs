using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool isThisPlayer;

    [SerializeField] private int bulletDamage;

    private Status theStatus;

    private void Awake()
    {
        theStatus = FindObjectOfType<Status>();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isThisPlayer)
        {
            if (other.transform.tag != "Player")
            {
                other.GetComponent<Status>().GetDamage(bulletDamage);
                Destroy(gameObject);
            }
        }

        if (!isThisPlayer)
        {
            if (other.transform.tag == "Player")
            {
                other.GetComponent<Status>().GetDamage(bulletDamage);
            }
        }
    }
}