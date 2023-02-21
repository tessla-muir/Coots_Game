using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BongoGameManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] BeatScroller bs;
    bool startPlaying;
    public float dspSongTime;

    public static BongoGameManager instance;

    [SerializeField] int scorePerNote;
    int currentScore = 0;

    int currentMulti = 1;
    int multiTracker;
    int[] multiThresholds = { 4, 8, 16 };

    // Bongo UI
    BongoUI bongoUI;

    void Start()
    {
        instance = this;
        bongoUI = GameObject.Find("Bongo UI").GetComponent<BongoUI>();
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
    }

    public void NoteHit()
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

        // Adjust score
        currentScore += scorePerNote * currentMulti;
        bongoUI.UpdateScore(currentScore);
    }

    public void NoteMissed()
    {
        Debug.Log("Missed!");

        // Adjust multiplier
        currentMulti = 1;
        multiTracker = 0;
        bongoUI.UpdateMulti(currentMulti);

        // Update UI
        bongoUI.CootsMiss();
        bongoUI.SetCatJam(false, false);
    }
}