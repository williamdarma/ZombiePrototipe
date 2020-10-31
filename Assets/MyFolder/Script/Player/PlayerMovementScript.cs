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
    [SerializeField] Joystick JSMovement;
    [SerializeField] Joystick JSRotate;
    public PlayerMovement PM;
    [SerializeField] PlayerShooting PS;
    [SerializeField] PlayerBehaviour PB;
    [SerializeField] CameraManager CM;

    public AudioSource walk;


    // Start is called before the first frame update
    void Start()
    {
        PB = GetComponent<PlayerBehaviour>();
        CM = GameObject.FindObjectOfType<CameraManager>();
        PlayerSpeed = PB.playerSpeed;
        JSMovement = GameObject.Find("MovementJoystick").GetComponent<Joystick>();
        JSRotate = GameObject.Find("RotationJoystick").GetComponent<Joystick>();
        TopDownCamera = GameObject.Find("TopdownCamera").GetComponent<Camera>();
        TopDownCamera.GetComponent<CameraFollowMovement>().FindPlayer();
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
        if (CM.topDownMode)
        {
            var h = JSMovement.Horizontal;
            var v = JSMovement.Vertical;
            //var h = Input.GetAxis("Horizontal");  
           // var v = Input.GetAxis("Vertical");
            var hr = JSRotate.Horizontal;
            var vr = JSRotate.Vertical;
            var targetVector = new Vector3(h, 0, v);
            var targetRotationVector = new Vector3(hr, 0, vr);
            var movementVector = moveTowardTarget(targetVector);
            //  rotateTowardMovementVector(movementVector);
            rotateTowardMovementVector(-targetRotationVector);
            if (movementVector.magnitude > 0.2)
            {
                PM = PlayerMovement.Walk;
                walk.gameObject.SetActive(true);
            }
            else
            {
                PM = PlayerMovement.Idle;
                walk.gameObject.SetActive(false);
            }
            if (targetRotationVector.magnitude >= .9f)
            {
                playerShooting();
            }

            //PlayerAnimator.SetFloat("PlayerActivity", movementVector.magnitude);
        }
        else
        {
            var h = JSMovement.Horizontal;
            var v = JSMovement.Vertical;
            var targetVector = new Vector3(h, 0, v);
            var movementVector = moveTowardTarget(-targetVector);
        }
        AnimatePlayer();

    }

    public void playerShooting()
    {
        if (PS.jumlahpelet<=0)
        {
            PM = PlayerMovement.Reload;
            PB.Reload.gameObject.SetActive(true);
            PS.reload.Play();
            Invoke("reloadBullet", 2.5f);
        }
        else
        {
            PM = PlayerMovement.Shooting;
        }
    }
    
    void reloadBullet()
    {
        PB.Reload.gameObject.SetActive(false);
        PS.Reload();
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
