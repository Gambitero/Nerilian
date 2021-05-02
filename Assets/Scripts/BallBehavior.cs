﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dir = -1;
    public CharacterController controller;
    
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();        
    }

    void Update()
    {   
        controller.Move(new Vector3(moveSpeed * dir, 0, 0) * Time.deltaTime);     
    }

    void OnTriggerEnter(Collider obj){        
        if(obj.CompareTag("Player")){
            obj.gameObject.GetComponent<Controller>().Die();
        }        
    }

    void OnTriggerExit(Collider obj){        
        if(obj.CompareTag("PatrolRange")){
            dir *= -1;
        }
    }
}
