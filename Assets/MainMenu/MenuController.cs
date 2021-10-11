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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
