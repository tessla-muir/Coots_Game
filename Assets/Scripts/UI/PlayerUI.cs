using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    BongoUI bongoUI;
    GameObject endScreen;
    GameObject settingsScreen;
    GameObject pauseScreen;
    GameObject creditsScreen;
    GameObject levelScreen;
    GameObject songScreen;

    [Header("End Screen")]
    [SerializeField] TextMeshProUGUI outcomeText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI perfectText;
    [SerializeField] TextMeshProUGUI greatText;
    [SerializeField] TextMeshProUGUI normalText;
    [SerializeField] TextMeshProUGUI missedText;
    [SerializeField] Image enemyImage;
    
    [Header("Level Select Screen")]
    [SerializeField] TextMeshProUGUI difficultyText;

    void Start()
    {
        bongoUI = GameObject.Find("Bongo UI").GetComponent<BongoUI>();

        // Set end screen to inactive
        endScreen = GameObject.Find("EndScreen UI");
        endScreen.SetActive(false);

        // Set settings screen to inactive
        settingsScreen = GameObject.Find("Settings UI");
        settingsScreen.SetActive(false);

        // Set pause screen to inactive
        pauseScreen = GameObject.Find("Pause Menu UI");
        pauseScreen.SetActive(false);

        // Set credits screen to inactive
        creditsScreen = GameObject.Find("Credits UI");
        creditsScreen.SetActive(false);

        // Set level screen to inactive
        levelScreen = GameObject.Find("Level Select UI");
        levelScreen.SetActive(false);

        // Set song screen to inactive
        songScreen = GameObject.Find("Song Select UI");
        songScreen.SetActive(false);
    }

    public void DisplayEndScreen()
    {   
        endScreen.SetActive(true);

        // Update values
        scoreText.text = "Score: " + BongoGameManager.instance.GetCurrentScore();
        perfectText.text = "Perfect: " + BongoGameManager.instance.perfectCount;
        greatText.text = "Great: " + BongoGameManager.instance.greatCount;
        normalText.text = "Normal: " + BongoGameManager.instance.normalCount;
        missedText.text = "Missed: " + BongoGameManager.instance.missedCount;

        // Decide & Update Outcome
        if (BongoGameManager.instance.missedCount/(1.0 * BongoGameManager.instance.GetTotalNotes()) == 0)
        {             
            outcomeText.text = "PERFECT";
            enemyImage.sprite = bongoUI.enemyOnHit;
        }
        else if (BongoGameManager.instance.missedCount/(1.0 * BongoGameManager.instance.GetTotalNotes()) <= 0.02)
        {
            outcomeText.text = "GREAT";
            enemyImage.sprite = bongoUI.enemyOnHit;
        }
        else if (BongoGameManager.instance.missedCount/(1.0 * BongoGameManager.instance.GetTotalNotes()) <= 0.05)
        {
            outcomeText.text = "GOOD";
            enemyImage.sprite = bongoUI.enemyOnHit;
        }
        else
        {
            outcomeText.text = "RESULT";
            enemyImage.sprite = bongoUI.enemyNormal;
        }
       
    }

    public void SetPauseMenu(bool val)
    {
        pauseScreen.SetActive(val);
    }

    public void SetSettingsMenu(bool val)
    {
        settingsScreen.SetActive(val);
    }

    public bool IsPauseScreenActive()
    {
        return pauseScreen.activeSelf;
    }

    public bool hasActiveScreens()
    {
        return pauseScreen.activeSelf || settingsScreen.activeSelf  || endScreen.activeSelf;
    }

    public void SetDifficultyText(string difficulty)
    {
        difficultyText.text = "Difficulty: " + difficulty;
    }
}
