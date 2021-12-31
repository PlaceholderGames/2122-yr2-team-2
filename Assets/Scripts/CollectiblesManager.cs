using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class CollectiblesManager : MonoBehaviour
{
    private float waitTime = 30f;
    private bool reloading = false;
    private string scriptName = "PlayerController";
    PlayerController script = null;
    [SerializeField] private int healAmount = 30;
    [SerializeField] GameObject collectible = null;
    SphereCollider collectibleSphereCollider = null;
    MeshRenderer collectibleMeshRenderer = null;
    GameObject player = null;
    



    //Gets the player GameObject from the collision
    //Next it gets the playerController script from the player
    //Then it calls the heal function from the script.
    //Finally it changes the neccesary variables so that the reloading process can take place
    void OnTriggerEnter(Collider other)
    {
        player = other.gameObject;
        script = player.GetComponent<PlayerController>();
        script.heal(healAmount);

        collectibleSphereCollider.enabled = false;
        collectibleMeshRenderer.enabled = false;

        reloading = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        collectible = this.gameObject;
        collectibleSphereCollider = collectible.GetComponent<SphereCollider>();
        collectibleMeshRenderer = collectible.GetComponent<MeshRenderer>();
    }

    void reload()
    {
        collectibleSphereCollider.enabled = true;
        collectibleMeshRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {
            waitTime -= Time.deltaTime;
        }
        
        //print("waitTime" + waitTime);

        if (waitTime <= 0.0f)
        {
            if (reloading)
            {
                reloading = false;
                reload();
                waitTime = 30f;
            }
        }
    }
}

    //Reloads the collectible, allowing it to be used again.  


