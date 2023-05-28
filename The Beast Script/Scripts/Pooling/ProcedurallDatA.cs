using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//This Code access the stored Tag, position and rotation and stores it in an array
public class ProcedurallDatA 
{
    public StoredDataFormat[] StoredDataFormats;

    public ProcedurallDatA(StoredDataFormat[] storedDataFormats)
    {
        StoredDataFormats = storedDataFormats;
    }    
}

[System.Serializable]
//This code Stores the Tag, position and rotation
public class StoredDataFormat
{
    public string Tag;
    public Vector3 Pos;
    public Quaternion Rot;


    public StoredDataFormat(string tag, Vector3 pos, Quaternion rot)
    {
        Tag = tag;
        Pos = pos;
        Rot = rot;
    }
}

[System.Serializable]
//This code is to read the text asset data through gamemanager. We use PoolingObj in GM script and assign the 
// respective enemy prefab and give its tag in a que
public class PoolingObj
{
    public string Tag;
    public Queue<GameObject> EnemyObjts = new Queue<GameObject>();
    public GameObject Reference;
}
