using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //integer value for health
    public int Health = 10;
    Player_Controller Player;

    private void Start()
    {
        //Gets Player Controller
        Player = GetComponent<Player_Controller>();
    }

    private void OnEnable()
    {
        //if Game object is enabled on scene starts a coroutine - disable Health obj
        StartCoroutine(DisableAfter());
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player_Controller>();

        //Checks whether the collided object is Player
        if(other.CompareTag("Player"))
        {
            //Finds Player Object on Scene and Gets Player Controller to access Health
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controller>();

            //Checks if Player's Current Health is lower than MaxHealth and Adds Health to Player
            if (Player.CurrentHealth < Player.MaxHealth)
            {
                if (Player != null)
                {
                    Player.AddHealth(Health);
                    Destroy(gameObject);
                }
            }
        }        
    }

    //Disables Gameobject after 15 secs.
    IEnumerator DisableAfter()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
}
