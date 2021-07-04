using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public static int Cindex = 0;
    public static int Pindex = 0;
    public bool invincible = false;
    public CharacterController controller;
    public PlayerStats PlayerStats;     
    public Text livesText = null;
    public Text scoreText = null;
    public float speed = 12f;
    public float weight = 1.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public static float limitJump = 1;
    public bool dashFlag = true;    
    public bool bunnyFlag = true;
    
    public float turnSpeed = 2.0f;
    private float precision = 0.001f;
    public int cameraTurnSpeed = 2;
    private float currentTurn;
    private Collider turnObj = null;
    private int turnDir = 0;
    private float turnProgress = 0f;

    public bool groundFlag = false;
    public bool flagTurning = false;
    public bool flagTurn = false;

    bool waitingForRespawn = false;
    bool waitingForEnd = false;
    float waitingToMoveCount = 0f;
    
    public float angle;
    public float rotationDir = -0.7f;

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
    private float jumpCount = 0;

    private int prevLookDir = 1;
    private int lookDir = 1;
    public float dashTime = 3f;
    public float dashSpeed = 0.5f;
    public float dashDelay = 0.075f;
    private float dashCount;
    private bool isDashing = false;

    public CameraController cameraController;

    public int bunnyCount = 1;

    private GameObject windowObj;

    public Animator animator;
    public GameObject pauseObj;

    public int score = 0;
    public int totalScore = 0;

    private PlayerInput playerInput;


    private float moveX;
    private float gravityX;    
    
    private float inputAcceleration = 12f;
    private float inputDeceleration = 15f;

    private AudioSource Jump1;
    private AudioSource Jump2;
    private AudioSource Jump3;
    private AudioSource ScoreSound;
    private AudioSource WalkSound;
    
    // Se asignan las variables necesarias para hacer el giro
    void SetTurnValues(int turnButton){
        WalkSound.Stop();
        animator.SetBool("Move", false);
        //turnDir = turnObj.gameObject.GetComponent<Turnpoint>().turnDir;
        turnDir = turnButton;
        //turnObj.gameObject.GetComponent<Turnpoint>().turnDir *= -1;
        newPos = turnObj.gameObject.transform.position;

        turnProgress = 0f;        
        
        //Debug.Log("TurnDir ==> " + turnDir);

        //Activar giro de cámara
        cameraController.turnCamera = true;
        cameraController.turnSpeed = turnSpeed/cameraTurnSpeed * turnDir;
        cameraController.targetTurnPosition = gameObject.transform.position;
        //
        controller.enabled = false;
        this.gameObject.transform.position = new Vector3(newPos.x, this.gameObject.transform.position.y, newPos.z);
        controller.enabled = true;
            
        angle = controller.gameObject.transform.rotation.eulerAngles.y;
        
        //Debug.Log("The Starting Angle");
        //Debug.Log(angle);

        angle = standardAngle(angle + turnSpeed * turnDir) + turnSpeed * -turnDir;

        //Debug.Log("The Fixed Starting Angle");
        //Debug.Log(angle);

        flagTurning = true;
        
        //Debug.Log("The Angle");
        //Debug.Log(angle + 90 * turnDir);
    }


    
    void OnTriggerEnter(Collider obj){
        // Se permite el realizar un giro, flagTurn = true
        if (obj.gameObject.CompareTag("Turnpoint")){
            //Debug.Log(obj.gameObject.transform.name);
            flagTurn = true;
            //Debug.Log("Enter turnpoint");
            turnObj = obj;
            return;
        }

        if (obj.gameObject.CompareTag("Spawnpoint")){
            //Debug.Log("Enter Spawnpoint");
            sceneController.SetSpawnpoint(obj.gameObject);
            obj.gameObject.transform.rotation = gameObject.transform.rotation;
            cameraController.SaveSpawnValues();
            GameObject.FindGameObjectWithTag("Window").GetComponent<CameraWindow>().SaveSpawnValues();
            return;
        }

        // Muerte por caída
        if (obj.gameObject.CompareTag("Deathplane")){
            this.Die();
            return;
        }

        if (obj.gameObject.CompareTag("Score")){
            PlayerStats.TakeGold();
            ScoreSound.Play();
            obj.gameObject.SetActive(false);
            return;
        }
        
        // No se desplaza la cámara en Y
        if (obj.gameObject.CompareTag("Window")){
            windowObj = null;
            return;
        }

        if (obj.gameObject.CompareTag("Portal")){            
            sceneController.LoadScoreScene();
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
        //Debug.Log("reseteando fallvel");
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
        cameraController.TurnCamera();

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

    public void Bounce(float value){
        Jump2.Play();
        fallVelocity = Vector3.up * jumpHeight * value * limitJump;
        animator.SetTrigger("Bounce");
    }

    // Métodos de pausa, resume y pause
    public void Pause(){
        Time.timeScale = 0f;
    }

    public void Resume(){
        Time.timeScale = 1f;
    }

    // Método de respawn, se ejecuta tras el fadeOut y el objetivo es llevar al jugador al último spawnPoint almacenado y resetear las partes del nivel    
    public void Respawn()
    {
        // Se actualiza el texto con las vidas
        livesText.text = "x" + PlayerStats.lives;
        // Se desactiva y reactiva el controller porque si no, no se puede cambiar el trasnform.position directamente.
        controller.enabled = false;
        gameObject.transform.position = sceneController.spawnPoint.transform.position;
        gameObject.transform.rotation = sceneController.spawnPoint.transform.rotation;
        controller.enabled = true;
        controller.Move(new Vector3(0f,2f,0f));
        cameraController.Respawn();
        GameObject.FindGameObjectWithTag("Window").GetComponent<CameraWindow>().Respawn();
        GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyReset>().EnemReset();
    } 

    // Define la muerrte del jugador, pierde una vida, se hace un fade in a negro (dura 1 segundo) y se detiene la camara parando el CameraWindow
    // Si le quedan vidas se activa el waitingForRespawn y el jugador volverá a aparecer en el último punto de respawn. De lo contrario aparecerá
    // el Game Over tras el fadeOut, proceso que comenzará poniendo la variable waitingForEnd a true.
    public void Die(){
        if(this.sceneController.waiting || this.invincible){
            return;
        }

        flagTurn = false;
        PlayerStats.lives--;        
        sceneController.Fade(1f);
        GameObject.FindGameObjectWithTag("Window").GetComponent<CameraWindow>().stop = true;
        cameraController.stop = true;
        
        if(PlayerStats.lives>=0)
        {
            waitingForRespawn = true;            
        }
        else
        {
            waitingForEnd = true;
        }
    }

    public void Move()
    {
        moveX = playerInput.actions["Move"].ReadValue<float>();
    }
    public virtual void MoveInput()
    {
        if (moveX == 0)
        {
            gravityX = Mathf.MoveTowards(gravityX, 0f, Time.deltaTime * inputDeceleration);
        }
        else
            gravityX = Mathf.MoveTowards(gravityX, moveX, Time.deltaTime * inputAcceleration);
    
        moveX = Mathf.Clamp(gravityX, -1, 1);
    }
    public CharacterBuild builder;// = new CharacterBuild();
    public void Awake()
    {        
        builder = gameObject.GetComponentInParent<CharacterBuild>();
        builder.CharacterClass.Add(CharacterBuild.clases.Normal);
        builder.CharacterClass.Add(CharacterBuild.clases.Vulkan);
        builder.CharacterClass.Add(CharacterBuild.clases.Herzs);
        //Debug.Log(builder.CharacterClass.Count);
        builder.CharacterPowerUps.Add(CharacterBuild.powerUps.No);
        builder.CharacterPowerUps.Add(CharacterBuild.powerUps.Dash);
        builder.CharacterPowerUps.Add(CharacterBuild.powerUps.Reactor);
    }
    public void Start()
    {
        cameraController = gameObject.transform.parent.GetComponentInChildren<CameraController>(); //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();        
        builder.PersonajeBuilder(builder.CharacterClass[Cindex], builder.CharacterPowerUps[Pindex], gameObject);
        animator = gameObject.GetComponentInChildren<Animator>();
        livesText.text = "x" + PlayerStats.lives;
        playerInput = GetComponent<PlayerInput>();
        Jump1 = GetComponent<AudioSource>();
        Jump2 = GetComponents<AudioSource>()[1];
        Jump3 = GetComponents<AudioSource>()[2];
        ScoreSound = GetComponents<AudioSource>()[3];
        WalkSound = GetComponents<AudioSource>()[4];
    }

    void FixedUpdate(){
        //-----------------------------------------------------------------------------------------
        //* Pausa
        //-----------------------------------------------------------------------------------------
        // Detectar si el juego está en pausa.
        if(Time.timeScale == 0){            
            return;
        }

        //-----------------------------------------------------------------------------------------
        //* Movimiento y gravedad
        //-----------------------------------------------------------------------------------------
        // Si el dash está activado
        if (dashFlag)
        {
            if (dashCount >= 0)
            {
                dashCount -= Time.deltaTime;
                // Delay de 0.035 segundos
                if (dashCount < dashTime - dashDelay * 0.05f){                    
                    controller.Move(transform.right * lookDir * dashSpeed);
                }
                return;
            }
            else if (isDashing)
            {
                dashCount -= Time.deltaTime;
                if (dashCount <= -dashTime * 3)
                {
                    isDashing = false;
                }
                return;
            }
        }

        // Se gestiona el reseteo de fallVelocity.y para que no se acumule la gravedad en varias caidas
        if(resetFallVel && groundFlag){                           
            resetFall();            
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
    }
    
    void Update()
    {        
        //-----------------------------------------------------------------------------------------
        //* Pausa del juego
        //----------------------------------------------------------------------------------------- 
        // Sólo se puede poner pausa si se está en el suelo.
        // Para poner pausa se pulsa la tecla escape, mientras el juego está en pausa no se reciben
        // inputs.
        if(playerInput.actions["Pause"].triggered && groundFlag){
            if(Time.timeScale == 0){
                Resume();
                pauseObj.SetActive(false);
            }
            else{
                Pause();
                pauseObj.SetActive(true);
            }
            return;
        }
        
        if(Time.timeScale == 0){            
            return;
        }        
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
                if (waitingToMoveCount <= 0f){
                    waitingToMoveCount = 1.0f;
                }
            }

            return;
        }
        // Si se está esperando al final de partida, cuando se termine el fadeOut, es decir cuando waiting sea
        // false, haremos la animación de GameOver
        else if(waitingForEnd)
        {
            if(!sceneController.waiting)
            {
                Respawn();
                sceneController.GameOver();
            }
            return;
        }
        //-----------------------------------------------------------------------------------------
        //* Giro
        //-----------------------------------------------------------------------------------------        
        if (flagTurning)
        {        
            return;
        }
        // Configurar valores para el giro
        if(!flagTurning){
            if (flagTurn && playerInput.actions["Up"].triggered)
            {
                //poner el dir -1 W
                SetTurnValues(-lookDir);            
            }
            if (flagTurn && playerInput.actions["Down"].triggered)
            {
                //poner el dir 1 S            
                SetTurnValues(lookDir);
            }
        }


        //-----------------------------------------------------------------------------------------
        //* Movimiento y gravedad
        //-----------------------------------------------------------------------------------------
        // Si el dash está activado        
        //float moveX = playerInput.actions["Move"].ReadValue<float>();//Input.GetAxis("Horizontal");
        if (waitingToMoveCount > 0f){
            waitingToMoveCount-= Time.deltaTime;
            return;
        }


        Move();
        MoveInput();
        
        if (moveX == 0){
            WalkSound.Stop();
            animator.SetBool("Move", false);
        }
        else if (!flagTurning){
            animator.SetBool("Move", true);
        }


        Vector3 move = transform.right * moveX;

        if(moveX != 0){
            if(!WalkSound.isPlaying)
                WalkSound.Play();
            lookDir = (int)Mathf.Sign(moveX);
            if (lookDir != prevLookDir){
                gameObject.transform.GetChild(0).Rotate(0f, 180f, 0f);
                prevLookDir = lookDir;                
            }
        }        

        // Acticación del dash
        if (dashFlag && playerInput.actions["Dash"].triggered && !isDashing){            
            dashCount = dashTime + dashDelay;
            isDashing = true;
        }
        
        // Hangtime: Se puede saltar unos frames después de caerse de un borde
        if (groundFlag){
            hangCount = hangTime;            
        }
        else{
            //animator.SetTrigger("Jump0");
            WalkSound.Stop();
            hangCount -= Time.deltaTime;
        }

        // Jump buffer: Se puede saltar unos frames antes de tocar el suelo
        if(playerInput.actions["Jump"].triggered){            
            jumpBufferCount = jumpBufferLength;            
        }
        else{            
            jumpBufferCount -= Time.deltaTime;
        }
        
        // Jump hold: Se salta más alto si se mantiene pulsada la tecla
        if(playerInput.actions["Jump"].triggered && fallVelocity.y > 0){
            fallVelocity.y *= 0.6f;
            if (fallVelocity.y < (jumpHeight + (1-jumpCount)*0.24f)/1){
                animator.SetBool("Height", false);
                //animator.SetFloat("MultJump", 2f);
            }
        }

        // Inicio del salto
        if(!jumping && hangCount > 0f && jumpBufferCount >= 0f)
        {
            WalkSound.Stop();
            //Debug.Log("salto");
            if (jumpCount == 0)
                Jump1.Play();
            else
                Jump3.Play();
            animator.SetBool("Ground", false);
            animator.SetBool("Height", true);
            animator.SetFloat("MultJump", 1f);
            if (moveX == 0){
                animator.SetTrigger("JumpInPlace");
            }
            else{
                animator.SetTrigger("Jump" + jumpCount);

            }            
            bunnyCount = 1;
            resetFall();
            fallVelocity = Vector3.up * (jumpHeight + jumpCount * 0.44f) * limitJump;
            jumping = true;
            jumpCount = 1 - jumpCount;
        }

        if (jumpBufferCount < -jumpBufferLength){
            jumpCount = 0;
        }

        // Si estamos en el aire, la gravedad surte efecto y descendemos
        if(!groundFlag){
            fallVelocity.y += gravity * weight * Time.deltaTime;
            // Salto doble o bunny hop
            if(bunnyFlag && bunnyCount == 1 && playerInput.actions["Jump"].triggered){
                Bounce(0.7f);
                bunnyCount = 0;
            }
        }

        move.y = fallVelocity.y;        

        controller.Move(move * speed * Time.deltaTime);        
        

        if(windowObj != null  && !isDashing && groundFlag){
            windowObj.GetComponent<CameraWindow>().MoveY(windowObj.transform.position.y < gameObject.transform.position.y, 6.25f);
            windowObj = null;
        }                
    }
}
