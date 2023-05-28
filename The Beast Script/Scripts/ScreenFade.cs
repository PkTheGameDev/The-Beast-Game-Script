using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    public Animator FadeAnimator;


    public void ScFadeOut()
    {
        FadeAnimator.SetTrigger("FadeOut");
        Debug.Log("Fade out");
    }
}
