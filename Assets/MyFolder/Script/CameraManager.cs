using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool topDownMode = false;
    [SerializeField] Camera topdownCamera, thirdPersonCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeCameraSetting()
    {
        topDownMode = !topDownMode;

    }
}
