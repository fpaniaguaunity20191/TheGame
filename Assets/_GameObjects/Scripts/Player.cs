﻿using System.Collections;
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
    private AudioSource audioSource;
    private bool hasPotion = false;
    //METHODS
    private void Awake()
    {
        myRB = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        LoadPosition();
    }
    private void Update()
    {
        if (estado == Estado.Shooting)
        {
            return;
        }
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetButton("Button 4"))
        {
            RunForward();
        } else
        {
            if (estado != Estado.Ilde)
            {
                StopRun();
            }
        }
        if (estado != Estado.Shooting && (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)))
        {
            StartFire();
        }
    }
    private void FixedUpdate()
    {
        if (estado == Estado.Shooting)
        {
            return;
        }
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
        audioSource.Stop();
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
            audioSource.Play();
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
    public void SetPotion()
    {
        hasPotion = true;
    }
    public bool HasPotion()
    {
        return hasPotion;
    }
    public void LoadPosition()
    {
        float x = PlayerPrefs.GetFloat("xPlayer", -29.33f);
        float y = PlayerPrefs.GetFloat("yPlayer", 10);
        float z = PlayerPrefs.GetFloat("zPlayer", -44.21f);
        transform.position = new Vector3(x, y, z);
    }
}
