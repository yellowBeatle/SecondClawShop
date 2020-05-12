using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverObjects : MonoBehaviour
{

    public Transform spawnPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Object" || other.transform.tag == "Player")
        {
            other.transform.position = spawnPos.position;
        }
    }
}
