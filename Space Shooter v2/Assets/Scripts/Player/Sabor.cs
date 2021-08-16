using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Rock")
        {
            //other.GetComponent<Fracture>().GetDamage();
        }
    }
}