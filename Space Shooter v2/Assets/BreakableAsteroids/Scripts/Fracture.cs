using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fracture : Status
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    public GameObject fractured;

    public override void GetDamage(int _damage)
    {
        base.GetDamage(_damage);

        if (currentHP <= 0)
        {
            FractureObject();
        }
    }

    private void FractureObject()
    {
        var fractures = Instantiate(fractured, transform.position, transform.rotation); //Spawn in the broken version
        fractures.transform.localScale = transform.localScale;
        Destroy(gameObject, 0.1f); //Destroy the object to stop it getting in the way
        Destroy(fractures, 3f);
    }
}