using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreenUI : MonoBehaviour
{
    GameObject endScreen;
    BongoUI bongoUI;

    // Score & Outcome
    [SerializeField] TextMeshProUGUI outcomeText;
    [SerializeField] TextMeshProUGUI scoreText;

    // Shows counts
    [SerializeField] TextMeshProUGUI perfectText;
    [SerializeField] TextMeshProUGUI greatText;
    [SerializeField] TextMeshProUGUI normalText;
    [SerializeField] TextMeshProUGUI missedText;

    // Sprite
    [SerializeField] Image enemyImage;

    void Start()
    {
        bongoUI = GameObject.Find("Bongo UI").GetComponent<BongoUI>();
        endScreen = GameObject.Find("EndScreen UI");
        endScreen.SetActive(false);
    }

    public void UpdateEndScreen()
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
}
