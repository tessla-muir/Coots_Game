using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    GameObject bongoUI;
    GameObject bongoGameManager;

    void Start()
    {
        instance = this;
        bongoUI = GameObject.Find("Bongo UI");
        BongoGameManager.instance.gameObject.SetActive(false);
        bongoUI.SetActive(false);
    }

    void Update()
    {
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void StartBongoGame()
    {
        BongoGameManager.instance.gameObject.SetActive(true);
        bongoUI.SetActive(true);
    }
}
