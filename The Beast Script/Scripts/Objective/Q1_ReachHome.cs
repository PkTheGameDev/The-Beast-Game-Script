using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Q1_ReachHome : MonoBehaviour
{
    //Reference to Quest Script
    public Quests quest;

    //Ref to Player Controller
    public Player_Controller Player;

    //To Set Quest Completed and Activate the next quest
    public bool isCompleted { get; private set; }
    bool GotTriggered = false;

    //Collider to reach Player reached Place
    public GameObject HintPlace;
    public GameObject FinalPlace, SpawnEnemy1, MapIcon;

    public Q_ReachObjective QReachPlace;
    public Weapon_Manager wp_Manager;
    public GameManager SGameManager;

    public GameObject NextQuestObj;

    //To Show What quest on UI
    public GameObject QuestPanel;
    public TMP_Text Title;
    public TMP_Text description;
    public TMP_Text Objective1;

    private void Start()
    {
        QReachPlace = GetComponent<Q_ReachObjective>();
        HintPlace.SetActive(false);
        SpawnEnemy1.SetActive(false);
        FinalPlace.SetActive(false);
        QuestPanel.SetActive(false);
        MapIcon.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player_Controller>();

        if(other.CompareTag("Player") && !GotTriggered)
        {
            quest.IsActive = true;
            //To Show What quest on UI
            QuestPanel.SetActive(true);
            Title.text = quest.Title;
            description.text = quest.Description;
            Objective1.text = quest.Objective1;
            SetQuestToPl();
            Debug.Log("Quest");
            HintPlace.SetActive(true);
            SpawnEnemy1.SetActive(true);
            MapIcon.SetActive(false);
            SGameManager.GetComponent<SaveLoad>().SaveGame();
            GotTriggered = true;
        } 
    }

    void SetQuestToPl()
    {
        //Adds Quest to Player
        Player.quest = quest;
    }

    public void OnQuestCompletion()
    {
        isCompleted = true;
        quest.IsActive = false;
        NextQuestObj.SetActive(true);
        Destroy(gameObject);
    }
}

