using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterController controller;
    public GameObject camera;
    private float speed = 12f;
    private float gravity = -9.81f*8;
    public float jumpHeight = 2.4f;
    
    public float turnSpeed = 0.25f;
    public int cameraTurnSpeed = 1;
    private float currentTurn;
    private Collider turnObj = null;
    private int turnDir = 0;
    private float turnProgress = 0f;

    public bool groundFlag = true;
    public bool flagCamera = false;
    public bool flagTurn = false;    
    
    public float angle;
    public float rotationDir = -0.25f;

    public Vector3 newPos;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    public bool isGrounded;
    
    void SetTurnValues(){        
        turnDir = turnObj.gameObject.GetComponent<Turnpoint>().turnDir;
        turnObj.gameObject.GetComponent<Turnpoint>().turnDir *= -1;
        newPos = turnObj.gameObject.transform.position;

        turnProgress = 0f;
        
        Debug.Log("TurnDir ==> " + turnDir);

        //Activar giro de cámara
        camera.GetComponent<CameraController>().turnCamera = true;
        camera.GetComponent<CameraController>().turnSpeed = turnSpeed/cameraTurnSpeed * turnDir;
        camera.GetComponent<CameraController>().targetTurnPosition = gameObject.transform.position;
        //

        this.gameObject.transform.position = new Vector3(newPos.x, this.gameObject.transform.position.y, newPos.z);
            
        angle = controller.gameObject.transform.rotation.eulerAngles.y;
        
        Debug.Log("The Starting Angle");
        Debug.Log(angle);

        angle = standardAngle(angle + 0.25f * turnDir) + 0.25f * -turnDir;

        Debug.Log("The Fixed Starting Angle");
        Debug.Log(angle);

        flagCamera = true;            
        
        Debug.Log("The Angle");
        Debug.Log(angle + 90 * turnDir);
    }

    void OnTriggerEnter(Collider obj){
        if (obj.gameObject.CompareTag("Turnpoint")){
            flagTurn = true;
            Debug.Log("Enter turnpoint");
            turnObj = obj;            
        }

        if (obj.gameObject.CompareTag("Platform")){
            groundFlag = true;
        }
    }

    void OnTriggerExit(Collider obj){        
        if (obj.gameObject.CompareTag("Turnpoint")){
            flagTurn = false;
            Debug.Log("Exit turnpoint");
        }

        if (obj.gameObject.CompareTag("Platform")){
            groundFlag = false;
        }
    }

    void OnTriggerStay(Collider obj){
        if (obj.gameObject.CompareTag("Platform")){
            groundFlag = true;
        }
    }

    float standardAngle(float angle){
        if(angle<0)
            return 360f+angle;
        return angle;
    }

    void TurnPlayer(){
        turnProgress += turnSpeed;

        controller.gameObject.transform.Rotate(0f, turnSpeed * turnDir, 0f);

        if (turnProgress + 0.001f >= 90){
            Debug.Log("Stop");
            controller.gameObject.transform.eulerAngles = new Vector3(controller.gameObject.transform.rotation.eulerAngles.x, angle + 90 * turnDir, controller.gameObject.transform.rotation.eulerAngles.z);
            flagCamera = false;
            return;
        }
    }

    void Update()
    {
        if (flagCamera)
        {
            /*
            //Time.deltaTime * -50f
            controller.gameObject.transform.Rotate(0f, 0.25f * rotationDir, 0f);
            Debug.Log("---");
            Debug.Log(controller.gameObject.transform.rotation.eulerAngles.y);
            
            if (controller.gameObject.transform.rotation.eulerAngles.y <= 8007f0as7f0){
                controller.gameObject.transform.eulerAngles = new Vector3(controller.gameObject.transform.rotation.eulerAngles.x, 8007f0as7f0, controller.gameObject.transform.rotation.eulerAngles.z);
                //controller.gameObject.transform.rotation.eulerAngles.Set(controller.gameObject.transform.rotation.eulerAngles.x, 8007f0as7f0-12f, controller.gameObject.transform.rotation.eulerAngles.z);
                flagCamera = false;                
            }
            */            
            TurnPlayer();
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * x;

        controller.Move(move * speed * Time.deltaTime);

        if(groundFlag && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(flagTurn && Input.GetButtonUp("CameraRotation"))
        {
            SetTurnValues();            
        }        
    }
}
