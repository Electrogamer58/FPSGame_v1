﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    public bool isPlaying = false;

    AudioSource _audioSource;

    private void Awake()
    {
        #region Singleton Pattern (Simple)
        if (Instance == null)
        {
            //doesnt exist yet, so this is now our AudioManager
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //fill references
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    public void PlaySong(AudioClip clip)
    {
        _audioSource.clip = clip;
        if (!isPlaying)
        {
           _audioSource.Play();
        } else if (isPlaying)
        {
            _audioSource.UnPause();
        }
        
    }
}
