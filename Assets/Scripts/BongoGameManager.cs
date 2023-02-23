using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BongoGameManager : MonoBehaviour
{
    public static BongoGameManager instance;

    // Music
    bool startPlaying = false;
    bool musicEnded = false;
    bool paused = false;
    float dspSongTime;
    float startPauseTime = 0;
    float endPauseTime = 0;
    float totalPauseTime = 0;
    [SerializeField] AudioSource music;
    [SerializeField] BeatScroller bs;

    // Score
    int currentScore = 0;
    int scorePerNote = 100;
    int scorePerGreatNote = 125;
    int scorePerPerfectNote = 150;
    public int perfectCount = 0;
    public int greatCount = 0;
    public int normalCount = 0;
    public int missedCount = 0;
    

    // Mutliplier
    int currentMulti = 1;
    int multiTracker;
    int[] multiThresholds = { 4, 8, 16 };

    // UI
    BongoUI bongoUI;
    PlayerUI playerUI;

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        bongoUI = GameObject.FindObjectOfType<BongoUI>();
        playerUI = GameObject.FindObjectOfType<PlayerUI>();
    }

    void Update()
    {
        // Start game by pressing space
        if (!startPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            startPlaying = true;
            bs.SetCanMove(true);

            dspSongTime = (float) AudioSettings.dspTime;
            music.Play();
        }

        // Allows player to pause
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            playerUI.SetPauseMenu(true);
            Pause();
        }
        // In pause menu -- can press escape to leave or the button
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            // Make sure all menus close then
            playerUI.SetPauseMenu(false);
            playerUI.SetSettingsMenu(false);
            Resume();
        }

        // Shows end screen when done
        if (!paused && startPlaying && !music.isPlaying && !musicEnded)
        {
            musicEnded = true;
            playerUI.DisplayEndScreen();
        }
    }

    public void Pause()
    {
        paused = true;
        if (!startPlaying) return;
        startPauseTime = (float) AudioSettings.dspTime;
        music.Pause();
        bs.SetCanMove(false);
    }

    public void Resume()
    {
        paused = false;
        if (!startPlaying) return;
        endPauseTime = (float) AudioSettings.dspTime;
        music.Play();
        bs.SetCanMove(true);

        // Total pause time -- accounts for multiple paused intervals
        totalPauseTime += endPauseTime - startPauseTime;
    }

    private void NoteHit()
    {
        // Adjust multiplier
        if (currentMulti - 1 < multiThresholds.Length)
        {
            multiTracker++;
            if (multiThresholds[currentMulti - 1] <= multiTracker)
            {
                multiTracker = 0;
                currentMulti++;
                bongoUI.UpdateMulti(currentMulti);

                // Update BongoCat
                if (currentMulti == 2) bongoUI.SetCatJam(true, false);
                if (currentMulti == 3) bongoUI.SetCatJam(true, true);
            }
        }

        // Adjust UI
        bongoUI.UpdateScore(currentScore);
        bongoUI.EnemyHit();
    }

    public void NoteNormalHit()
    {
        normalCount++;
        currentScore += scorePerNote * currentMulti;
        NoteHit();
    }

    public void NoteGreatHit()
    {
        greatCount++;
        currentScore += scorePerGreatNote * currentMulti;
        NoteHit();
    }

    public void NotePerfectHit()
    {
        perfectCount++;
        currentScore += scorePerPerfectNote * currentMulti;
        NoteHit();
    }

    public void NoteMissed()
    {
        missedCount++;

        // Adjust multiplier
        currentMulti = 1;
        multiTracker = 0;
        bongoUI.UpdateMulti(currentMulti);

        // Update UI
        bongoUI.CootsMiss();
        bongoUI.SetCatJam(false, false);
    }

    public float GetDpsTime()
    {
        return dspSongTime;
    }

    public float GetPauseTime()
    {
        return totalPauseTime;
    }

    public bool GetPaused()
    {
        return paused;
    }

    public int GetTotalNotes()
    {
        return perfectCount + greatCount + normalCount + missedCount;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}