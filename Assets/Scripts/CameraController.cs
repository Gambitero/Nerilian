using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    private float turnProgress = 0f;
    private float precision = 0.0001f;

    public bool turnCamera = false;
    public float turnSpeed = 0.25f;

    public bool stop = false;

    public Vector3 targetTurnPosition;
    private Vector3 offset;
    private Vector3 spawnOffset;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    void SaveSpawnValues()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
        spawnOffset = offset;
    }

    public void Respawn()
    {   
        transform.rotation = spawnRotation;     
        transform.position = spawnPosition;
        offset = spawnOffset;        
        stop = false;
    }

    public void TurnCamera(){
        turnProgress += Mathf.Abs(turnSpeed);

        offset = transform.position - target.position;
        gameObject.transform.RotateAround(target.position, Vector3.up, turnSpeed);
        

        if (turnProgress + precision >= 90){
            turnProgress = 0f;
            gameObject.GetComponent<Transform>().rotation.Set(
               //truncamiento de las unidades. al parecer unity considera las precisiones en radianes y al girar 90 grados se queda residuo
               Mathf.RoundToInt(gameObject.GetComponent<Transform>().rotation.x),
               Mathf.RoundToInt(gameObject.GetComponent<Transform>().rotation.y),
               Mathf.RoundToInt(gameObject.GetComponent<Transform>().rotation.z),
               Mathf.RoundToInt(gameObject.GetComponent<Transform>().rotation.w));
            turnCamera = false;
            return;
        }
    }

    void Start()
    {        
        stop = false;
        turnProgress = 0f;
        offset = transform.position - target.position;
        SaveSpawnValues();
    }

    void Update()
    {
        if(!stop){                
            if(turnCamera){            
                TurnCamera();
                return;
            }
            Vector3 targetCamPos = target.position + offset;
            //targetCamPos = new Vector3 (targetCamPos.x, targetCamPos.y, targetCamPos.z);

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);        
        }
    }
}
