using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject prefabExplosion;
    public Transform explosionPoint;
    private Animator animator;
    private Rigidbody myRB;
    private const string ANIM_PARAM_WALKING = "Walking";
    private long timeToChangeState;
    private const float MAX_ROTATION = 45f;
    private const int MIN_TIME_TO_CHANGE = 5;
    private const int MAX_TIME_TO_CHANGE = 10;
    private const int POINTS = 10;
    private GameManager gameManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody>();
        timeToChangeState = Random.RandomRange(MIN_TIME_TO_CHANGE, MAX_TIME_TO_CHANGE);
    }
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating("ChangeState", timeToChangeState, timeToChangeState);
    }
    void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walking")
        {
            myRB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        } 
    }
    void ChangeState()
    {
        float yRotation;
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle")
        {
            yRotation = Random.Range(-MAX_ROTATION, MAX_ROTATION);
            transform.Rotate(0, yRotation, 0);
            animator.SetBool(ANIM_PARAM_WALKING, true);
        } else if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walking")
        {
            animator.SetBool(ANIM_PARAM_WALKING, false);
        }

    }
    public void Destroy()
    {
        gameManager.AddPoints(POINTS);
        Instantiate(prefabExplosion, explosionPoint.transform.position, explosionPoint.transform.rotation);
        Destroy(this.gameObject);
    }
}
