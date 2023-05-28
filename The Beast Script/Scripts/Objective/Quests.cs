using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quests
{
    public int QuestId;

    //Quest Title
    public string Title;

    //Description
    public string Description;

    public string Objective1;

    public string Objective2;

    public string Objective3;

    //Condition to check Active or not
    public bool IsActive = false;
}
