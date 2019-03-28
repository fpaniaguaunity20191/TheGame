using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Estado { Ilde, Walking, Running }
    public Estado estado = Estado.Ilde;
    private const string ANIM_PARAM_WALKING = "Walking";
    private const string ANIM_PARAM_RUNNING = "Running";
    private const string ANIM_PARAM_HORIZONTAL = "Horizontal";
    private const string ANIM_PARAM_VERTICAL = "Vertical";
    public float speed;
    public float speedRunning;
    private float y;
    private float x;
    private Rigidbody myRB;
    private Animator myAnimator;
    private void Awake()
    {
        myRB = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RunForward();
        } else
        {
            if (estado != Estado.Ilde)
            {
                StopRun();
            }
        }
    }
    private void FixedUpdate()
    {
        if (y > 0)
        {
            MoveForward();
        }
        else
        {
            StopMoving();
        }
        if (x != 0)
        {
            Turn();
        }
    }
    private void StopMoving()
    {
        myAnimator.SetBool(ANIM_PARAM_WALKING, false);
        myAnimator.SetBool(ANIM_PARAM_RUNNING, false);
        estado = Estado.Ilde;
    }
    private void MoveForward()
    {
        if (estado==Estado.Ilde)
        {
            myAnimator.SetBool(ANIM_PARAM_WALKING, true);
            estado = Estado.Walking;
        } else if (estado==Estado.Walking)
        {
            myRB.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        }
        else if (estado==Estado.Running)
        {
            myRB.MovePosition(transform.position + transform.forward * Time.deltaTime * speedRunning);
            myAnimator.SetFloat(ANIM_PARAM_HORIZONTAL, x);
            myAnimator.SetFloat(ANIM_PARAM_VERTICAL, y);
        } 
    }
    private void RunForward()
    {
        if (estado==Estado.Walking)
        {
            myAnimator.SetBool(ANIM_PARAM_RUNNING, true);
            myAnimator.SetFloat(ANIM_PARAM_HORIZONTAL, x);
            myAnimator.SetFloat(ANIM_PARAM_VERTICAL, y);
            estado = Estado.Running;
        } 
    }
    private void StopRun()
    {
        myAnimator.SetBool(ANIM_PARAM_RUNNING, false);
        estado = Estado.Walking;
    }
    private void Turn()
    {
        transform.Rotate(0, x, 0);
    }
}
