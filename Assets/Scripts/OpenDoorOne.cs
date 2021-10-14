using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOne : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if(GameVariables.theScore == 1)
        {
            Destroy(gameObject);
        }
        
    }
}
