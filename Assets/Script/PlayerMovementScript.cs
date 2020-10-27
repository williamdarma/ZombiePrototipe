using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] float PlayerSpeed;
    [SerializeField] Camera TopDownCamera;
    [SerializeField] float rotateSpeed;
    [SerializeField] Animator PlayerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //var h = Input.GetAxis("Horizontal");  
        //var v = Input.GetAxis("Vertical");
        var h = CrossPlatformInputManager.GetAxis("Horizontal");
        var v = CrossPlatformInputManager.GetAxis("Vertical");
        var targetVector = new Vector3(h, 0, v);
        var movementVector = moveTowardTarget(targetVector);
        rotateTowardMovementVector(movementVector);
        PlayerAnimator.SetFloat("MovementAnim",movementVector.magnitude);
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
