using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject objective;
    public CharacterController controller;
    float normalSpeed = 0f;
    public float dir = 0.15f;
    public float chaseSpeed = 0.4f;
    public float prevDir = 0f;
    float speed = 12f;
    public float gravity = -7.5f;
    public Vector3 fallVelocity;
    float resetFallTimer = 0f;
    public bool resetFallVel = false;
    public bool groundFlag = false;
    public bool stop = false;
    public bool chase = false;
    public bool axis = true; // true x, false z

    public int health = 2;
    
    void resetFall(){
        fallVelocity.y = 0;
        resetFallVel = false;
        resetFallTimer = 0f;
    }

    void OnTriggerEnter(Collider obj){        
        if(obj.CompareTag("Player")){
            chase = false;
            dir *= normalSpeed/chaseSpeed;
            obj.gameObject.GetComponent<Controller>().Die();
        }        
    }

    public void GetHit(){
        Debug.Log("Me han dado");
        health --;
        objective.GetComponent<Controller>().Bounce();
        if (health == 0){
            this.Die();
        }
    }

    void Die(){
        this.gameObject.SetActive(false);        
    }

    void Update()
    {         
        //Vector3 move = transform.right;
        if(chase){
            prevDir = dir;
            if(axis){
                dir = Mathf.Sign(objective.transform.position.x - gameObject.transform.position.x) * chaseSpeed;
            }
            else{
                dir = Mathf.Sign(objective.transform.position.z - gameObject.transform.position.z) * chaseSpeed;
            }
            if (dir * prevDir < 0){
                transform.Rotate(Vector3.up, 180);
                stop = false;
            }
        }

        if(!groundFlag){
            fallVelocity.y += gravity * Time.deltaTime;
        }
        if(stop){
            controller.Move(fallVelocity * speed * Time.deltaTime);    
        }
        else{            
            Vector3 move;

            if(axis){
                move = Vector3.right * dir;
            }
            else{
                move = Vector3.forward * dir;
            }
            move.y = fallVelocity.y;
            controller.Move(move * speed * Time.deltaTime);
            
            if(resetFallVel){
                if(resetFallTimer<0.5f){
                    resetFallTimer += Time.deltaTime;
                }
                else{
                    resetFall();
                }
            }
        }
    }

    void Start(){
        objective = GameObject.FindGameObjectWithTag("Player");
        normalSpeed = Mathf.Abs(dir);
    }
}
