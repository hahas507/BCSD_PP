using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    private void Update()
    {
        transform.Translate(Vector3.right * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player")
        {
            //damage;
        }

        if (other.transform.tag == "Rock")
        {
            Destroy(gameObject);
            other.GetComponent<Fracture>().FractureObject();
        }
        Debug.Log(other);
    }
}