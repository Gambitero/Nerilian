using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    private float turnProgress = 0f;

    public bool turnCamera = false;
    public float turnSpeed = 0.25f;

    public Vector3 targetTurnPosition;
    private Vector3 offset;

    void Start()
    {
        turnProgress = 0f;
        offset = transform.position - target.position;
    }

    public void TurnCamera(){
        turnProgress += Mathf.Abs(turnSpeed);

        offset = transform.position - target.position;
        gameObject.transform.RotateAround(target.position, Vector3.up, turnSpeed);        

        if (turnProgress + 0.001f >= 90){
            turnProgress = 0f;
            turnCamera = false;
            return;
        }
    }


    void Update()
    {
        if(turnCamera){            
            TurnCamera();
            return;
        }
        Vector3 targetCamPos = target.position + offset;
        //targetCamPos = new Vector3 (targetCamPos.x, targetCamPos.y, targetCamPos.z);

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);        
    }
}
