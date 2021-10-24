using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update

    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] public string currentLevel;

    [SerializeField] Canvas PauseMenu = null;

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

    GameObject PauseMenuObject = null;
    GameObject BrightnessSliderObject = null;

    void Start()
    {
        PauseMenuObject = GameObject.Find("PauseMenu");//the pause menu object
        BrightnessSliderObject = GameObject.Find("BrightnessSlider");
        currentLevel = SceneManager.GetActiveScene().name;

        if (PauseMenuObject != null)
        {
            print("PauseMenu object found!");
            PauseMenu = PauseMenuObject.GetComponent<Canvas>();
        }
        else if (PauseMenuObject == null)
        {
            print("PauseMenu object not found!");
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

        if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.enabled == false)
        {
            print("Menu opened");
            PauseMenu.enabled = true;

            
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.enabled == true)
        {
            print("Menu closed");
            PauseMenu.enabled = false;

            
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
                print("BrightnessSlider object not found!");
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
