using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    ZombieController zcontroller;    
    void OnTriggerEnter(Collider obj){
        if(obj.CompareTag("Player")){
            zcontroller.chase = true;
        }

        if(obj.CompareTag("Enemy") || obj.CompareTag("Platform")){
            zcontroller.transform.Rotate(Vector3.up, 180);
            zcontroller.dir *= -1;
        }
    }

    void OnTriggerExit(Collider obj){
        if(obj.CompareTag("Player")){
            zcontroller.chase = false;            
            zcontroller.dir *= 0.25f/0.4f;
            if (zcontroller.stop){
                zcontroller.stop = false;
                zcontroller.dir *= -1;
                zcontroller.transform.Rotate(Vector3.up, 180);                
            }
        }
    }

    void Start(){
        zcontroller = gameObject.GetComponentInParent<ZombieController>();        
    }
}
