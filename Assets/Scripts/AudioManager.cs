using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource clickSound;

    void Start() 
    {
        clickSound = GameObject.Find("SFX Holder").GetComponent<AudioSource>();
    }

    public void ClickAudio()
    {
        clickSound.Play();
    }
}
