using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWindow : MonoBehaviour
{
    public Transform target;
    private float offset;
    private float count;

    private Vector3 mov;
    public float smoothing = 5f;
    public float initY;

    bool outLimit = false;
    public bool stop;
    void Start()
    {  
        outLimit = false;
        stop = false;
        count = 0f;      
        offset = transform.position.y - target.position.y;
    }

    public void MoveY(bool Up){        
        
        int dir = 1;

        if(!Up){
            dir = -1;
        }        
        mov = new Vector3(0,5*dir,0);
        
        outLimit = true;

        initY = transform.position.y;        
    }
    void Update()
    {
        if(!stop){
            if(outLimit){
                transform.position = new Vector3(transform.position.x, 
                        Vector3.Lerp(transform.position, transform.position + mov, smoothing * Time.deltaTime).y, 
                        transform.position.z);            

                if(Mathf.Abs(transform.position.y - target.position.y) <= 0.1){
                    transform.position.Set(transform.position.x, target.position.y + offset, transform.position.z);                
                    count = 0f;
                    outLimit = false;
                }
            }

            count += Time.deltaTime;       
        
        
            if (count > 0.25f && count < 1){
                transform.position.Set(transform.position.x, target.position.y + offset, transform.position.z);
            }
            Vector3 targetCamPos = target.position + new Vector3(0,offset,0);
            transform.position = new Vector3(targetCamPos.x, transform.position.y,targetCamPos.z);
        }

       
    }
}
