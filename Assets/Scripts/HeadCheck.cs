using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    public Controller playerController;
    void OnCollisionEnter(Collision obj){        
        if (obj.gameObject.CompareTag("Platform")){
            playerController.fallVelocity.y = -0.5f;            
        }
    }
}
