using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public enum PlayerMovement { Idle,  Walk, Shooting, Reload, };

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] float PlayerSpeed;
    [SerializeField] Camera TopDownCamera;
    [SerializeField] float rotateSpeed;
    [SerializeField] Animator PlayerAnimator;
    [SerializeField] GameObject MovementJoystick;
    [SerializeField] Joystick JS;
    [SerializeField] Joystick JS2;
    [SerializeField] PlayerMovement PM;
    [SerializeField] PlayerShooting PS;
    [SerializeField] PlayerBehaviour PB;


    // Start is called before the first frame update
    void Start()
    {
        PB = GetComponent<PlayerBehaviour>();
        PlayerSpeed = PB.PC.PlayerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
       // var h = Input.GetAxis("Horizontal");  
       // var v = Input.GetAxis("Vertical");
        // var h = CrossPlatformInputManager.GetAxis("Horizontal");
        // var v = CrossPlatformInputManager.GetAxis("Vertical");
        if (!PB.Alive)
        {
            return;
        }
        var h = JS.Horizontal;
        var v = JS.Vertical;
        var hr = JS2.Horizontal;
        var vr = JS2.Vertical;
        var targetVector = new Vector3(h, 0, v);
        var targetRotationVector = new Vector3(hr, 0, vr);
        var movementVector = moveTowardTarget(targetVector);
        //  rotateTowardMovementVector(movementVector);
        rotateTowardMovementVector(-targetRotationVector);
        if (movementVector.magnitude > 0.2)
        {
            PM = PlayerMovement.Walk;
        }
        else
        {
            PM = PlayerMovement.Idle;
        }
        if (targetRotationVector.magnitude >.8f)
        {
            playerShooting();
        }
        AnimatePlayer();
        //PlayerAnimator.SetFloat("PlayerActivity", movementVector.magnitude);
    }

    public void playerShooting()
    {
        if (PS.jumlahpelet<=0)
        {
            PM = PlayerMovement.Reload;
            Invoke("reloadBullet", 2.5f);
        }
        else
        {
            PM = PlayerMovement.Shooting;
        }
    }
    
    void reloadBullet()
    {
        PS.jumlahpelet = 24;
        PM = PlayerMovement.Walk;
    }

    void AnimatePlayer()
    {
        if (PM == PlayerMovement.Idle)
        {
            PlayerAnimator.SetInteger("PlayerMovement", 0);
        }
        else if (PM == PlayerMovement.Walk)
        {
            PlayerAnimator.SetInteger("PlayerMovement", 1);
        }
        else if (PM == PlayerMovement.Shooting)
        {
            PlayerAnimator.SetInteger("PlayerMovement", 2);
        }
        else if (PM == PlayerMovement.Reload)
        {
            PlayerAnimator.SetInteger("PlayerMovement", 3);
        }
    }

    private void rotateTowardMovementVector(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0)
        {
            return;
        }
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private Vector3 moveTowardTarget(Vector3 targetVector)
    {
        var speed = PlayerSpeed  * Time.deltaTime;
        targetVector = Quaternion.Euler(0, TopDownCamera.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }
}
