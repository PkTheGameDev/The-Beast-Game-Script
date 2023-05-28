using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject DialogPanel;
    public TMP_Text TextDisplay;
    public string[] Sentences;
    private int Index;
    public float TypingSpeed;
    public GameObject ContinueTxt;
    public Player_Controller PlayerCtl;
    public AudioClip PopSound;
    private AudioSource DiagSnd;
    
   

    private void Awake()
    {
        this.enabled = false;
        DialogPanel.SetActive(false);
    }

    private void OnEnable()
    {
        var Wizard = this.GetComponent<NPC_CheckPl>();
        DiagSnd = this.GetComponent<AudioSource>();

        StartCoroutine(Type());
        DialogPanel.SetActive(true);
        PlayerCtl.enabled = false;
        PlayerCtl.PlAnimator.enabled = false;
        Wizard.Prompt.SetActive(false);
    }

    private void Update()
    {
        if(TextDisplay.text == Sentences[Index])
        {
            ContinueTxt.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                NextSentence();
            }            
        }        
    }

    IEnumerator Type()
    {
        foreach(char letter in Sentences[Index].ToCharArray())
        {
            TextDisplay.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }        
    }

    public void NextSentence()
    {
        DiagSnd.clip = PopSound;
        DiagSnd.Play();
        ContinueTxt.SetActive(false);
        
        if (Index < Sentences.Length - 1)
        {
            Index++;
            TextDisplay.text = "";
            StartCoroutine(Type());
            
        }
        else
        {
            TextDisplay.text = "";
            DialogPanel.SetActive(false);
            PlayerCtl.enabled = true;
            PlayerCtl.PlAnimator.enabled = true;
            ContinueTxt.SetActive(false);
        }
    }
}
