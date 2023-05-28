using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectsAll : MonoBehaviour
{
    //Reference to quest gameobjects
    public GameObject[] AllQuest;

    private void Start()
    {
        int j = AllQuest.Length;
        for (int i = 0; i < j; i++)
        {
            AllQuest[i].SetActive(false);
        }

        AllQuest[0].SetActive(true);
    }

    public void NewGame()
    {
        Start();
    }
}
