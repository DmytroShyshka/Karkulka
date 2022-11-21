using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkspeed = 6f;
    [SerializeField] float runspeed = 10f;
    [SerializeField] float jumpforce = 6f;
    [SerializeField] float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    [SerializeField] float speedSmoothTime = 1f;
    float speedSmoothVelocity;
    float currentSpeed;
    



    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    private BoxCollider colider;
    private Rigidbody rb;
    private Transform cameraT;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
       
        float targetSpeed = walkspeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);




        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpforce, rb.velocity.z);
        }

       
    }

   
    private bool IsGrounded()
    {

        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
    
}
