using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnThirdLightOn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameVariables.theScore == 3)
        {
            this.GetComponent<Light>().enabled = true;
        }



    }
}
