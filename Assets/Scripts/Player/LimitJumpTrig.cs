﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitJumpTrig : MonoBehaviour
{
    void OnTriggerEnter(Collider obj){
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Platform2") || obj.gameObject.CompareTag("Jumpable")){            
            Controller.limitJump = 0.7f;
        }
    }

    void OnTriggerExit(Collider obj){        
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Platform2") || obj.gameObject.CompareTag("Jumpable")){
            Controller.limitJump = 1f;
        }
    }
}