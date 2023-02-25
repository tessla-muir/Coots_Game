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

    [Header("End Screen")]
    [SerializeField] TextMeshProUGUI outcomeText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI perfectText;
    [SerializeField] TextMeshProUGUI greatText;
    [SerializeField] TextMeshProUGUI normalText;
    [SerializeField] TextMeshProUGUI missedText;
    [SerializeField] Image enemyImage;

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
        outcomeText.text = "VICTORY";

        // Update enemy
        enemyImage.sprite = bongoUI.enemyOnHit;
    }

    public void SetPauseMenu(bool val)
    {
        pauseScreen.SetActive(val);
    }

    public void SetSettingsMenu(bool val)
    {
        settingsScreen.SetActive(val);
    }
}
