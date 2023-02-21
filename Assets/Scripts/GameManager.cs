using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] BeatScroller bs;
    bool startPlaying;
    public float dspSongTime;

    public static GameManager instance;

    [SerializeField] int scorePerNote;
    [SerializeField] TextMeshProUGUI scoreText;
    int currentScore = 0;

    [SerializeField] TextMeshProUGUI multiText;
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
        Debug.Log("Hit!");

        // Adjust multiplier
        if (currentMulti - 1 < multiThresholds.Length)
        {
            multiTracker++;
            if (multiThresholds[currentMulti - 1] <= multiTracker)
            {
                multiTracker = 0;
                currentMulti++;
                multiText.text = "Multiplier: x" + currentMulti;
            }
        }

        // Adjust score
        currentScore += scorePerNote * currentMulti;
        scoreText.text = "Score: " + currentScore;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed!");

        // Adjust multiplier
        currentMulti = 1;
        multiTracker = 0;
        multiText.text = "Multiplier: x" + currentMulti;

        // Update UI
        bongoUI.CootsMiss();
    }
}