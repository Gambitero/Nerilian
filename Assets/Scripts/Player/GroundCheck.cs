using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script checkea si estás o no en el suelo, ya que si estás en el aire no deberías poder saltar nuevamente
public class GroundCheck : MonoBehaviour
{
    public bool groundFlag = true;
    Controller playerController;
    public int frameCounter = 0;
    
    void OnCollisionEnter(Collision obj){
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Platform2") || obj.gameObject.CompareTag("Jumpable")){            
            frameCounter = 0;
        }        
    }

    void OnCollisionExit(Collision obj){
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Platform2") || obj.gameObject.CompareTag("Jumpable")){                        
            groundFlag = false;
            playerController.groundFlag = groundFlag;            
        }
    }

    void OnCollisionStay(Collision obj){
        if (obj.gameObject.CompareTag("Platform") || obj.gameObject.CompareTag("Platform2") || obj.gameObject.CompareTag("Jumpable")){
            if (frameCounter >= 5){                
                if (playerController.jumping && playerController.fallVelocity.y < 0f) {                                    
                    playerController.resetFallVel = true;
                    playerController.animator.SetBool("Ground", true);
                }
                groundFlag = true;
                playerController.groundFlag = groundFlag;
                playerController.jumping = false;
            }
            else{
                frameCounter++;
            }
        }
    }

    void Start()
    {
        playerController = gameObject.GetComponentInParent<Controller>();
        groundFlag = true;
    }
}
