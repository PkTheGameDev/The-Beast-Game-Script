using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Q_ReachObjective : MonoBehaviour
{
    public GameManager SGameManager;
    public Transform DestroyRoot;
    public GameObject Mimo, WizardQ3;

    public HintText hintRef;
    public TMP_Text Hint;
    public GameObject HintObject;

    Q1_ReachHome Quest1;
    Q2_SaveMimo Quest2;
    Q3_TheCastle Quest3;
    Q4_WayOutCastle Quest4;
    Q5_SwordOfBolt Quest5;
    Q6_BowOfAtlanta Quest6;
    Q7_GreatSwdOfNoxus Quest7;
    Q8_FinalShowdown Quest8;

    // Start is called before the first frame update
    private void Start()
    {
        //Quest1 = GetComponent<Q1_ReachHome>();
        Quest1 = this.GetComponentInParent<Q1_ReachHome>();
        Quest2 = this.GetComponentInParent<Q2_SaveMimo>();
        Quest3 = this.GetComponentInParent<Q3_TheCastle>();
        Quest4 = this.GetComponentInParent<Q4_WayOutCastle>();
        Quest5 = this.GetComponentInParent<Q5_SwordOfBolt>();
        Quest6 = this.GetComponentInParent<Q6_BowOfAtlanta>();
        Quest7 = this.GetComponentInParent<Q7_GreatSwdOfNoxus>();
        Quest8 = this.GetComponentInParent<Q8_FinalShowdown>();
    }
    void Awake()
    {
        if(DestroyRoot == null)
        {
            DestroyRoot = transform;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        var Player = other.GetComponent<Player_Controller>();
        if(Player != null)
        {
            if (this.gameObject.name == "SpnEnemies1")
            {
                Quest1.FinalPlace.SetActive(true);
                SGameManager.GetComponent<SaveLoad>().SaveGame();
                Debug.Log("Spawn Place Reached");
                Destroy(gameObject);                
            }

            if (this.gameObject.name == "VillageGate")
            {
                Hint.text = hintRef.Hint;
                HintObject.SetActive(true);
                SGameManager.GetComponent<SaveLoad>().SaveGame();
                Debug.Log("Final Place Reached");
                Destroy(gameObject);
                Quest1.OnQuestCompletion();
                Debug.Log(Quest1.isCompleted);
            }

            if(this.gameObject.name == "ReachWizard")
            {
                Hint.text = hintRef.Hint;
                HintObject.SetActive(true);
                Quest2.ReachWizard.SetActive(true);
                SGameManager.GetComponent<SaveLoad>().SaveGame();
                Debug.Log("Wizard found");
                Quest2.OnQuestCompletion();
            }

            if (this.gameObject.name == "ReachCastle")
            {
                Hint.text = hintRef.Hint;
                HintObject.SetActive(true);
                SGameManager.GetComponent<SaveLoad>().SaveGame();
                Destroy(gameObject, 0.25f);
                Quest3.OnQuestCompletion();
                Debug.Log(Quest3.isCompleted);
            }

            if(this.gameObject.name == "CastleOut")
            {
                Hint.text = hintRef.Hint;
                HintObject.SetActive(true);
                SGameManager.GetComponent<SaveLoad>().SaveGame();
                Debug.Log("Castle Reached");
                Destroy(gameObject);
                Quest4.OnQuestCompletion();
            }

            if (this.gameObject.name == "SwordCollected")
            {
                Quest5.ActivateObjects();
                SGameManager.GetComponent<SaveLoad>().SaveGame();
            }
            if(this.gameObject.name == "SwordColTrigger")
            {
                Hint.text = hintRef.Hint;
                HintObject.SetActive(true);
                Destroy(gameObject);
                Quest5.OnQuestCompletion();
            }
            if(this.gameObject.name == "InIsland")
            {
                Hint.text = hintRef.Hint;
                HintObject.SetActive(true);
                SGameManager.GetComponent<SaveLoad>().SaveGame();
                Quest8.EnemySpIcon.SetActive(false);
                Quest8.checkEnemyObj();
                Debug.Log("Reached Inside Island");
                Destroy(gameObject);
            }

            if (this.gameObject.name == "BridgeGate")
            {
                Quest7.OnQuestCompletion();
                SGameManager.GetComponent<SaveLoad>().SaveGame();
            }

            if (this.gameObject.name == "Q6 Complete")
            {
                Quest6.BowIcon.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var Player = other.GetComponent<Player_Controller>();
        if (Player != null)
        {
            if (this.gameObject.name == "GoDirection")
            {
                Debug.Log("Show Hint");
                Hint.text = hintRef.Hint;
                HintObject.SetActive(true);
                Destroy(gameObject);
            }

            if (this.gameObject.name == "ReachMimo")
            {
                if(Mimo.GetComponent<Dialogue>().enabled)
                {
                    Hint.text = hintRef.Hint;
                    HintObject.SetActive(true);
                    Quest2.ReachWizard.SetActive(true);
                    Quest2.Wizard.SetActive(true);
                    Quest2.MimoIcon.SetActive(false);
                    SGameManager.GetComponent<SaveLoad>().SaveGame();
                    Debug.Log("Mimo found");
                }                
            }

            if(this.gameObject.name == "Q6 Complete")
            {
                if(WizardQ3.GetComponent<Dialogue>().enabled)
                {
                    Hint.text = hintRef.Hint;
                    HintObject.SetActive(true);
                    Quest6.OnQuestCompletion();
                    SGameManager.GetComponent<SaveLoad>().SaveGame();
                    Destroy(gameObject);
                    Debug.Log("Quest 6 is Complete");
                }                
            }

            if (this.gameObject.name == "ReachSword2")
            {
                Hint.text = hintRef.Hint;
                HintObject.SetActive(true);
                SGameManager.GetComponent<SaveLoad>().SaveGame();
                Debug.Log("Q7 Weapon Reached Meet Wizards");
                Destroy(gameObject);
                Quest7.WeaponIcon.SetActive(false);
                Quest7.BridgeGate.SetActive(true);
                //Quest7.BridgeIcon.SetActive(true);
                Quest7.Wizard.SetActive(true);
                Quest7.BridgeBlock.SetActive(false);
                
            }
        }
    }   
}
