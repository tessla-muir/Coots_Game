using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource[] sounds;

    void Awake() 
    {
        instance = this;
    }

    void Start() 
    {
        sounds = GameObject.Find("SFX Holder").GetComponents<AudioSource>();
    }

    public void ClickAudio()
    {
        sounds[Random.Range(0, 3)].Play();
    }

    public AudioSource GetRandomCheer()
    {
        return sounds[Random.Range(3,5)];
    }

    public AudioSource GetRandomBoo()
    {
        return sounds[5];
    }
}
