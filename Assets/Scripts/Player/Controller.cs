using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public bool invincible = false;
    public CharacterController controller;
    public PlayerStats plStats; 
    public GameObject sceneCamera;
    public Text livesText = null;
    public float speed = 12f;
    public float weight = 1.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    
    public float turnSpeed = 0.25f;
    private float precision = 0.001f;
    public int cameraTurnSpeed = 1;
    private float currentTurn;
    private Collider turnObj = null;
    private int turnDir = 0;
    private float turnProgress = 0f;

    public bool groundFlag = false;
    public bool flagTurning = false;
    public bool flagTurn = false;

    bool waitingForRespawn = false;
    bool waitingForEnd = false;
    
    public float angle;
    public float rotationDir = -0.25f;

    public SceneController sceneController;
    public Vector3 newPos;    

    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Vector3 fallVelocity;

    public bool jumping = false;
    public bool resetFallVel = false;
    public float resetFallTimer = 0f;

    public float hangTime = 0.2f;
    private float hangCount;

    public float jumpBufferLength = 0.1f;
    private float jumpBufferCount;

    private int lookDir = 1;
    public float dashTime = 3f;
    public float dashSpeed = 0.5f;
    public float dashDelay = 0.075f;
    private float dashCount;
    private bool isDashing = false;

    private GameObject windowObj;
    
    // Se asignan las variables necesarias para hacer el giro
    void SetTurnValues(int turnButton){
        //turnDir = turnObj.gameObject.GetComponent<Turnpoint>().turnDir;
        turnDir = turnButton;
        //turnObj.gameObject.GetComponent<Turnpoint>().turnDir *= -1;
        newPos = turnObj.gameObject.transform.position;

        turnProgress = 0f;
        
        //Debug.Log("TurnDir ==> " + turnDir);

        //Activar giro de cámara
        sceneCamera.GetComponent<CameraController>().turnCamera = true;
        sceneCamera.GetComponent<CameraController>().turnSpeed = turnSpeed/cameraTurnSpeed * turnDir;
        sceneCamera.GetComponent<CameraController>().targetTurnPosition = gameObject.transform.position;
        //
        controller.enabled = false;
        this.gameObject.transform.position = new Vector3(newPos.x, this.gameObject.transform.position.y, newPos.z);
        controller.enabled = true;
            
        angle = controller.gameObject.transform.rotation.eulerAngles.y;
        
        //Debug.Log("The Starting Angle");
        //Debug.Log(angle);

        angle = standardAngle(angle + 0.25f * turnDir) + 0.25f * -turnDir;

        //Debug.Log("The Fixed Starting Angle");
        //Debug.Log(angle);

        flagTurning = true;            
        
        //Debug.Log("The Angle");
        //Debug.Log(angle + 90 * turnDir);
    }

    void OnTriggerEnter(Collider obj){
        // Se permite el realizar un giro, flagTurn = true
        if (obj.gameObject.CompareTag("Turnpoint")){
            flagTurn = true;
            //Debug.Log("Enter turnpoint");
            turnObj = obj;
            return;
        }

        // Muerte por caída
        if (obj.gameObject.CompareTag("Deathplane")){
            this.Die();
            return;
        }
        
        // No se desplaza la cámara en Y
        if (obj.gameObject.CompareTag("Window")){
            windowObj = null;
            return;
        }
    }
    void OnCollisionEnter(Collision obj){        
        if (obj.gameObject.CompareTag("Saw")){            
            this.Die();
            return;
        }
    }

    void OnTriggerExit(Collider obj){
        // Se deja de permitir el realizar un giro, flagTurn = false      
        if (obj.gameObject.CompareTag("Turnpoint")){
            flagTurn = false;
            return;
            //Debug.Log("Exit turnpoint");
        }

        // Se activa el desplazamiento de cámara en y porque el jugador se ha salido de la ventana de la cámara.
        if (obj.gameObject.CompareTag("Window")){
            windowObj = obj.gameObject;
            return;
        }
    }

    void resetFall(){
        fallVelocity.y = 0;
        resetFallVel = false;
        resetFallTimer = 0f;
    }
    
    // Función auxiliar para evitar ángulos negativos
    float standardAngle(float angle){
        if(angle<0)
            return 360f+angle;
        return angle;
    }

    // Se define el giro del jugador, turnSpeed define cuanto gira el jugador por cada vez que se llama a este método.
    // El valor se va acumulando en turnProgress y cuando llega a 90 se detiene el giro, la suma de 0.001f en el if se hace
    // porque los valores flotantes no son exactos y es posible que llegue a dar 89.996.
    void TurnPlayer(){
        turnProgress += turnSpeed;

        controller.gameObject.transform.Rotate(0f, turnSpeed * turnDir, 0f);

        if (turnProgress + precision >= 90){
            //Debug.Log("Stop");
            controller.gameObject.transform.eulerAngles = new Vector3(controller.gameObject.transform.rotation.eulerAngles.x, angle + 90 * turnDir, controller.gameObject.transform.rotation.eulerAngles.z);
            controller.gameObject.GetComponent<Transform>().rotation.Set( 
                //truncamiento de las unidades. al parecer unity considera las precisiones en radianes y al girar 90 grados se queda residuo
                Mathf.RoundToInt(controller.gameObject.GetComponent<Transform>().rotation.x),
                Mathf.RoundToInt(controller.gameObject.GetComponent<Transform>().rotation.y),
                Mathf.RoundToInt(controller.gameObject.GetComponent<Transform>().rotation.z),
                Mathf.RoundToInt(controller.gameObject.GetComponent<Transform>().rotation.w));
            flagTurning = false;
            return;
        }
    }

    // Método de respawn, se ejecuta tras el fadeOut y el objetivo es llevar al jugador al último spawnPoint almacenado y resetear las partes del nivel    
    public void Respawn()
    {        
        // Se actualiza el texto con las vidas
        livesText.text = "x" + plStats.lives;
        // Se desactiva y reactiva el controller porque si no, no se puede cambiar el trasnform.position directamente.
        controller.enabled = false;
        gameObject.transform.position = sceneController.spawnPoint.transform.position;
        gameObject.transform.rotation = sceneController.spawnPoint.transform.rotation;
        controller.enabled = true;
        controller.Move(new Vector3(0f,2f,0f));
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().Respawn();
        GameObject.FindGameObjectWithTag("Window").GetComponent<CameraWindow>().Respawn();          
    } 

    // Define la muerrte del jugador, pierde una vida, se hace un fade in a negro (dura 1 segundo) y se detiene la camara parando el CameraWindow
    // Si le quedan vidas se activa el waitingForRespawn y el jugador volverá a aparecer en el último punto de respawn. De lo contrario aparecerá
    // el Game Over tras el fadeOut, proceso que comenzará poniendo la variable waitingForEnd a true.
    public void Die(){
        if(this.sceneController.waiting || this.invincible){
            return;
        }

        plStats.lives--;        
        sceneController.Fade(1f);
        GameObject.FindGameObjectWithTag("Window").GetComponent<CameraWindow>().stop = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().stop = true;
        
        if(plStats.lives>=0)
        {
            waitingForRespawn = true;            
        }
        else
        {
            waitingForEnd = true;
        }
    }
    
    public void Start()
    {
        var builder = gameObject.GetComponentInParent<CharacterBuild>();
        builder.PersonajeBuilder(builder.CharacterClass, builder.CharacterPowerUps, gameObject);
        //livesText.text = "x" + plStats.lives;
    }

    void Update()
    {
        //* Comportamientos relacionados con la muerte del jugador
        //-----------------------------------------------------------------------------------------
        // Si se está esperando a respawnear, es porque se está esperando a que finalize la animación,
        // mientras la variable waiting de sceneController sea true, se está realizando la animación por
        // lo que deberemos esperar a que esta se vuelva falsa antes de realizar el Respawn.
        if(waitingForRespawn)
        {            
            if(!sceneController.waiting)
            {
                waitingForRespawn = false;
                Respawn();
                // El delayedFade es una animación de Fade que funciona igual que un fade pero tiene un delay
                // (definido por la primera variable que se le pasa) en realizar la animación de fade
                sceneController.DelayedFade(1f, 1.0f);                
            }
        }
        // Si se está esperando al final de partida, cuando se termine el fadeOut, es decir cuando waiting sea
        // false, haremos la animación de GameOver
        else if(waitingForEnd)
        {
            if(!sceneController.waiting)
            {
                sceneController.GameOver();
            }
        }
        //-----------------------------------------------------------------------------------------
        //* Giro
        //-----------------------------------------------------------------------------------------
        // Realizar giro
        if (flagTurning)
        {                   
            TurnPlayer();
            return;
        }

        // Configurar valores para el giro
        if (flagTurn && Input.GetButtonUp("CameraRotationUP"))
        {
            //poner el dir -1 W
            SetTurnValues(-1);
        }
        if (flagTurn && Input.GetButtonUp("CameraRotationDOWN"))
        {
            //poner el dir 1 S            
            SetTurnValues(1);  
        }
                  
                
        //-----------------------------------------------------------------------------------------
        //* Movimiento y gravedad
        //-----------------------------------------------------------------------------------------
        // Si el dash está activado
        if (dashCount >= 0){            
            dashCount -= Time.deltaTime;
            // Delay de 0.035 segundos
            if(dashCount < dashTime - dashDelay)
                Debug.Log("Dash");
                controller.Move(transform.right * lookDir * dashSpeed);
            return;
        }
        else if (isDashing){
            dashCount -= Time.deltaTime;
            if(dashCount <= -dashTime * 3){
                isDashing = false;
            }
        }
        
        float x = Input.GetAxis("Horizontal");

        Vector3 move = transform.right * x;

        if(x != 0){
            lookDir = (int)Mathf.Sign(x);
        }

        // Acticación del dash
        if (Input.GetButtonDown("Dash") && !isDashing){            
            dashCount = dashTime + dashDelay;
            isDashing = true;
        }
        
        // Hangtime: Se puede saltar unos frames después de caerse de un borde
        if (groundFlag){
            hangCount = hangTime;
        }
        else{
            hangCount -= Time.deltaTime;
        }

        // Jump buffer: Se puede saltar unos frames antes de tocar el suelo
        if(Input.GetButtonDown("Jump")){
            jumpBufferCount = jumpBufferLength;
        }
        else{
            jumpBufferCount -= Time.deltaTime;
        }
        
        // Jump hold: Se salta más alto si se mantiene pulsada la tecla
        if(Input.GetButtonUp("Jump") && fallVelocity.y > 0){
            fallVelocity.y *= 0.6f;
        }

        // Inicio del salto
        if(!jumping && hangCount > 0f && jumpBufferCount >= 0f)
        {
            resetFall();
            fallVelocity = Vector3.up * jumpHeight;
            jumping = true;
        }

        // Si estamos en el aire, la gravedad surte efecto y descendemos
        if(!groundFlag){
            fallVelocity.y += gravity * weight * Time.deltaTime;
        }

        move.y = fallVelocity.y;

        controller.Move(move * speed * Time.deltaTime);
        
        // Se gestiona el reseteo de fallVelocity.y para que no se acumule la gravedad en varias caidas
        if(resetFallVel){
            if(resetFallTimer<0.5f){
                resetFallTimer += Time.deltaTime;
            }
            else{
                resetFall();
            }
        }
        //-----------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------
        //* Cámara
        //-----------------------------------------------------------------------------------------
        // Si estamos en el suelo y no estamos en colisión con el cameraWindow, desplazamos el mismo
        // en y
        if(windowObj != null && groundFlag  && !isDashing){
            windowObj.GetComponent<CameraWindow>().MoveY(windowObj.transform.position.y < gameObject.transform.position.y);
            windowObj = null;
        }
        //-----------------------------------------------------------------------------------------
    }
}
