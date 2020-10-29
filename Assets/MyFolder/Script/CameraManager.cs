using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool topDownMode = true;
    [SerializeField] Camera topdownCamera, thirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeCameraSetting()
    {
        topDownMode = !topDownMode;
        if (topDownMode)
        {
            topdownCamera.gameObject.SetActive(true);
            thirdPersonCamera.gameObject.SetActive(false);
        }
        else
        {
            thirdPersonCamera.gameObject.SetActive(true);
            topdownCamera.gameObject.SetActive(false);
        }

    }
}
