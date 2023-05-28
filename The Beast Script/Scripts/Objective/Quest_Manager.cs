using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour
{
    public Quests ActiveQuest;

    //List<Quests> Quest = new List<Quests>();
    public GameObject[] QuestObjs;

    public Quests[] AllQuest;
    public bool IsCompleted { get; private set; }

    int OnCompletion = 0;

    private void Start()
    {
        Collider col;
        int j = QuestObjs.Length;
        for(int i = 0; i < j; i++)
        {
            if(i == 0)
            {
                QuestObjs[i].SetActive(true);
                col = QuestObjs[i].GetComponent<Collider>();
                col.GetComponent<Player_Controller>().quest = ActiveQuest;
                Debug.Log(ActiveQuest.Title);
                //ActiveQuest.IsActive = true;
            }
        }
    }

    bool IsQuestComplete()
    {
        if(IsCompleted == true)
        {
            //if quest is completed move to the next one
            OnCompletion++;
        }
        else
        {
            //Active Quest true
        }

        return IsCompleted;
    }
}

 //Add Quest to List
 //Only one quest at a time. Completing one quest Activates or instantiates the next quest trigger

