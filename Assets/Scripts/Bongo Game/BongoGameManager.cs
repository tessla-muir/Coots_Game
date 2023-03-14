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
    AudioSource music;
    BeatScroller bs;
    NoteTracker noteTracker;

    // Arrow generation
    [SerializeField] GameObject arrowHolder;
    [SerializeField] GameObject upArrow;
    [SerializeField] GameObject downArrow;
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;

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
        bs = GameObject.FindObjectOfType<BeatScroller>();
        noteTracker = GameObject.FindObjectOfType<NoteTracker>();

        PlaceArrows();
    }

    void PlaceArrows()
    {
        // Remove all arrows
        foreach (Transform arrow in arrowHolder.transform)
        {
            Destroy(arrow.gameObject);
        }

        for (int i = 0; i < 20; i++)
        {
            int choice = Random.Range(1, 5);
            float yVal = 0;
            GameObject newArrow = null;

            switch (choice)
            {
                case 1:
                    newArrow = Instantiate(upArrow); 
                    yVal = 150f;
                    break;

                case 2:
                    newArrow = Instantiate(rightArrow);
                    yVal = 50f;
                    break;

                case 3:
                    newArrow = Instantiate(leftArrow);
                    yVal = -50f;
                    break;

                case 4:
                    newArrow = Instantiate(downArrow);
                    yVal = -150f;
                    break;

                default:
                    continue;
            }
            
            newArrow.transform.SetParent(arrowHolder.transform, true);
            newArrow.transform.localScale = new Vector3(1, 1, 1);
            newArrow.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            newArrow.GetComponent<RectTransform>().localPosition = new Vector3(-320 + 80*i, yVal, 0);
        }
    }

    void Update()
    {
        // Start game by pressing space
        if (!startPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            bongoUI.UpdateStartText(false);
            startPlaying = true;
            bs.SetCanMove(true);
            noteTracker.Setup();

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

    public void NoteNormalHit(GameObject button)
    {
        normalCount++;
        currentScore += scorePerNote * currentMulti;
        NoteHit();
        bongoUI.UpdateAccuracyText(button, 1);
    }

    public void NoteGreatHit(GameObject button)
    {
        greatCount++;
        currentScore += scorePerGreatNote * currentMulti;
        NoteHit();
        bongoUI.UpdateAccuracyText(button, 2);
    }

    public void NotePerfectHit(GameObject button)
    {
        perfectCount++;
        currentScore += scorePerPerfectNote * currentMulti;
        NoteHit();
        bongoUI.UpdateAccuracyText(button, 3);
    }

    public void NoteMissed(GameObject button)
    {
        missedCount++;

        // Adjust multiplier
        currentMulti = 1;
        multiTracker = 0;
        bongoUI.UpdateMulti(currentMulti);

        // Update UI
        bongoUI.CootsMiss();
        bongoUI.SetCatJam(false, false);
        bongoUI.UpdateAccuracyText(button, 0);
    }

    public void NoteOffbeat(GameObject button)
    {
        missedCount++;

        // Adjust multiplier
        currentMulti = 1;
        multiTracker = 0;
        bongoUI.UpdateMulti(currentMulti);

        // Update UI
        bongoUI.CootsMiss();
        bongoUI.SetCatJam(false, false);
        bongoUI.UpdateAccuracyText(button, 4);
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

    public bool GetStartPlaying()
    {
        return startPlaying;
    }

    public void SetMusic(AudioSource audio)
    {
        music = audio;
    }

    public void Restart()
    {
        // Multiplier reset
        currentMulti = 1;
        multiTracker = 0;

        // Score reset
        currentScore = 0;

        // Pause time
        startPauseTime = 0;
        endPauseTime = 0;
        totalPauseTime = 0;

        // Total counts
        perfectCount = greatCount = normalCount = missedCount = 0;

        // Reset bools
        startPlaying = musicEnded = paused = false;

        // End music
        music.Stop();

        // Arrow reset -- Order matters: Needs after startPlaying reset
        bs.ResetArrows();

        // Update UI
        bongoUI.UpdateMulti(currentMulti);
        bongoUI.UpdateScore(currentScore);
        bongoUI.SetCatJam(false, false);
        bongoUI.ResetAccuracyText();
        bongoUI.UpdateStartText(true);

        // Reset Indicies
        noteTracker.SetLeftIndex(0);
        noteTracker.SetRightIndex(0);
        noteTracker.SetUpIndex(0);
        noteTracker.SetDownIndex(0);
    }
}