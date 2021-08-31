using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabor : MonoBehaviour
{
    [SerializeField] private float damage;
    private Status theStatus;

    private void Awake()
    {
        theStatus = FindObjectOfType<Status>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Jako" || other.transform.tag == "Rock" || other.transform.tag == "MainTarget" || other.transform.tag == "Parts")
        {
            other.GetComponent<Status>().GetDamage(damage);
        }
    }
}