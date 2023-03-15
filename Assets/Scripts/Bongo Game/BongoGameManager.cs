using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BongoGameManager : MonoBehaviour
{
    public static BongoGameManager instance;

    // Music
    bool startedSong = false;
    bool displayedEndMenu = false;
    bool paused = false;
    float dspSongTime;
    float startPauseTime = 0;
    float endPauseTime = 0;
    float totalPauseTime = 0;
    AudioSource music;
    BeatScroller bs;
    NoteTracker noteTracker;
    [SerializeField] SliderScript slider;

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
        // Remove all arrows in holder -- if any
        foreach (Transform arrow in arrowHolder.transform)
        {
            Destroy(arrow.gameObject);
        }

        // Determine arrows needed
        float length = music.clip.length;
        float tempo = bs.GetTempo();

        for (int i = 0; i < length * tempo / (2.1 * 60f); i++)
        {
            int choice = Random.Range(1, 101);

            // Single arrow
            if (choice <= 75)
            {
                MakeRandomArrow(i);
            }
            // Double arrows
            else if (choice <= 94)
            {
                int firstChoice = MakeRandomArrow(i);
                MakeRandomArrow(i, firstChoice);
            }
            // No arrow
            else
            {
                continue;
            }
        }
    }

    private int MakeRandomArrow(float xVal, int restriction = 0)
    {
        float yVal = 0;
        GameObject newArrow = null;
        int arrowChoice = Random.Range(1, 5);

        // Make sure choice isn't the restricted value
        while (arrowChoice == restriction)
        {
           arrowChoice = Random.Range(1, 5); 
        }
        
        // Make arrow based on choice
        switch (arrowChoice)
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
        }

        newArrow.transform.SetParent(arrowHolder.transform, true);
        newArrow.transform.localScale = new Vector3(1, 1, 1);
        newArrow.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        newArrow.GetComponent<RectTransform>().localPosition = new Vector3(-320 + 80 * xVal, yVal, 0);
        return arrowChoice;
    }

    void Update()
    {
        // Start game by pressing space
        if (!startedSong && Input.GetKeyDown(KeyCode.Space))
        {
            bongoUI.UpdateStartText(10);
            startedSong = true;
            bs.SetCanMove(true);
            noteTracker.Setup();

            dspSongTime = (float)AudioSettings.dspTime;
            music.Play();
        }

        // Pause by presssing ESC
        if (startedSong && !paused && Input.GetKeyDown(KeyCode.Escape))
        {
            playerUI.SetPauseMenu(true);
            bongoUI.UpdateStartText(2);
            Pause();
        }
        // Closes pause menu with esc
        else if (startedSong && paused && Input.GetKeyDown(KeyCode.Escape))
        {
            playerUI.SetPauseMenu(false);
        }
        // Resume song
        else if (startedSong && paused && Input.GetKeyDown(KeyCode.Space) && !playerUI.hasActiveScreens())
        {
            bongoUI.UpdateStartText(10);
            Resume();
        }

        // Shows end screen when done
        if (startedSong && !paused && !music.isPlaying && !displayedEndMenu)
        {
            displayedEndMenu = true;
            playerUI.DisplayEndScreen();
        }
    }

    public void Pause()
    {
        paused = true;
        if (!startedSong) return;
        startPauseTime = (float)AudioSettings.dspTime;
        music.Pause();
        bs.SetCanMove(false);
    }

    public void Resume()
    {
        paused = false;
        if (!startedSong) return;
        endPauseTime = (float)AudioSettings.dspTime;
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
        return startedSong;
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
        startedSong = displayedEndMenu = paused = false;

        // End music
        music.Stop();

        // Arrow reset -- Order matters: Needs after startPlaying reset
        bs.ResetArrows();

        // Update UI
        bongoUI.UpdateMulti(currentMulti);
        bongoUI.UpdateScore(currentScore);
        bongoUI.SetCatJam(false, false);
        bongoUI.ResetAccuracyText();
        bongoUI.UpdateStartText(1);

        // Reset Indicies
        noteTracker.SetLeftIndex(0);
        noteTracker.SetRightIndex(0);
        noteTracker.SetUpIndex(0);
        noteTracker.SetDownIndex(0);
    }
}