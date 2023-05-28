using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Q4_WayOutCastle : MonoBehaviour
{
    public Quests quest;

    public bool isCompleted { get; private set; }
    bool GotTriggered = false;

    public Player_Controller PlayerCtrl;

    public GameObject ReachPlace, MapIcon, CastleoutIcon;
    public GameManager SGameManager;

    public GameObject NextQuestObj, QuestPanel;

    public TMP_Text Title;
    public TMP_Text description;
    public TMP_Text Objective1;

    // Start is called before the first frame update
    void Start()
    {
        if (!this.quest.IsActive)
        {
            gameObject.SetActive(false);
        }
        MapIcon.SetActive(true);
        CastleoutIcon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player_Controller>();

        if (other.CompareTag("Player") && !GotTriggered)
        {
            quest.IsActive = true;
            SetQuestToPlayer();
            ReachPlace.SetActive(true);
            Title.text = quest.Title;
            description.text = quest.Description;
            Objective1.text = quest.Objective1;
            QuestPanel.SetActive(true);
            MapIcon.SetActive(false);
            CastleoutIcon.SetActive(true);
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
        Destroy(gameObject, 2f);
    }
}
