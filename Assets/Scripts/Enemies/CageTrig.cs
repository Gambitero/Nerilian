using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageTrig : MonoBehaviour
{
    public bool trig = false;
    void OnTriggerEnter(Collider obj){
        //this.gameObject.
        if(!trig && (obj.gameObject.transform.CompareTag("Player") || obj.gameObject.transform.parent.CompareTag("Player"))){
            trig = true;
            gameObject.transform.parent.GetChild(0).Translate(10000, 0, 0);
            GameObject zombie;
            for (int i = 0; i < gameObject.transform.parent.GetChild(2).childCount; i++){
                zombie = gameObject.transform.parent.GetChild(2).GetChild(i).gameObject;
                zombie.SetActive(true);
                zombie.GetComponent<ZombieController>().stop = false;
            }
        }        
    }
}
