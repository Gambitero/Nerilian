using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoBehaviour
{
    GameObject objective;
    GameObject laserBullet;
    Vector3 delayedPos;
    float delayedCount;
    float count;
    bool trig;
    public bool follow = true;
    public float trigDistance = 500f;
    public float trigDistance2 = 30f;
    public float trigAngle = 30f;
    public float lookingDir = 1; //0 left, 1 right, 2 forward, 3 backward    
    Vector3 dist;
    Vector3 comparer;
    void Start()
    {        
        objective = GameObject.FindGameObjectsWithTag("Player")[0];        
        delayedPos = objective.transform.position;
        trig = false;

        switch (lookingDir){
            case(0):
                comparer = Vector3.left;
                break;
            case(1):
                comparer = Vector3.right;
                break;
            case(2):
                comparer = Vector3.forward;
                break;
            case(3):
                comparer = Vector3.back;
                break;
                
        }
        
    }
    
    void shoot(){
        laserBullet = gameObject.transform.GetChild(1).gameObject;
        laserBullet.transform.SetParent(gameObject.transform.parent.GetChild(0));
        laserBullet.SetActive(true);        
    }

    void Update()
    {
        dist = -gameObject.transform.position+objective.transform.position;
        if (dist.sqrMagnitude < trigDistance && dist.sqrMagnitude > trigDistance2  && Vector3.Angle(dist, comparer) < trigAngle){
            trig = true;
        }
        else{
            trig = false;
            count = 1.0f;
        }
        
        // Si el jugador se encuentra en la distancia y ángulo de activación
        if(trig){
            count += Time.deltaTime;
            
            if(follow){
                delayedCount += Time.deltaTime;
                
                if(delayedCount > 0.1f){                
                    delayedCount = 0f;
                    gameObject.transform.LookAt(delayedPos);
                    delayedPos = objective.transform.position;
                }
            }

            //gameObject.transform.LookAt(Vector3.Lerp(delayedPos, objective.transform.position, .95f * Time.deltaTime));

            if(count > 1.5f){
                this.shoot();
                count = 0f;                
            }
        }
    }

    void FixedUpdate(){
        if(trig && follow)
            gameObject.transform.LookAt(Vector3.Lerp(delayedPos, objective.transform.position, .125f));
    }
}
