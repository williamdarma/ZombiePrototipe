using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMovement : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 12.5f;
    public Vector3 offset;



    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = desiredPosition;
    }
}
