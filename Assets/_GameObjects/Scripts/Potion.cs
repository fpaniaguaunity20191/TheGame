using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Potion : EventTrigger
{
    public GameObject inventory;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            transform.SetParent(inventory.transform);
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation= Quaternion.Euler(0,0,0);
            transform.localPosition = new Vector3(0, 0, 0);
            GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;//No quiero sobra en el canvas
            other.gameObject.GetComponent<Player>().SetPotion();
            StorePosition(other.gameObject);
        }
    }
    private void StorePosition(GameObject player)
    {
        PlayerPrefs.SetFloat("xPlayer", player.transform.position.x);
        PlayerPrefs.SetFloat("yPlayer", player.transform.position.y);
        PlayerPrefs.SetFloat("zPlayer", player.transform.position.z);
        PlayerPrefs.Save();
    }

    public override void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDown called.");
    }
}
