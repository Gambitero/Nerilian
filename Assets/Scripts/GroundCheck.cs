﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script checkea si estás o no en el suelo, ya que si estás en el aire no deberías poder saltar nuevamente
public class GroundCheck : MonoBehaviour
{
    public bool groundFlag = true;
    public Controller playerController;
    
    void OnCollisionEnter(Collision obj){
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Jumpable")){            
            groundFlag = true;
            //playerController.velocity.y = 0;       
            playerController.resetFallVel = true;
            playerController.jumping = false;
            playerController.groundFlag = groundFlag;            
        }        
    }

    void OnCollisionExit(Collision obj){
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Jumpable")){            
            groundFlag = false;
            playerController.groundFlag = groundFlag;
        }
    }

    void OnCollisionStay(Collision obj){
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Jumpable")){
            groundFlag = true;
            playerController.groundFlag = groundFlag;
        }
    }

    void Start()
    {
        playerController = Object.FindObjectOfType<Controller>();
        groundFlag = true;
    }
}
