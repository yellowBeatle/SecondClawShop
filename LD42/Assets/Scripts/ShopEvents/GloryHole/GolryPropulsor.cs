using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolryPropulsor : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        other.transform.Rotate(Random.insideUnitSphere * 360);

    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().velocity = transform.right * 12.5f;
    }
}
