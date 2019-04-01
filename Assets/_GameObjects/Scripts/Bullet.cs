using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float TIME_TO_DESTROY = 5f;
    void Start()
    {
        Destroy(this.gameObject, TIME_TO_DESTROY);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Destroy();
            Destroy(this.gameObject);
        }
    }
}
