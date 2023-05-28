using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadObjInfo : MonoBehaviour
{
    public string[] Tags;
    public string FileName = "First";

    //private void Start()
    //{
    //    ReadObjectInfo();
    //}

    public void ReadObjectInfo()
    {
        int j = Tags.Length;

        List<StoredDataFormat> storedDataFormat = new List<StoredDataFormat>();

        for (int i = 0; i < j; i++)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(Tags[i]);

            int y = gameObjects.Length;
            if (y > 0)
            {
                for (int x = 0; x < y; x++)
                {
                    Vector3 pos = gameObjects[x].transform.position;
                    Quaternion rot = gameObjects[x].transform.rotation;

                    storedDataFormat.Add(new StoredDataFormat(Tags[i], pos, rot));
                }
            }            
        }

        ProcedurallDatA proceduralData = new ProcedurallDatA(storedDataFormat.ToArray());

        string data = JsonUtility.ToJson(proceduralData);
        System.IO.File.WriteAllText(Application.dataPath + "/Scripts/Pooling/Data/" + FileName + ".json", data);
    }
}
