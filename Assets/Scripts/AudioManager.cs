using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource[] clickSounds;

    void Start() 
    {
        clickSounds = GameObject.Find("SFX Holder").GetComponents<AudioSource>();
    }

    public void ClickAudio()
    {
        Debug.Log(clickSounds.Length);
        clickSounds[Random.Range(0, clickSounds.Length-1)].Play();
    }
}
