using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject inventory;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.SetParent(inventory.transform);
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation= Quaternion.Euler(0,0,0);
            transform.localPosition = new Vector3(0, 0, 0);
            GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;//No quiero sobra en el canvas
            other.gameObject.GetComponent<Player>().SetPotion();
        }
    }
}
