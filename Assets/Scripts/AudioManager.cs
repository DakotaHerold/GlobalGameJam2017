using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    public AudioSource musicSource;

    public AudioClip backgroundClip;

	// Use this for initialization
	void Awake () 
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = backgroundClip;
        musicSource.Play();
	}
}
