using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource AudioSc;
    public Sounds[] Sound;

    private void Start()
    {
        AudioSc = GetComponent<AudioSource>();

        foreach (Sounds s in Sound)
        {
            s.ASource = GetComponent<AudioSource>();
            s.ASource.clip = s.Sound;
            s.ASource.volume = s.Volume;
            s.ASource.pitch = s.Pitch;
        }
    }

    public void PlaySound(string Name)
    {
        foreach(Sounds s in Sound)
        {
            if(s.Name == Name)
            {
                s.ASource.clip = s.Sound;
                s.ASource.volume = s.Volume;
                s.ASource.pitch = s.Pitch;
                s.ASource.Play();                
            }
        }
    }

    public void StepSound()
    {
        PlaySound("FootStep");
    }

    public void DeathSound()
    {
        PlaySound("Death");
    }
}
