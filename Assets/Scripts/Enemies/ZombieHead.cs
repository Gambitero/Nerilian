using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    void OnTriggerEnter(Collider obj){        
        if(obj.CompareTag("PlayerFeet")){
            gameObject.transform.parent.gameObject.GetComponent<ZombieController>().GetHit();
        }        
    }
}
