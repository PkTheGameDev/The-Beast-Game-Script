using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme_Music : MonoBehaviour
{
    //Singleton - sets the game object with this script as a single object and maintain 
    public static Theme_Music instance;

    private void Awake()
    {
        //checks if instance is null and uses this or if not null destroys the gameobject
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //Since i want to destroy this object on load is commented the below line.
        //if not the below will hold the gameobject and instances it on the next scene.
        // - on the next it checks for the instance - if any present it is destroyed and only one is maintained 
        //DontDestroyOnLoad(gameObject);        
    }
}
