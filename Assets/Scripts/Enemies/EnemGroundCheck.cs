using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemGroundCheck : MonoBehaviour
{
    public bool groundFlag = true;
    ZombieController enemController;
    
    void OnCollisionEnter(Collision obj){
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Platform2") || obj.gameObject.CompareTag("Jumpable")){
            if (enemController.health > 0){
                groundFlag = true;
            }
            //enemController.velocity.y = 0;       
            enemController.resetFallVel = true;
            //enemController.jumping = false;
            enemController.groundFlag = groundFlag;            
        }        
    }

    void OnCollisionExit(Collision obj){
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Platform2") || obj.gameObject.CompareTag("Jumpable")){            
            groundFlag = false;
            enemController.groundFlag = groundFlag;
        }
    }

    void OnCollisionStay(Collision obj){
        if (obj.gameObject.CompareTag("Platform")|| obj.gameObject.CompareTag("Platform2") || obj.gameObject.CompareTag("Jumpable")){
            if (enemController.health > 0){
                groundFlag = true;
            }
            enemController.groundFlag = groundFlag;
        }
    }

    void Start()
    {
        enemController = gameObject.GetComponentInParent<ZombieController>();
        groundFlag = true;
    }
}
