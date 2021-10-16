using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoringScript : MonoBehaviour
{

    public GameObject scoreText;
    

    void OnTriggerEnter(Collider other)
    {
        GameVariables.theScore += 1;
        scoreText.GetComponent<Text>().text = "Pieces collected: " + GameVariables.theScore;
        Destroy(gameObject);

        if(GameVariables.theScore == 4)
        {
            SceneManager.LoadScene("WinMenu", LoadSceneMode.Additive);
        }

    }

}
