using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject playerPosition; //Look at Player
    private Vector3 myPosition;
    private Vector3 tarPosition;
    private Vector3 lookAngle;

    private void Start()
    {
        playerPosition = GameObject.Find("Player");
    }

    private void Update()
    {
        Debug.Log("playerPosition" + playerPosition);
        Debug.Log("myPosition" + myPosition);
        LookAt();
    }

    private float GetDegree(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.z - _from.z, _to.x - _from.x) * 180 / Mathf.PI;
    }

    private void LookAt()
    {
        float myX = transform.position.x; //Horizontal
        float myZ = transform.position.z; //Vertical

        float tarX = playerPosition.transform.position.x;
        float tarZ = playerPosition.transform.position.z;

        myPosition = new Vector3(myX, 0f, myZ);
        tarPosition = new Vector3(tarX, 0f, tarZ);

        lookAngle = new Vector3(0f, GetDegree(myPosition, tarPosition), 0f);

        transform.eulerAngles = -lookAngle;
    }
}