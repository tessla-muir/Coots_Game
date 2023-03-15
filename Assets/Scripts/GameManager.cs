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
    List<Transform> arrows = new List<Transform>();
    int[] tempos = {123, 172, 83};
    int currentLevel = 0;
    int difficulty = 0;

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        // Inactive at start
        bongoUI = GameObject.Find("Bongo UI");
        bongoUI.SetActive(false);
        BongoGameManager.instance.gameObject.SetActive(false);

        // Get music
        bs = GameObject.FindObjectOfType<BeatScroller>();
        music = GameObject.Find("Music Holder").GetComponents<AudioSource>();

        // Get arrows
        foreach (Transform child in GameObject.Find("Arrow Holder").transform)
        {
            arrows.Add(child.transform);
        }
    }

    public void StartBongoGame(int level) 
    {
        // Start values
        BongoGameManager.instance.gameObject.SetActive(true);
        bongoUI.SetActive(true);

        bs.Setup(arrows[level], tempos[level]);

        // Update on level
        currentLevel = level;
        BongoGameManager.instance.SetMusic(music[level]);
        BongoGameManager.instance.SetDifficulty(difficulty);
        BongoGameManager.instance.PlaceArrows();
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

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetDifficulty(int val)
    {
        difficulty = val;
    }
}
