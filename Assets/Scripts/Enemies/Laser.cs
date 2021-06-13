using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    float speed = 20f;
    float count = 0f;
    
    void Start()
    {
        startPosition = gameObject.transform.localPosition;
        startRotation = gameObject.transform.localRotation;        
        gameObject.SetActive(false);
    }
     
    void Update()
    {
        gameObject.transform.Translate(0,speed*Time.deltaTime,0);
        count += Time.deltaTime;

        if(count > 4f){
            this.Reset();  
        }
    }
    void OnTriggerEnter(Collider obj){
        if(obj.CompareTag("Player")){
            obj.gameObject.GetComponent<Controller>().Die();
            this.Reset();
        }

        if(obj.CompareTag("Platform")){
            this.Reset();
        }
    }

    void Reset(){
        gameObject.SetActive(false);

        count = 0f;
        
        gameObject.transform.SetParent(gameObject.transform.parent.transform.parent.GetChild(1));
        gameObject.transform.localRotation = startRotation;
        gameObject.transform.localPosition = startPosition;
    }
}
