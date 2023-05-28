using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
    public int C_Id;

    public CollectibleData C_Data;
    public bool IsCollected { get; set; }

    private void Start()
    {
        C_Data.Coll_Id = C_Id;        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player_Controller Player;
        other.TryGetComponent<Player_Controller>(out Player);
        C_Data.WasCollected = true;
        C_Data.Coll_Id = C_Id;
        IsCollected = true;
        Player.AddCollectible();
        gameObject.SetActive(false);
    }

    //Serialized Data of Collectible - when Load Game is Done This method is Runned to check whether the object is 
    //Collected and sets it inactive
    public void LoadGame(CollectibleData collectData)
    {
        C_Data = collectData;
        gameObject.SetActive(!C_Data.WasCollected);
        
        Debug.Log("Load Coll data set false");
    }
}
