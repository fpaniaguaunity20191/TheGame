using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float maxDistanceToPlayer;
    public float speed;
    private float sqrMaxDistanceToPlayer;
    private float sqrDistanceToPlayer;
    private GameObject player;
    private float yPos;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        yPos = transform.position.y;
    }
    private void Start()
    {
        sqrMaxDistanceToPlayer = maxDistanceToPlayer * maxDistanceToPlayer;
    }
    void Update()
    {
        transform.LookAt(player.transform);
        sqrDistanceToPlayer = GetDistance();
        if (sqrDistanceToPlayer > sqrMaxDistanceToPlayer)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (transform.position.y < yPos)
            {
                transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            }
        }
    }
    private float GetDistance()
    {
        float distance = Vector3.SqrMagnitude(player.transform.position - transform.position);
        return distance;
    }
}
