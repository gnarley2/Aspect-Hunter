using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource audio;
    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

        audio = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }
}
