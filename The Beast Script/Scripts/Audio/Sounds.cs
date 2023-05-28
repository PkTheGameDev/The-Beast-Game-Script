using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string Name; 

    public AudioClip Sound;

    [Range(0f, 2f)]
    public float Volume;

    [Range(0.1f, 3f)]
    public float Pitch;

    [HideInInspector]
    public AudioSource ASource;
}
