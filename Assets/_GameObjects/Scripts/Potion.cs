using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject inventory;
    bool inInventory = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inInventory = true;
            transform.SetParent(inventory.transform);
            transform.rotation = Quaternion.Euler(0,0,0);
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(0, 0, 0);
        }
    }
    private void Update()
    {
        if (inInventory)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
