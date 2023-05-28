using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Q8_FinalShowdown : MonoBehaviour
{
    public Quests quest;

    public bool isCompleted { get; private set; }
    bool GotTriggered = false;

    public Player_Controller PlayerCtrl;

    public GameObject EnemySpn, Wizard, Wayblock, MapIcon, EnemySpIcon, CheckEnemy;
    public GameManager SGameManager;

    public GameObject ScreenFade;
    public GameObject QuestPanel;

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
        CheckEnemy.SetActive(false);
        EnemySpn.SetActive(false);
        MapIcon.SetActive(true);
        Wayblock.SetActive(false);
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
            EnemySpn.SetActive(true);
            EnemySpIcon.SetActive(true);
            MapIcon.SetActive(false);
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
        ScreenFade.SetActive(true);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        if(isCompleted)
        {
            SGameManager.GameComplete();
        }        
    }

    public void checkEnemyObj()
    {
        StartCoroutine(setCheckEnActive());
    }

    IEnumerator setCheckEnActive()
    {
        yield return new WaitForSeconds(2f);
        CheckEnemy.SetActive(true);
        Debug.Log("2 secs");
    }
}
