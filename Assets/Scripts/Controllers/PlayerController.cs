using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;

    //The current walk speed, and the current max walk speed (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
    [SerializeField] float movementSpeed = 6.0f;//default 6.0f
    [SerializeField] float currentMovementSpeed = 6.0f;

    //The current jump height, and the current max jump height (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
    [SerializeField] float jumpHeight = 5.0f;//default 5.0f
    [SerializeField] float currentJumpHeight = 5.0f;

    //The current sprint speed, and the current max sprint speed (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
    [SerializeField] float sprintMovementSpeed = 9.0f;//default 9.0f
    [SerializeField] float currentSprintMovementSpeed = 9.0f;

    

    [SerializeField] float gravity = -13f;

    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.3f;

    [SerializeField] CharacterController controller = null;
    [SerializeField] bool lockCursor = true;//Locks the cursor in place so it doesn't leave the game screen

    Canvas PauseMenu = null;


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

        //sprintMovementSpeed = currentMovementSpeed + sprintMovementSpeed;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        GameObject PauseMenuObject = GameObject.Find("PauseMenu");//the pause menu object

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
            print("SPRINT" + sprintMovementSpeed);
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
}