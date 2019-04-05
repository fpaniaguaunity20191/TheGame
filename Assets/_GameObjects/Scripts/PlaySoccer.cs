using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoccer : MonoBehaviour
{
    public float force;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        }
    }
}
