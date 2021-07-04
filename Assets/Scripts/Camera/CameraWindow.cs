using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWindow : MonoBehaviour
{
    public bool trueFollow = false;

    public Transform target;
    private float offset;
    private float count;    

    private Vector3 mov;
    public float smoothing = 5f;
    public float initY;
    public Vector3 spawnPosition;

    bool outLimit = false;
    public bool stop;

    public SceneController sceneController;

    public void SaveSpawnValues()
    {
        spawnPosition = transform.position;        
    }    

    // Se reajusta la Y si el jugador deja de colisionar con el objeto CameraWindow triggereando el outlimit
    public void MoveY(bool Up){        
        if(!stop){            
            int dir = 1;

            if(!Up){
                dir = -1;
            }        
            mov = new Vector3(0,4*dir,0);
            
            outLimit = true;

            initY = transform.position.y;        
        }
        
    }

    public void MoveY(bool Up, float Vel){        
        if(!stop){            
            int dir = 1;

            if(!Up){
                dir = -1;
            }        
            mov = new Vector3(0,Vel*dir,0);
            
            outLimit = true;

            initY = transform.position.y;        
        }
        
    }
    
    public void Respawn()
    {        
        transform.position = new Vector3(transform.position.x, spawnPosition.y, transform.position.z);        
        offset = transform.position.y - target.position.y;
        initY = transform.position.y;   
        outLimit = false;
        stop = false;
        trueFollow = false;
    }
    
    void Start()
    {   
        outLimit = false;
        stop = false;
        count = 0f;      
        offset = transform.position.y - target.position.y;
        SaveSpawnValues();
    }

    void Update()
    {
        if(!stop){
            Vector3 targetCamPos = target.position + new Vector3(0,offset,0);
            if(trueFollow){                
                //transform.position = new Vector3(targetCamPos.x, targetCamPos.y,targetCamPos.z);
                transform.position = new Vector3(targetCamPos.x, 
                        Vector3.Lerp(transform.position, target.position, smoothing * 2.5f * Time.deltaTime).y, 
                        targetCamPos.z);                
                return;
            }

            if(outLimit){
                transform.position = new Vector3(transform.position.x, 
                        Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime).y, 
                        transform.position.z);            
                if(Mathf.Abs(transform.position.y - target.position.y) <= 1.2f * mov.y){
                    mov.Set(0,target.position.y - transform.position.y ,0);
                }
                if(Mathf.Abs(transform.position.y - target.position.y) <= 0.6){
                    transform.position.Set(transform.position.x, target.position.y + offset, transform.position.z);                
                    count = 0f;
                    outLimit = false;
                }
            }

            count += Time.deltaTime;       
        
        
            if (count > 0.25f && count < 1){
                transform.position.Set(transform.position.x, target.position.y + offset, transform.position.z);
            }
            transform.position = new Vector3(targetCamPos.x, transform.position.y,targetCamPos.z);
        }
    }
}
