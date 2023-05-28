using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Q2_SaveMimo : MonoBehaviour
{
    public Quests quest;

    public Player_Controller PlayerCtrl;
    public bool isCompleted { get; private set; }
    bool GotTriggered = false;

    public GameObject Mimo, RoseNpc, ReachMimo, ReachWizard, Wizard, MapIcon, MimoIcon;

    public GameObject NextObjective;
    public GameObject[] PatrolEnemy;
    public GameManager SGameManager;

    //To Show What quest on UI
    public GameObject QuestPanel;
    public TMP_Text Title;
    public TMP_Text description;
    public TMP_Text Objective1;
    public TMP_Text Objective2;

    private void Start()
    {
        if (!this.quest.IsActive)
        {
            gameObject.SetActive(false);
        }

        Mimo.SetActive(false);
        ReachMimo.SetActive(false);
        ReachWizard.SetActive(false);
        Wizard.SetActive(false);
        MapIcon.SetActive(true);
        //MimoIcon.SetActive(false);
        //NextObjective.SetActive(false);       
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player_Controller>();

        if (other.CompareTag("Player"))
        {
            MapIcon.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Player_Controller>();

        if(other.CompareTag("Player") && !GotTriggered)
        {
            if(RoseNpc.GetComponent<Dialogue>().enabled)
            {
                quest.IsActive = true;
                SetQuestToPlayer();
                Mimo.SetActive(true);
                ReachMimo.SetActive(true);
                Title.text = quest.Title;
                description.text = quest.Description;
                Objective1.text = quest.Objective1;
                Objective2.text = quest.Objective2;
                QuestPanel.SetActive(true);
                GotTriggered = true;
                MapIcon.SetActive(false);
                //MimoIcon.SetActive(false);
                SGameManager.GetComponent<SaveLoad>().SaveGame();

                int j = PatrolEnemy.Length;
                for (int i = 0; i < j; i++)
                {
                    PatrolEnemy[i].SetActive(true);
                }
            }            
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
        NextObjective.SetActive(true);
        Objective2.text = "";
        Destroy(gameObject, 8f);
        Debug.Log("7 secs");
    }
}
