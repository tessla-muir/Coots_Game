using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BongoGameManager : MonoBehaviour
{
    public static BongoGameManager instance;

    // Music
    bool startPlaying;
    bool musicStopped;
    public float dspSongTime;
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
    EndScreenUI endScreenUI;

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        bongoUI = GameObject.FindObjectOfType<BongoUI>();;
        endScreenUI = GameObject.FindObjectOfType<EndScreenUI>();;
    }

    void Update()
    {
        if (!startPlaying && Input.anyKeyDown)
        {
            startPlaying = true;
            bs.SetHasStarted(true);

            dspSongTime = (float) AudioSettings.dspTime;
            music.Play();
        }

        if (startPlaying && !music.isPlaying && !musicStopped)
        {
            musicStopped = true;
            // End screen
            endScreenUI.UpdateEndScreen();
        }
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

    public int GetTotalNotes()
    {
        return perfectCount + greatCount + normalCount + missedCount;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}