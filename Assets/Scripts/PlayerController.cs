using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float movementSpeed = 6.0f;//default 6.0
    [SerializeField] float jumpHeight = 5.0f;
    [SerializeField] float currentMovementSpeed = 6.0f;//current movement speed, modified by upgrades.
    [SerializeField] float sprintMovementBoost = 3.0f;

    [SerializeField] float gravity = -13f;

    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.3f;

    [SerializeField] CharacterController controller = null;
    [SerializeField] bool lockCursor = true;//Locks the cursor in place so it doesn't leave the game screen


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

        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        updateMovement();
    }

    void UpdateMouseLook()//gets mouse input
    {
        Vector2 TargetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, TargetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);


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
            movementSpeed += sprintMovementBoost;
        }
        else if (movementSpeed != currentMovementSpeed)
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
