using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public bool saw = false;
    public bool zombie = false;
    public int turnBuffer = -1;    
    SawBehavior behavior;
    ZombieController zcontroller;

    void OnDisable(){
        turnBuffer = -1;
    }
    
    void OnCollisionExit(Collision obj){     
        if (obj.gameObject.CompareTag("Platform")){
            if(saw){
                behavior.transform.LookAt(new Vector3(behavior.transform.localPosition.x, -behavior.transform.localPosition.y, behavior.transform.localPosition.z));
                behavior.dir *= -1;
            }
            if(zombie){
                if (turnBuffer == 0){
                    if(!zcontroller.chase){
                        zcontroller.transform.Rotate(Vector3.up, 180);
                        zcontroller.dir *= -1;                        
                    }
                    else{
                        zcontroller.stop = true;
                    }
                    turnBuffer = -1;
                }
                else
                {
                    turnBuffer -= 1;
                }
            }
        }
    }

    void OnCollisionEnter(Collision obj){
        if (obj.gameObject.CompareTag("Platform")){
            if(zombie){
                turnBuffer += 1;
            }
        }
    }

    void Start(){
        if(zombie){
            zcontroller = gameObject.GetComponentInParent<ZombieController>();
        }
        else if (saw){
            behavior = gameObject.GetComponentInParent<SawBehavior>();
        }
    }
}
