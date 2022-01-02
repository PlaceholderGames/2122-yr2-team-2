using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update


    int healthLevelsChange = 0;
    int healthLevel = 0;
    float healthLevelCost = 100;
    float playerMoney = 1000;


    //This is where the players object and scripts are
    GameObject player = null;
    PlayerController playerControllerScript = null;

    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] public string currentLevel;

    [SerializeField] Canvas PauseMenu = null;
    [SerializeField] Canvas upgradeMenu = null;
    TMP_Text currentHealthText = null;
    TMP_Text HealthLevelText = null;
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


    //The healthLevelsChange variable is what get's incremented by the UI
    //If the player decides to confirm the changes made then this value is used when calling
    //the changeHealthLevel() function in the PlayerController.
    //It is then set to 0
    string healthLevelString = "";
    public void changeHealthLevelChange(int change)
    {
        healthLevelsChange += change;

        if (healthLevelsChange < 0)
        {
            healthLevelsChange = 0;
            
        }

        HealthLevelText.text = "Level: " + healthLevel + " + " + healthLevelsChange;
    }


    float totalSpent = 0;
    public void addHealthLevels()
    {
        totalSpent = healthLevelsChange * healthLevelCost;
        if (playerMoney - (healthLevelsChange * healthLevelCost) >= 0 && healthLevelsChange > 0)
        {
            playerControllerScript.changeHealthLevel(healthLevelsChange);
            healthLevelsChange = 0;
            playerControllerScript.spendPlayerMoney(totalSpent);
            HealthLevelText.text = "Level: " + playerControllerScript.getHealthLevel() + " + 0";
            currentHealthText.text = "Current: " + playerControllerScript.getMaxHealth();
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
        HealthLevelText = GameObject.Find("CurrentLevel").GetComponent<TMP_Text>();
        moneyText = GameObject.Find("Money").GetComponent<TMP_Text>();

        //Finds the player GameObject
        //Then finds the PlayerController Script
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        playerMoney = playerControllerScript.getPlayerMoney();
        healthLevel = playerControllerScript.getHealthLevel();

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
        healthLevel = playerControllerScript.getHealthLevel();
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
