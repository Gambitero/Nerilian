using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public SawBehavior behavior;
    
    void OnCollisionExit(Collision obj){     
        if (obj.gameObject.CompareTag("Platform")){
            behavior.transform.LookAt(new Vector3(behavior.transform.localPosition.x, -behavior.transform.localPosition.y, behavior.transform.localPosition.z));
            behavior.dir *= -1;
        }
    }
}
