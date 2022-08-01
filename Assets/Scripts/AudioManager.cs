using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; // list of sound objects (from class)

    public static AudioManager instance; // an instance of this object

    // Awake is called before Start
    void Awake()
    {
        // check if there is already an audiomanager (only want one), if not assign to this
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // destroy this one
            return;
        }

        DontDestroyOnLoad(gameObject);

        // assign each sound in the sound list
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>(); // get the audiosource from them

            // assign all parameters
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Play music and stuff from here
        Play("MusicLoop");
    }

    // call in other scripts to play sound
    public void Play(string name)
    {
        // using System, array is what it sounds like, find searches for a particular element in an array (first parameter), that meets the search criteria (second parameter)
        Sound s = Array.Find(sounds, sound => sound.name == name); // sees if a given sound in sounds has the same name as string name

        // make sure we dont cause an error if sound not found
        if (s == null)
        {
            Debug.LogWarning("Sound: '" + name + "' not found!");
            return;
        }
        
        // play the sound
        s.source.Play();
    }
}
