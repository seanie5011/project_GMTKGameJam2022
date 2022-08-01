using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] // makes class usable in inspector
public class Sound
{
    public string name; // what we want to call it

    public AudioClip clip; // the mp3 or wav or whatever

    // parameters
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    public bool loop;

    [HideInInspector]
    public AudioSource source; // assigns an audio source 
}