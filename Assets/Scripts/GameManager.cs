using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    GameObject bongoUI;
    GameObject bongoGameManager;
    BeatScroller bs;
    AudioSource[] music;
    [SerializeField] GameObject[] arrows;
    int[] tempos = {172, 172};

    void Start()
    {
        instance = this;

        // Inactive at start
        bongoUI = GameObject.Find("Bongo UI");
        bongoUI.SetActive(false);
        BongoGameManager.instance.gameObject.SetActive(false);

        // Get music
        bs = GameObject.FindObjectOfType<BeatScroller>();
        music = GameObject.Find("Music Holder").GetComponents<AudioSource>();
    }

    public void StartBongoGame(int level)
    {
        // Start values
        BongoGameManager.instance.gameObject.SetActive(true);
        bongoUI.SetActive(true);

        // Update on level
        BongoGameManager.instance.SetMusic(music[level]);
        bs.Setup(arrows[level], tempos[level]);
    }

    public AudioSource[] GetMusic()
    {
        return music;
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
