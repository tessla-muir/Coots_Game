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
    int[] tempos = {123, 172, 83, 113};
    int[] arrowsToSpawn = {289, 289, 289, 289};
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
    }

    public void StartBongoGame(int level) 
    {
        // Set active
        BongoGameManager.instance.gameObject.SetActive(true);
        bongoUI.SetActive(true);

        // Start values
        bs.Setup(tempos[level]);

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
