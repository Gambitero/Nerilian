using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReset : MonoBehaviour
{
    public void EnemReset(){
        foreach (Transform child in gameObject.transform){
            if(!child.gameObject.activeSelf){
                child.gameObject.SetActive(true);
            }
        }
    }
}
