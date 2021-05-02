using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherTrig : MonoBehaviour
{    
    public SmasherBehavior behavior;

    void OnTriggerEnter(Collider obj){
        //this.gameObject.
        if(!behavior.trig && (obj.gameObject.transform.CompareTag("Player") || obj.gameObject.transform.parent.CompareTag("Player"))){                        
            behavior.count = 0f;
            behavior.moveSpeed = Mathf.Abs(behavior.moveSpeed);
            behavior.trig = true;
        }        
    }    
}
