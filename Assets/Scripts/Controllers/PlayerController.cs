using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;

    //The current walk speed, and the current max walk speed (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
    [SerializeField] float movementSpeed = 7.0f;//default 7.0f
    [SerializeField] float currentMovementSpeed = 7.0f;

    //The current jump height, and the current max jump height (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
    [SerializeField] float jumpHeight = 5.0f;//default 5.0f
    [SerializeField] float currentJumpHeight = 5.0f;

    //The current sprint speed, and the current max sprint speed (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
    //Eventually I want this to be changed to a sprintMovementSpeedBoost so that sprinting adds to the movement speed instead of setting it to a hard number
    //This is currently an issue because when shift is held it would constantly add the speed until your're doing the speed of light!!
    [SerializeField] float sprintMovementSpeed = 10.0f;//default 10.0f
    [SerializeField] float currentSprintMovementSpeed = 10.0f;

    

    [SerializeField] float gravity = -13f;

    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.3f;

    [SerializeField] CharacterController controller = null;
    [SerializeField] bool lockCursor = true;//Locks the cursor in place so it doesn't leave the game screen

    //Player stats
    [SerializeField] int maxHealth = 100;
    [SerializeField] int health = 100;

    Canvas PauseMenu = null;
    GameObject PauseMenuObject = null;

    //HUD
    GameObject HudObject = null;
    Canvas HUD = null;
    GameObject HealthBarObject = null;
    Slider HealthBar = null;
    GameObject healthBarTextObject = null;
    TMP_Text healthBarText = null;


    float cameraPitch = 0.0f;//camera pitch, default is 0.0f
    float velocityY = 0.0f;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();//The controller to be used for character movement
        HudObject = GameObject.Find("HUD");//The HUD object that holds all the HUD items
        HealthBarObject = GameObject.Find("Health"); //The health bar object that acutally contains the health bar slider
        HUD = HudObject.GetComponent<Canvas>();//The canvas in the HUD object
        HealthBar = HealthBarObject.GetComponent<Slider>();//The health bar slider within the health bar object.
        healthBarTextObject = GameObject.Find("HealthText");//The text that displays the exact health of the player
        healthBarText = healthBarTextObject.GetComponent<TMP_Text>();//The text that displays the exact health of the player
        


        //sprintMovementSpeed = currentMovementSpeed + sprintMovementSpeed;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        PauseMenuObject = GameObject.Find("PauseMenu");//the pause menu object

        if (PauseMenuObject != null)
        {
            print("PauseMenu object found!");
        }
        else if (PauseMenuObject == null)
        {
            print("PauseMenu object not found!");
        }

        PauseMenu = PauseMenuObject.GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {
        if (lockCursor)
        {//These two lines locks the mouse to the middle of the screen and hides it from the player
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {//These two lines allow the mouse to move and be seen by the player
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        //If left mouse button is clicked
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            controller.GetComponent<Animator>().SetBool("attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            controller.GetComponent<Animator>().SetBool("attack", false);
        }

        //Animation for walking forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            controller.GetComponent<Animator>().SetBool("isWalking", true);
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
            controller.GetComponent<Animator>().SetBool("isWalking", false);
        }

        //Animation for walking backward
        if (Input.GetKeyDown(KeyCode.S))
        {
            controller.GetComponent<Animator>().SetBool("isBack", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            controller.GetComponent<Animator>().SetBool("isBack", false);
        }

        //Animation for walking left
        if (Input.GetKeyDown(KeyCode.A))
        {
            controller.GetComponent<Animator>().SetBool("isLeft", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            controller.GetComponent<Animator>().SetBool("isLeft", false);
        }

        //Animation for walking right
        if (Input.GetKeyDown(KeyCode.D))
        {
            controller.GetComponent<Animator>().SetBool("isRight", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            controller.GetComponent<Animator>().SetBool("isRight", false);
        }

        //Animation for jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.GetComponent<Animator>().SetBool("isJumping", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            controller.GetComponent<Animator>().SetBool("isJumping", false);
        }

        UpdateMouseLook();
        updateMovement();
    }

    void UpdateMouseLook()//gets mouse input
    {
        Vector2 TargetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, TargetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        if (Input.GetKeyDown(KeyCode.Q) && lockCursor == true)//Allows the cursor to move freely and makes it visible
        {
            lockCursor = false;
            print("LockCursor" + lockCursor);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && lockCursor == false)//Hides the cursor and locks it to the screen
        {
            lockCursor = true;
            print("LockCursor" + lockCursor);
        }

        if (PauseMenu.enabled)
        {
            lockCursor = false;
            mouseSensitivity = 0.0f;
            movementSpeed = 0.0f;
            sprintMovementSpeed = 0.0f;
            jumpHeight = 0.0f;
        }
        else if (PauseMenu.enabled == false)
        {
            lockCursor = true;
            mouseSensitivity = 3.5f;
            movementSpeed = currentMovementSpeed;
            sprintMovementSpeed = currentSprintMovementSpeed;
            jumpHeight = currentJumpHeight;
        }

        //print(mouseDelta);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    

    void updateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0;


        velocityY += gravity * Time.deltaTime;


        if (Input.GetKey(KeyCode.LeftShift))
        {
            //print("SPRINT" + sprintMovementSpeed);  
            movementSpeed = sprintMovementSpeed;
        }
        else if (movementSpeed != currentMovementSpeed && PauseMenu.enabled == false)
        {
            movementSpeed = currentMovementSpeed;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocityY += jumpHeight;
        }


        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * movementSpeed + Vector3.up * velocityY;
        //print(currentDir);

        controller.Move(velocity * Time.deltaTime);//passes the move vector to the CharacterController
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        HealthBar.value = health;
        healthBarText.text = health + "/" + maxHealth;
        if (health <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void heal(int healing)
    {
        //just making sure the healing doesn't push you past the max health
        if (health + healing < maxHealth)
        {
            health += healing;
        }
        else
        {
            health = maxHealth;
        }

        HealthBar.value = health;
        healthBarText.text = health + "/" + maxHealth;
    }
}
