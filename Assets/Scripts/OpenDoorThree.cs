using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorThree : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (GameVariables.theScore == 3)
        {
            Destroy(gameObject);
        }

    }
}
