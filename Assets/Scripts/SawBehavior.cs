using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBehavior : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float dir = -1;
    public CharacterController controller;
    

    private bool IsFacingDown()
    {
        return transform.localScale.y > Mathf.Epsilon;
    }
    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();        
    }

    
    void Update()
    {   
        controller.Move(new Vector3(0,moveSpeed * dir,0) * Time.deltaTime);     
    }
    void OnCollisionEnter(Collision obj){
        Debug.Log("hello there");
    }
}
