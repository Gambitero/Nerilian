﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterController controller;
    private float speed = 12f;
    private float gravity = -9.81f*9;
    private float jumpHeight = 2.4f;

    public bool flagCamera = false;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    public bool isGrounded;
    
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

        if(Input.GetButtonUp("CameraRotation"))
        {
            flagCamera = true;
        }

        if (flagCamera)
        {
            //Time.deltaTime * -50f
            controller.gameObject.transform.Rotate(0f, -.25f, 0f);            
            if (controller.gameObject.transform.rotation.eulerAngles.y <= 270f){
                flagCamera = false;
            }
        }
    }    
}
