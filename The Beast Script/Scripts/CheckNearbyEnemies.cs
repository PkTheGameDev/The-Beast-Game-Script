using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckNearbyEnemies : MonoBehaviour
{
    public Transform GizmoTransform;
    public float GSpRadius = 10f;
    public LayerMask Enemy;
    public GameObject WayBlock;
    public GameObject FinalCutscene, Player;

    public HintText HintECount;
    public TMP_Text TextBox;
    public GameObject HintPanel;

    public Q8_FinalShowdown Quest8;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        FinalCutscene.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Collider[] Enemies = Physics.OverlapSphere(GizmoTransform.position, GSpRadius, Enemy);
        int EnmyCount = Enemies.Length;
        if (EnmyCount > 0)
        {   
            WayBlock.SetActive(true);
        }
        else
        {
            WayBlock.SetActive(false);

            Player.GetComponent<Player_Controller>().enabled = false;
            Player.GetComponent<Player_Controller>().CamTransform.gameObject.SetActive(false);
            Player.GetComponent<Player_Controller>().PlayerUi.SetActive(false);
            FinalCutscene.SetActive(true);

            StartCoroutine(WaitandPlayCredit());
        }
    }

    IEnumerator WaitandPlayCredit()
    {
        yield return new WaitForSeconds(18f);
        Destroy(FinalCutscene, 0.2f);
        Player.GetComponent<Player_Controller>().enabled = true;
        Player.GetComponent<Player_Controller>().CamTransform.gameObject.SetActive(true);
        Player.GetComponent<Player_Controller>().PlayerUi.SetActive(false);
        
        Destroy(gameObject);

        Debug.Log("18 secs");
        Quest8.OnQuestCompletion();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GizmoTransform.position, GSpRadius);
    }
}
