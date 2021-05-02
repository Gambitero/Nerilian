using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkWall : MonoBehaviour
{
    public float blinkTime = 1f;
    float count;
    bool state;
    void Start()
    {
        count = 0f;
        state = true;
    }
    
    void Update()
    {
        count += Time.deltaTime;

        if(count > blinkTime){
            state = !state;
            count = 0f;
            gameObject.GetComponent<MeshRenderer>().enabled = state;
            gameObject.GetComponent<BoxCollider>().enabled = state;
        }
    }

    void OnTriggerEnter(Collider obj){
        if(obj.gameObject.CompareTag("Player")){
            obj.gameObject.GetComponent<Controller>().Die();
        }
    }
}
