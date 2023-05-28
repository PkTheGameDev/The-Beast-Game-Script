using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Cutscene : MonoBehaviour
{
    public GameObject Player, CutScCam1; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player.GetComponent<Player_Controller>().enabled = false;
            Player.GetComponent<AudioSource>().enabled = false;
            Player.GetComponent<Player_Controller>().CamTransform.gameObject.SetActive(false);
            CutScCam1.SetActive(true);
            StartCoroutine(PlayCutscene1());
        }
    }

    IEnumerator PlayCutscene1()
    {
        yield return new WaitForSeconds(7f);
        Debug.Log("Playing Cutscene");
        Destroy(CutScCam1, 1f);
        Player.GetComponent<Player_Controller>().enabled = true;
        Player.GetComponent<Player_Controller>().CamTransform.gameObject.SetActive(true);
        Player.GetComponent<AudioSource>().enabled = true;        
        Debug.Log("6 secs");
        
    }
}
