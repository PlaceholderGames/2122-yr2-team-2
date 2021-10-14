using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorTwo : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (GameVariables.theScore == 2)
        {
            Destroy(gameObject);
        }

    }
}
