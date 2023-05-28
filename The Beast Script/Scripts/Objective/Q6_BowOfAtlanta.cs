using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Q6_BowOfAtlanta : MonoBehaviour
{
    public Quests quest;

    public bool isCompleted { get; private set; }
    bool GotTriggered = false;

    public Player_Controller PlayerCtrl;

    public GameObject ReachBow, Wizard, GateBlock, MapIcon, BowIcon;
    public GameObject[] PatrolEnemy;
    public GameManager SGameManager;
    public GameObject NextQuestObj, QuestPanel;

    public TMP_Text Title;
    public TMP_Text description;
    public TMP_Text Objective1;

    private void Start()
    {
        if (!this.quest.IsActive)
        {
            gameObject.SetActive(false);
        }
        MapIcon.SetActive(true);
        ReachBow.SetActive(false);
        BowIcon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player_Controller>();

        if (other.CompareTag("Player") && !GotTriggered)
        {
            quest.IsActive = true;
            SetQuestToPlayer();
            Title.text = quest.Title;
            description.text = quest.Description;
            Objective1.text = quest.Objective1;
            QuestPanel.SetActive(true);
            MapIcon.SetActive(false);
            ReachBow.SetActive(true);
            BowIcon.SetActive(true);
            SGameManager.GetComponent<SaveLoad>().SaveGame();
            GotTriggered = true;
        }
    }

    void SetQuestToPlayer()
    {
        PlayerCtrl.quest = quest;
    }

    public void OnQuestCompletion()
    {
        isCompleted = true;
        quest.IsActive = false;
        NextQuestObj.SetActive(true);
        GateBlock.SetActive(false);
        int j = PatrolEnemy.Length;
        for (int i = 0; i < j; i++)
        {
            PatrolEnemy[i].SetActive(true);
        }
        Destroy(gameObject, 1f);
    }
}
