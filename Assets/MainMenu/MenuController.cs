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

    [SerializeField] Canvas PauseMenu = null;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void ExitGameDialogYes()
    {
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }
        


    void Start()
    {
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
        print("Menu Controller Started!");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && PauseMenu.enabled == false)
        {
            print("Menu opened");
            PauseMenu.enabled = true;

            //These two lines allow the mouse to move and be seen by the player
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && PauseMenu.enabled == true)
        {
            print("Menu closed");
            PauseMenu.enabled = false;

            //These two lines locks the mouse to the middle of the screen and hides it from the player
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
