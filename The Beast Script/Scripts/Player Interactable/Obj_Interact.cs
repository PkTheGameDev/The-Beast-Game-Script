using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Obj_Interact : MonoBehaviour
{
    public Player_Controller PlayerCtrl;

    public ReadLoreManager LoreBtnsManage;

    public GameObject Prompt, LoreCanvas;

    public Transform GizmoTr;

    public LayerMask PlayerLayer;

    public int MyId;

    public HintText Hinttext;

    public TMP_Text TxtBox;
    public GameObject HintPanel;

    // Start is called before the first frame update
    void Start()
    {
        //Prompt and Canvas is set to false 
        LoreCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for Player with Raycast, if Player is in range the prompt is set active 
        Collider[] Player = Physics.OverlapSphere(GizmoTr.position, 2f, PlayerLayer);

        int j = Player.Length;
        if(j > 0)
        {
            //With Input E pressed and Canvas is not active - open the respective lore object Image
            if (Input.GetKeyDown(KeyCode.E) && !LoreCanvas.activeSelf)
            {
                LoreCanvas.SetActive(true);
                
                StartCoroutine(WaitAndDisable());                

                if (gameObject.name == "Lore Stone 1")
                {
                    LoreBtnsManage.LoreBtns[0].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 2")
                {
                    LoreBtnsManage.LoreBtns[1].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 3")
                {
                    LoreBtnsManage.LoreBtns[2].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 4")
                {
                    LoreBtnsManage.LoreBtns[3].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 5")
                {
                    LoreBtnsManage.LoreBtns[4].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 6")
                {
                    LoreBtnsManage.LoreBtns[5].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 7")
                {
                    LoreBtnsManage.LoreBtns[6].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 8")
                {
                    LoreBtnsManage.LoreBtns[7].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 9")
                {
                    LoreBtnsManage.LoreBtns[8].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 10")
                {
                    LoreBtnsManage.LoreBtns[9].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 11")
                {
                    LoreBtnsManage.LoreBtns[10].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
                if (gameObject.name == "Lore Stone 12")
                {
                    LoreBtnsManage.LoreBtns[11].interactable = true;
                    LoreBtnsManage.IsLoreCollected = true;
                }
            }
            //if lore Image is Active prompt is set inactive
            if (LoreCanvas.activeSelf)
            {
                Prompt.SetActive(false);
            }
        }
    }

    //Disable Lore Image after 5 secs
    IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(5f);
        LoreCanvas.SetActive(false);

        TxtBox.text = "Press J to open Lore Panel";
        if(!LoreCanvas.activeSelf && !HintPanel.activeSelf)
        {
            HintPanel.SetActive(true);
        }        
    }
}
