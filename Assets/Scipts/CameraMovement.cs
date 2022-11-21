using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{


    [SerializeField] private float sens = 10f;
    [SerializeField] private Transform target;
    [SerializeField] private float dstFromTarget = 2f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    
    void Start()
    {
        
      
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        
        yaw += sens * Input.GetAxis("Mouse X");
        pitch -= sens * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        transform.position = target.position - transform.forward * dstFromTarget;
       
    }

   
}

