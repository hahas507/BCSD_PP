using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabor : MonoBehaviour
{
    private CapsuleCollider saborCol;

    private void Start()
    {
        saborCol = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Rock")
        {
            other.GetComponent<Fracture>().FractureObject();
        }
    }

    public void ColliderOn(bool _attack)
    {
        saborCol.enabled = _attack;
    }
}