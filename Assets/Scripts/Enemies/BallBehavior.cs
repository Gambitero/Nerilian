using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dir = -1;
    public CharacterController controller;
    public bool x;
    public bool z;
    public bool y;
    
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();        
    }

    void Update()
    {   
        Vector3 move;
        if(z)
            move = new Vector3(0, 0, moveSpeed * dir) * Time.deltaTime;
        else if(y)
            move = new Vector3(0, moveSpeed * dir, 0) * Time.deltaTime;
        else
            move = new Vector3(moveSpeed * dir, 0, 0) * Time.deltaTime;            
        controller.Move(move);
    }

    void OnTriggerEnter(Collider obj){        
        if(obj.CompareTag("Player")){
            obj.gameObject.GetComponent<Controller>().Die();
        }        
    }

    void OnTriggerExit(Collider obj){        
        if(obj.CompareTag("PatrolRange")){
            dir *= -1;
        }
    }
}
