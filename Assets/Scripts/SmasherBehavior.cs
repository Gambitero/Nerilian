using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherBehavior : MonoBehaviour
{
    public float moveSpeed = 0.03f;
    public float stopTime = 0.5f;
    public bool trig = false;
    Vector3 initPos;
    public float count;

    void Start()
    {        
        initPos = this.gameObject.transform.parent.transform.position;        
        count = 0f;
    }

    void Update()
    {
        if(!trig && moveSpeed < 0){
            count += Time.deltaTime;
            if(count > stopTime){
                count = 0f;
                trig = true;
            }
        }
        if(trig){
            this.gameObject.transform.parent.Translate(new Vector3(0, -moveSpeed, 0));                        
            if(this.gameObject.transform.parent.transform.position.y - initPos.y > moveSpeed){
                this.gameObject.transform.parent.transform.position = initPos;
                trig = false;
            }
        }
    }

    void OnTriggerEnter(Collider obj){
        if(obj.gameObject.transform.parent.CompareTag("Player")){
            obj.gameObject.GetComponentInParent<Controller>().Die();
        }
        
        else if(obj.CompareTag("Platform")){            
            moveSpeed *= -1;
            trig = false;
        }
    }
}
