using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float force;
    public void Fire()
    {
        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * force);
    }
}
