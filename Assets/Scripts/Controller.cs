using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterController controller;
    private float speed = 12f;
    private float gravity = -9.81f*9;
    private float jumpHeight = 2.4f;

    public bool flagCamera = false;
    public bool flagTurn = false;

    public int turnDir = 0;
    public float angle = 270f;
    public float rotationDir = -0.25f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    public bool isGrounded;
    

    void OnTriggerEnter(Collider obj){
        if (obj.gameObject.CompareTag("Turnpoint")){
            flagTurn = true;
            gameObject.transform.parent = obj.gameObject.transform;
            Debug.Log("papi chulo");
        }
    }

    void OnTriggerExit(Collider obj){
        if (obj.gameObject.CompareTag("Turnpoint")){
            flagTurn = false;
            gameObject.transform.parent = null;
            Debug.Log("papi chulo adios");
        }
    }

    float standardAngle(float angle){
        if(angle<0)
            return 360f+angle;
        return angle;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * x;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonUp("CameraRotation") && flagTurn)
        {
            flagCamera = true;

            switch(turnDir){
                case 0:
                    angle = 0f; //norte
                    rotationDir = -1;
                    turnDir = 1;
                    break;
                case 1:
                    angle = 90f; //oeste
                    rotationDir = 1;
                    turnDir = 0;
                    break;
                case 2:
                    angle = 180f; //este
                    rotationDir = -1;
                    turnDir = 3;
                    break;
                case 3:
                    angle = 270f; //sur
                    rotationDir = 1;
                    turnDir = 0;
                    break;

            }
        }

        if (flagCamera)
        {
            //Time.deltaTime * -50f
            controller.gameObject.transform.Rotate(0f, 0.25f * rotationDir, 0f);            
            if (controller.gameObject.transform.rotation.eulerAngles.y <= standardAngle(angle + 90f) * rotationDir){
                flagCamera = false;                
            }
        }
    }
}
