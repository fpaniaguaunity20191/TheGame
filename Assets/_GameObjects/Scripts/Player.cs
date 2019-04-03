using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //PUBLIC
    public enum Estado { Ilde, Walking, Running, Shooting }
    public Estado estado = Estado.Ilde;
    public float speed;
    public float speedRunning;
    public float angularSpeed;
    public Gun gun;
    public GameManager gm;
    //PRIVATE
    private const string ANIM_PARAM_WALKING = "Walking";
    private const string ANIM_PARAM_RUNNING = "Running";
    private const string ANIM_PARAM_HORIZONTAL = "Horizontal";
    private const string ANIM_PARAM_VERTICAL = "Vertical";
    private const string ANIM_PARAM_SHOOTING = "Shooting";
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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Button 4"))
        {
            RunForward();
        } else
        {
            if (estado != Estado.Ilde)
            {
                StopRun();
            }
        }
        if (estado==Estado.Ilde && Input.GetButtonDown("Fire1"))
        {
            StartFire();
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
        transform.Rotate(0, x * angularSpeed, 0);
    }
    private void StartFire()
    {
        myAnimator.SetTrigger(ANIM_PARAM_SHOOTING);
        estado = Estado.Shooting;
    }
    public void Fire()
    {
        gun.Fire();
    }
    public void StopFire()
    {
        estado = Estado.Ilde;
    }
}
