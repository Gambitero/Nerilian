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
    }

    void OnTriggerExit(Collider obj){
        if(obj.CompareTag("Player")){
            zcontroller.chase = false;
            zcontroller.stop = false;
            zcontroller.dir *= 0.25f/0.4f;
        }
    }

    void Start(){
        zcontroller = gameObject.GetComponentInParent<ZombieController>();
    }
}
