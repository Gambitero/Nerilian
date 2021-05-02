using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    void onTriggerEnter(Collider obj){
        Debug.Log("afa " + obj.tag);
    }
}
