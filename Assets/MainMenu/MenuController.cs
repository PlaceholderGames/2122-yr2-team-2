using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update


    int[] levelChangeArray = new int[] { 0, 0, 0, 0, 0, 0 };
    int[] levelArray = new int[] { 0, 0, 0, 0, 0, 0 };
    int healthLevel = 0;
    float healthLevelCost = 100;
    float playerMoney = 1000;

    int[] statUpgradeCosts = new int[] { 100, 1000, 100, 100, 1000, 100 };

    //This is where the players object and scripts are
    GameObject player = null;
    PlayerController playerControllerScript = null;
    GameObject weaponHitDetection = null;
    weaponHitDetection weaponControllerScript = null;

    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] public string currentLevel;

    [SerializeField] Canvas PauseMenu = null;
    [SerializeField] Canvas upgradeMenu = null;


    TMP_Text currentHealthText = null;
    TMP_Text HealthLevelText = null;
    
    TMP_Text currentHealthRegenerationText = null;
    TMP_Text HealthRegenerationLevelText = null;
    
    TMP_Text currentDamageText = null;
    TMP_Text DamageLevelText = null;
    
    TMP_Text currentDamageProtectionText = null;
    TMP_Text DamageProtectionLevelText = null;
    
    TMP_Text currentSpeedText = null;
    TMP_Text SpeedLevelText = null;
    
    TMP_Text currentIncomeMultiplierText = null;
    TMP_Text IncomeMultiplierLevelText = null;



    TMP_Text moneyText = null;



    [SerializeField] Slider BrightnessSlider = null;
    [SerializeField] Color ambientLightSetting;
    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
        currentLevel = SceneManager.GetActiveScene().name;
        RenderSettings.ambientLight = ambientLightSetting;
        DynamicGI.UpdateEnvironment();
    }

    public void ExitGameDialogYes()
    {
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    


    public void BrightnessSliderMoved()
    {
        Screen.brightness = BrightnessSlider.value;
        RenderSettings.ambientLight = new Color(1f, 1f, 1f) * BrightnessSlider.value;
        ambientLightSetting = RenderSettings.ambientLight;
        DynamicGI.UpdateEnvironment();
    }

    private int change = 1;
    //Had to make this function because the UI buttons only allow the use of functions
    //That only have 0 or 1 parameter, so this gets around that
    public void flipChangeBool(int setToWhat)
    {
        change = setToWhat;
    }


    //The healthLevelsChange variable is what get's incremented by the UI
    //If the player decides to confirm the changes made then this value is used when calling
    //the changeHealthLevel() function in the PlayerController.
    //It is then set to 0
    public void changeLevels(int changeWhat)
    {
        switch (changeWhat)
        {
            case 0:
                levelChangeArray[0] += change;

                if (levelChangeArray[0] < 0)
                {
                    levelChangeArray[0] = 0;
                }

                HealthLevelText.text = "Level: " + levelArray[0] + " + " + levelChangeArray[0];
                break;
            case 1:
                levelChangeArray[1] += change;

                if (levelChangeArray[1] < 0)
                {
                    levelChangeArray[1] = 0;
                }

                HealthRegenerationLevelText.text = "Level: " + levelArray[1] + " + " + levelChangeArray[1];
                break;
            case 2:
                levelChangeArray[2] += change;

                if (levelChangeArray[2] < 0)
                {
                    levelChangeArray[2] = 0;
                }

                DamageLevelText.text = "Level: " + levelArray[2] + " + " + levelChangeArray[2];
                break;
            case 3:
                levelChangeArray[3] += change;

                if (levelChangeArray[3] < 0)
                {
                    levelChangeArray[3] = 0;
                }

                DamageProtectionLevelText.text = "Level: " + levelArray[3] + " + " + levelChangeArray[3];
                break;
            case 4:
                levelChangeArray[4] += change;

                if (levelChangeArray[4] < 0)
                {
                    levelChangeArray[4] = 0;
                }

                SpeedLevelText.text = "Level: " + levelArray[4] + " + " + levelChangeArray[4];
                break;
            case 5:
                levelChangeArray[5] += change;

                if (levelChangeArray[5] < 0)
                {
                    levelChangeArray[5] = 0;
                }

                IncomeMultiplierLevelText.text = "Level: " + levelArray[5] + " + " + levelChangeArray[5];
                break;
        }

    }

    //This whole function needs sorting out.
    //it needs to tot up the total costs and check the player can afford it before applying changes
    //It then needs to loop through all 6 stats and apply changes
    float totalSpent = 0;
    public void applyLevelsChange(int changeWhat)
    {

        for (int i = 0; i < 6; i++)
        {
            totalSpent += levelChangeArray[i] * statUpgradeCosts[i];
        }

        if (playerMoney - totalSpent >= 0)
        {
            if (levelChangeArray[0] > 0)
            {
                playerControllerScript.changeStatLevel(levelChangeArray[0], 0);
                HealthLevelText.text = "Level: " + playerControllerScript.getHealthLevel() + " + 0";
                currentHealthText.text = "Current: " + playerControllerScript.getMaxHealth();
                levelChangeArray[0] = 0;
            }

            if (levelChangeArray[1] > 0)
            {
                playerControllerScript.changeStatLevel(levelChangeArray[1], 1);
                HealthRegenerationLevelText.text = "Level: " + playerControllerScript.getHealthRegenerationLevel() + " + 0";
                currentHealthRegenerationText.text = "Current: " + playerControllerScript.getHealthRegeneration();
                levelChangeArray[1] = 0;
            }

            if (levelChangeArray[2] > 0)
            {
                weaponControllerScript.setDamage(playerControllerScript.getDamageLevel());
                DamageLevelText.text = "Level: " + playerControllerScript.getDamageLevel() + " + 0";
                currentDamageText.text = "Current: " + weaponControllerScript.getDamage();
                levelChangeArray[2] = 0;
            }

            if (levelChangeArray[3] > 0)
            {
                playerControllerScript.changeStatLevel(levelChangeArray[3], 3);
                DamageProtectionLevelText.text = "Level: " + playerControllerScript.getDamageProtectionLevel() + " + 0";
                currentDamageProtectionText.text = "Current: " + playerControllerScript.getDamageProtection();
                levelChangeArray[3] = 0;
            }

            if (levelChangeArray[4] > 0)
            {
                playerControllerScript.changeStatLevel(levelChangeArray[4], 4);
                SpeedLevelText.text = "Level: " + playerControllerScript.getSpeedLevel() + " + 0";
                currentSpeedText.text = "Current: " + playerControllerScript.getSpeed();
                levelChangeArray[4] = 0;
            }

            if (levelChangeArray[5] > 0)
            {
                playerControllerScript.changeStatLevel(levelChangeArray[5], 5);
                IncomeMultiplierLevelText.text = "Level: " + playerControllerScript.getIncomeMultiplierLevel() + " + 0";
                currentIncomeMultiplierText.text = "Current: " + playerControllerScript.getIncomeMultiplier();
                levelChangeArray[5] = 0;
            }


            playerControllerScript.spendPlayerMoney(totalSpent);
            
            moneyText.text = "| Money: " + playerMoney;
        }
        
    }


    GameObject PauseMenuObject = null;
    GameObject upgradeMenuObject = null;
    GameObject BrightnessSliderObject = null;

    void Start()
    {
        PauseMenuObject = GameObject.Find("PauseMenu");//the pause menu object
        upgradeMenuObject = GameObject.Find("UpgradesPanel");//the pause menu object
        BrightnessSliderObject = GameObject.Find("BrightnessSlider");
        currentLevel = SceneManager.GetActiveScene().name;

        currentHealthText = GameObject.Find("CurrentHealth").GetComponent<TMP_Text>();
        HealthLevelText = GameObject.Find("CurrentHealthLevel").GetComponent<TMP_Text>();

        currentHealthRegenerationText = GameObject.Find("CurrentHealthRegeneration").GetComponent<TMP_Text>();
        HealthRegenerationLevelText = GameObject.Find("CurrentHealthRegenerationLevel").GetComponent<TMP_Text>();

        currentDamageText = GameObject.Find("CurrentDamage").GetComponent<TMP_Text>();
        DamageLevelText = GameObject.Find("CurrentDamageLevel").GetComponent<TMP_Text>();

        currentDamageProtectionText = GameObject.Find("CurrentDamageProtection").GetComponent<TMP_Text>();
        DamageProtectionLevelText = GameObject.Find("CurrentDamageProtectionLevel").GetComponent<TMP_Text>();

        currentSpeedText = GameObject.Find("CurrentSpeed").GetComponent<TMP_Text>();
        SpeedLevelText = GameObject.Find("CurrentSpeedLevel").GetComponent<TMP_Text>();

        currentIncomeMultiplierText = GameObject.Find("CurrentIncomeMultiplier").GetComponent<TMP_Text>();
        IncomeMultiplierLevelText = GameObject.Find("CurrentIncomeMultiplierLevel").GetComponent<TMP_Text>();


        moneyText = GameObject.Find("Money").GetComponent<TMP_Text>();

        //Finds the player GameObject
        //Then finds the PlayerController Script
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        playerMoney = playerControllerScript.getPlayerMoney();
        levelArray[0] = playerControllerScript.getHealthLevel();
        levelArray[1] = playerControllerScript.getHealthRegenerationLevel();
        levelArray[2] = playerControllerScript.getDamageLevel();
        levelArray[3] = playerControllerScript.getDamageProtectionLevel();
        levelArray[4] = playerControllerScript.getSpeedLevel();
        levelArray[5] = playerControllerScript.getIncomeMultiplierLevel();


        weaponHitDetection = GameObject.Find("attackPoint");
        weaponControllerScript = weaponHitDetection.GetComponent<weaponHitDetection>();

        //Looks for the pause menu GameObject then assignts it a variable, otherwise prints an error message
        if (PauseMenuObject != null)
        {
            print("PauseMenu object found!");
            PauseMenu = PauseMenuObject.GetComponent<Canvas>();
        }
        else if (PauseMenuObject == null)
        {
            print("PauseMenu object not found!");
        }

        //Looks for the pause menu GameObject then assignts it a variable, otherwise prints an error message
        if (upgradeMenuObject != null)
        {
            print("upgradeMenu object found!");
            upgradeMenu = upgradeMenuObject.GetComponent<Canvas>();
        }
        else if (upgradeMenuObject == null)
        {
            print("upgradeMenu object not found!");
        }





        if (BrightnessSliderObject != null)
        {
            print("BrightnessSlider object found!");
            BrightnessSlider = BrightnessSliderObject.GetComponent<Slider>();
        }
        else if (BrightnessSliderObject == null)
        {
            print("BrightnessSlider object not found!");
        }

        print("Menu Controller Started!");
    }

    // Update is called once per frame
    void Update()
    {
        //print("Current: " + playerControllerScript.getMaxHealth());
        playerMoney = playerControllerScript.getPlayerMoney();
        levelArray[0] = playerControllerScript.getHealthLevel();
        levelArray[1] = playerControllerScript.getHealthRegenerationLevel();
        levelArray[2] = playerControllerScript.getDamageLevel();
        levelArray[3] = playerControllerScript.getDamageProtectionLevel();
        levelArray[4] = playerControllerScript.getSpeedLevel();
        levelArray[5] = playerControllerScript.getIncomeMultiplierLevel();


        currentHealthText.text = "Current: " + playerControllerScript.getMaxHealth();
        moneyText.text = "| Money: " + playerMoney;

        if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.enabled == false)
        {
            //print("Menu opened");
            PauseMenu.enabled = true;

            
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.enabled == true)
        {
            //print("Menu closed");
            PauseMenu.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.U) && upgradeMenu.enabled == false)
        {
            print("Menu opened");
            upgradeMenu.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.U) && upgradeMenu.enabled == true)
        {
            print("Menu closed");
            upgradeMenu.enabled = false;
        }


        if (BrightnessSliderObject == null)
        {
            BrightnessSliderObject = GameObject.Find("BrightnessSlider");

            if (BrightnessSliderObject != null)
            {
                print("BrightnessSlider object found!");
                BrightnessSlider = BrightnessSliderObject.GetComponent<Slider>();
            }
            else if (BrightnessSliderObject == null)
            {
                //print("BrightnessSlider object not found!");
            }
        }

        if (PauseMenuObject == null && currentLevel == "Level1")
        {
            PauseMenuObject = GameObject.Find("PauseMenu");

            if (PauseMenuObject != null)
            {
                print("PauseMenu object found!");
                PauseMenu = PauseMenuObject.GetComponent<Canvas>();
            }
            else if (PauseMenuObject == null)
            {
                print("PauseMenu object not found!");
            }
        }
    }
}
