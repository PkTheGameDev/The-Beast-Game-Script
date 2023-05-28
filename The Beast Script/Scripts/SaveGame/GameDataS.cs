using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player Data and overall gamedata
//Player + Collecctible data in a single class
//Adding serialized collectible data in a list and storing it
[System.Serializable]
public class GameDataS
{
    public Vector3 PlayerPosition;

    public Quaternion PlRotation;

    public float Health;

    public int QuestId;

    public string QuestTitle;

    public bool QuestActive;

    public int CollectibleCount;

    public List<CollectibleData> CollData = new List<CollectibleData>();
}

//Serializing collectible data with a Id and a bool
//if the Id exist and bool is true when loading - the gameobject is set inactive
[System.Serializable]
public class CollectibleData
{
    public int Coll_Id;
    public bool WasCollected;

    public CollectibleData(Collectible Collect)
    {
        Coll_Id = Collect.C_Id;
        WasCollected = Collect.IsCollected;
    }
}