using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest_Ui : MonoBehaviour
{
    //sets Quest Panel to false
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    //On enabled run co-routine 
    private void OnEnable()
    {
        StartCoroutine(WaitAndDisable());
    }

    //Disable panel after 5 secs
    IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(6f);
        this.gameObject.SetActive(false);   
    }
}
