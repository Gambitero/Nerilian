using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool groundFlag = true;
    public Controller playerController;
    void Start()
    {
        playerController = Object.FindObjectOfType<Controller>();
        groundFlag = true;
    }
    
    void OnCollisionEnter(Collision obj){
        if (obj.gameObject.CompareTag("Platform")){            
            groundFlag = true;
            playerController.groundFlag = groundFlag;
        }
    }

    void OnCollisionExit(Collision obj){
        if (obj.gameObject.CompareTag("Platform")){            
            groundFlag = false;
            playerController.groundFlag = groundFlag;
        }
    }

    void OnCollisionStay(Collision obj){
        if (obj.gameObject.CompareTag("Platform")){            
            groundFlag = true;
            playerController.groundFlag = groundFlag;
        }
    }

}
