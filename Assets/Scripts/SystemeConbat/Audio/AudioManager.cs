using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager Instance;

       
    private void Awake()
    {
        Instance = this;
        foreach (Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    private void Start()
    {
        //Play("Theme");
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        s.Source.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //PlaySeveral("Bak_Hover", 5);
        }
    }

    public void PlaySeveral(string name, int numberOfSounds)
    {
        int rand = Random.Range(1, numberOfSounds);
        name += rand;
        //print("Son : " + name);
        Sound s = Array.Find(Sounds, Sound => Sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        s.Source.Play();
    }
}
