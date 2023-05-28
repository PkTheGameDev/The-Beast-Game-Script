using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadLoreManager : MonoBehaviour
{
    public Button[] LoreBtns;

    public GameObject ReadLoreObj, CloseBtn, LoreCloseBtn;

    public GameObject[] OpenLoreCs;

    public Player_Controller PlayerCtrl;

    public int ButtonId;

    [HideInInspector] public bool IsLoreCollected = false;

    void Start()
    {
        //Sets All buttons for lore to false if nothing is collected
        int j = LoreBtns.Length;
        for (int i = 0; i < j; i++)
        {
            LoreBtns[i].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Opens the Lore Panel and sets the respective close button
        //Disables the Player Controller and Animator
        if (Input.GetKeyDown(KeyCode.J))
        {
            ReadLoreObj.SetActive(true);            
            CloseBtn.SetActive(true);
            PlayerCtrl.enabled = false;
            PlayerCtrl.PlAnimator.enabled = false;
            Debug.Log("J Pressed");
        }

        //Closes the panel with Escape button and sets Player controller and animator active
        if(ReadLoreObj.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerCtrl.enabled = true;
                PlayerCtrl.PlAnimator.enabled = true;
                ReadLoreObj.SetActive(false);
                CloseBtn.SetActive(false);
                LoreCloseBtn.SetActive(false);

                int j = OpenLoreCs.Length;
                for (int i = 0; i < j; i++)
                {
                    if (OpenLoreCs[i].activeSelf)
                    {
                        OpenLoreCs[i].SetActive(false);
                        Debug.Log("Set Canvas InActive");
                    }
                }
            }
        }        
    }
}
