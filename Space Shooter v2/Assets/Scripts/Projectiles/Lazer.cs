using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool isHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isHit)
        {
            if (other.transform.tag == "Jako" || other.transform.tag == "Rock")
            {
                isHit = true;
                other.GetComponent<Status>().GetDamage(damage);
            }
            else if (other.transform.tag == "Player")
            {
                return;
            }
        }
    }
}