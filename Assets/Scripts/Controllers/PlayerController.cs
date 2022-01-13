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

    

    //The current jump height, and the current max jump height (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
    [SerializeField] float jumpHeight = 5.0f;//default 5.0f
    [SerializeField] float currentJumpHeight = 5.0f;

    

    

    [SerializeField] float gravity = -13f;

    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.3f;

    [SerializeField] CharacterController controller = null;
    [SerializeField] bool lockCursor = true;//Locks the cursor in place so it doesn't leave the game screen

    //========================================Player stats========================================

    //====================Health====================
    //Defaults are:
    //health = 100
    //health level = 0
    //max health = 100
    //healthRegenerationLevel = 0
    //healthRegeneration = 1
    int defaultHealth = 100;
    [SerializeField] int healthLevel = 0;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int health = 100;
    [SerializeField] int healthRegenerationLevel = 0;
    [SerializeField] int healthRegeneration = 1;

    bool healthRegenerating = false;
    float healthRegenerationTimer = 1.0f;

    //====================Speed====================
    //The current walk speed, and the current max walk speed (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
    float defaultMovementSpeed = 7.0f;
    [SerializeField] float movementSpeed = 7.0f;//default 7.0f
    [SerializeField] float currentMovementSpeed = 7.0f;
    [SerializeField] int movementSpeedLevel = 0;

    //The current sprint speed, and the current max sprint speed (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
    //Eventually I want this to be changed to a sprintMovementSpeedBoost so that sprinting adds to the movement speed instead of setting it to a hard number
    //This is currently an issue because when shift is held it would constantly add the speed until your're doing the speed of light!!
    float defaultSprintMovementSpeed = 10.0f;
    [SerializeField] float sprintMovementSpeed = 10.0f;//default 10.0f
    [SerializeField] float currentSprintMovementSpeed = 10.0f;


    //====================Damage====================
    //This is handled in the "weaponHitDetection.cs" file
    [SerializeField] int damageLevel = 0;

    //====================Damage Protection====================
    //This percentage is used to reduce the amount of damage inflicted on the player
    //float defaultDamageProtectionPercentage = 0.0f;
    //[SerializeField] float damageProtectionPercentage = 0.0f;

    int defaultDamageProtection = 0;
    [SerializeField] int damageProtectionLevel = 0;
    [SerializeField] int damageProtection = 0;

    //====================Money====================
    //Default money is 0
    [SerializeField] float money = 1000.0f;
    float defaultIncomeMultiplier = 1.0f;
    [SerializeField] int incomeMultiplierLevel = 0;
    [SerializeField] float incomeMultiplier = 1.0f;
    //Income multiplier will go here
    //The framework for this one isn't done yet, so won't be implemented quite yet


    Canvas PauseMenu = null;
    GameObject PauseMenuObject = null;

    Canvas upgradeMenu = null;
    GameObject upgradeMenuObject = null;

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
        controller = GameObject.Find("Player").GetComponent<CharacterController>();//The controller to be used for character movement
        HudObject = GameObject.Find("HUD");//The HUD object that holds all the HUD items
        HealthBarObject = GameObject.Find("HealthBar"); //The health bar object that acutally contains the health bar slider
        HUD = HudObject.GetComponent<Canvas>();//The canvas in the HUD object
        HealthBar = HealthBarObject.GetComponent<Slider>();//The health bar slider within the health bar object.
        healthBarTextObject = GameObject.Find("HealthText");//The text that displays the exact health of the player
        healthBarText = healthBarTextObject.GetComponent<TMP_Text>();//The text that displays the exact health of the player
        money = 1000.0f;

        healthRegeneration = 1 + healthRegenerationLevel;
        incomeMultiplier = 1.0f;

        //====================Health====================
        //Defaults are:
        //health = 100
        //health level = 0
        //max health = 100
        //healthRegenerationLevel = 0
        //healthRegeneration = 1
        defaultHealth = 100;
        healthLevel = 0;
        maxHealth = 100;
        health = 100;
        healthRegenerationLevel = 0;
        healthRegeneration = 1;

        healthRegenerating = false;
        healthRegenerationTimer = 1.0f;

        //====================Speed====================
        //The current walk speed, and the current max walk speed (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
        defaultMovementSpeed = 7.0f;
        movementSpeed = 7.0f;//default 7.0f
        currentMovementSpeed = 7.0f;
        movementSpeedLevel = 0;

        //The current sprint speed, and the current max sprint speed (so if any status effects modify sprint speed it can easily be set back), this can be changed with upgrades
        //Eventually I want this to be changed to a sprintMovementSpeedBoost so that sprinting adds to the movement speed instead of setting it to a hard number
        //This is currently an issue because when shift is held it would constantly add the speed until your're doing the speed of light!!
        defaultSprintMovementSpeed = 10.0f;
        sprintMovementSpeed = 10.0f;//default 10.0f
        currentSprintMovementSpeed = 10.0f;


        //====================Damage====================
        //This is handled in the "weaponHitDetection.cs" file
        damageLevel = 0;

        //====================Damage Protection====================
        //This percentage is used to reduce the amount of damage inflicted on the player
        //float defaultDamageProtectionPercentage = 0.0f;
        //[SerializeField] float damageProtectionPercentage = 0.0f;

        defaultDamageProtection = 0;
        damageProtectionLevel = 0;
        damageProtection = 0;

        //====================Money====================
        //Default money is 0
        money = 1000.0f;
        defaultIncomeMultiplier = 1.0f;
        incomeMultiplierLevel = 0;
        incomeMultiplier = 1.0f;
        //Income multiplier will go here
        //The framework for this one isn't done yet, so won't be implemented quite yet


        //When an enemy dies, the player is rewarded
        EnemyAI.OnEnemyKilled += rewardPlayer;

        if (controller == null)
        {
            controller = GameObject.Find("Player").GetComponent<CharacterController>();//The controller to be used for character movement
        }

        if (HudObject == null)
        {
            HudObject = GameObject.Find("HUD");//The HUD object that holds all the HUD items
        }

        if (HealthBarObject == null)
        {
            HealthBarObject = GameObject.Find("HealthBar"); //The health bar object that acutally contains the health bar slider
        }

        if (HealthBar == null)
        {
            HealthBar = HealthBarObject.GetComponent<Slider>();//The health bar slider within the health bar object.
        }

        if (healthBarTextObject == null)
        {
            healthBarTextObject = GameObject.Find("HealthText");//The text that displays the exact health of the player
        }

        if (healthBarText == null)
        {
            healthBarText = healthBarTextObject.GetComponent<TMP_Text>();//The text that displays the exact health of the player
        }

        if (HUD == null)
        {
            HUD = HudObject.GetComponent<Canvas>();//The canvas in the HUD object
        }

        

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



        upgradeMenuObject = GameObject.Find("UpgradesPanel");//the upgrade menu object

        if (upgradeMenuObject != null)
        {
            print("upgradeMenu object found!");
        }
        else if (upgradeMenuObject == null)
        {
            print("upgradeMenu object not found!");
        }

        upgradeMenu = upgradeMenuObject.GetComponent<Canvas>();
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

        if (controller == null)
        {
            //print("Looking for Controller!");
            controller = GameObject.Find("Player").GetComponent<CharacterController>();//The controller to be used for character movement
        }
        else
        //{
        //    print("Controller found!?");
        //}

        if (HudObject == null)
        {
            HudObject = GameObject.Find("HUD");//The HUD object that holds all the HUD items
        }

        if (HealthBarObject == null)
        {
            HealthBarObject = GameObject.Find("HealthBar"); //The health bar object that acutally contains the health bar slider
        }

        if (HUD == null)
        {
            HUD = HudObject.GetComponent<Canvas>();//The canvas in the HUD object
        }

        if (HealthBar == null)
        {
            HealthBar = HealthBarObject.GetComponent<Slider>();//The health bar slider within the health bar object.
        }

        if (healthBarTextObject == null)
        {
            healthBarTextObject = GameObject.Find("HealthText");//The text that displays the exact health of the player
        }

        if (healthBarText == null)
        {
            healthBarText = healthBarTextObject.GetComponent<TMP_Text>();//The text that displays the exact health of the player
        }
        
        if (healthRegenerating == true)
        {
            healthRegenerationTimer -= Time.deltaTime;
            if (healthRegenerationTimer <= 0.0f)
            {
                if ((health + healthRegeneration) <= maxHealth)
                {
                    health += healthRegeneration;
                }
                else
                {
                    health = maxHealth;
                }

                //print("Health Regen" + healthRegeneration);
                healthBarText.text = health + "/" + maxHealth;
                HealthBar.value = health;

                healthRegenerationTimer = 1.0f;

                if(health == maxHealth)
                {
                    healthRegenerating = false;
                }
            }
            
        }
        
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

        if (PauseMenu.enabled || upgradeMenu.enabled)
        {
            lockCursor = false;
            mouseSensitivity = 0.0f;
            movementSpeed = 0.0f;
            sprintMovementSpeed = 0.0f;
            jumpHeight = 0.0f;
        }
        else if (!PauseMenu.enabled || !upgradeMenu.enabled)
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
        if (!controller)
        {
            print("Looking for Controller!");
            controller = GameObject.Find("Player").GetComponent<CharacterController>();//The controller to be used for character movement
        }

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

    //Damages the players health
    //The incoming damage is modified by the damage protection variable
    public void takeDamage(int damage)
    {
        healthRegenerating = false;
        health -= (damage -= damageProtection);
        HealthBar.value = health;
        healthBarText.text = health + "/" + maxHealth;
        if (health <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }

        StartCoroutine(enableHealthRegen());
    }

    IEnumerator enableHealthRegen()
    {
        //WaitForSecondsRealtime(5); is used if you don't want the games current tick speed to affect the timer
        //yield return new WaitForSecondsRealtime(5);
        yield return new WaitForSeconds(5);
        healthRegenerating = true;
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

    //increases/decreases the max health by the total levels upgraded
    //Updates the health bars values then refreshes it.
    //If the default for a stat is 0 then it'll just add the total of new levels onto the stat
    //If the default for a stat is above 0 then it'll add the default + total of new levels
    //Some stats use the levels to multiply a number (like health, it does "100 + level * 10")
    public void changeStatLevel(int numberOfLevels, int changeWhat)
    {
        switch (changeWhat)
        {
            case 0:
                healthLevel += numberOfLevels;
                print("Health levels: " + healthLevel);
                maxHealth += healthLevel * 10;
                print("Max Health: " + maxHealth);
                HealthBar.maxValue = maxHealth;
                healthBarText.text = health + "/" + maxHealth;
                break;
            case 1:
                healthRegenerationLevel += numberOfLevels;
                print("Health Regen " + healthRegeneration);
                healthRegeneration = 1 + healthRegenerationLevel;
                print("Health Regen " + healthRegeneration);
                print("Health Regen levels: " + healthRegenerationLevel + " health regen: " + healthRegeneration);
                break;
            case 2:
                damageLevel += numberOfLevels;
                //More to be done here once the rest of these upgrades are sorted
                break;
            case 3:
                damageProtectionLevel += numberOfLevels;
                damageProtection = damageProtectionLevel;
                break;
            case 4:
                movementSpeedLevel += numberOfLevels;
                movementSpeed = 7.0f + (float)numberOfLevels;
                currentMovementSpeed = 7.0f + (float)numberOfLevels;
                sprintMovementSpeed = 10.0f + (float)numberOfLevels;
                currentSprintMovementSpeed = 10.0f + (float)numberOfLevels;
                break;
            case 5:
                incomeMultiplierLevel += numberOfLevels;
                incomeMultiplier = 1 + incomeMultiplierLevel * 0.1f;
                break;
        }
    }

    public int getHealthLevel()
    {
        return healthLevel;
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    //This is used if the health level is being set straight to something
    //e.g when a status affect hard sets your health level
    public void setHealthLevel(int level)
    {
        healthLevel = level;
    }



    public float getPlayerMoney()
    {
        //print("Money: " + money);
        return money;
    }

    public void spendPlayerMoney(float spending)
    {
        print("Spending: " + spending + " deducted from: " + money);
        money -= spending;
    }


    public int getHealthRegenerationLevel()
    {
        return healthRegenerationLevel;
    }

    public int getHealthRegeneration()
    {
        return healthRegeneration;
    }

    public int getDamageLevel()
    {
        return damageLevel;
    }

    public int getDamageProtectionLevel()
    {
        return damageProtectionLevel;
    }

    public int getDamageProtection()
    {
        return damageProtection;
    }

    public int getSpeedLevel()
    {
        return movementSpeedLevel;
    }

    public float getSpeed()
    {
        return movementSpeed;
    }

    public int getIncomeMultiplierLevel()
    {
        return incomeMultiplierLevel;
    }

    public float getIncomeMultiplier()
    {
        return incomeMultiplier;
    }


    public void rewardPlayer()
    {
        print("Player being rewarded");
        money += (10.0f * incomeMultiplier);
        //money = 1010.0f;
    }
}
