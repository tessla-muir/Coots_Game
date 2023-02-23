using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    int level = 0;
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
        // Grabs UI needed
        if (Input.GetKeyDown("space")) 
        {
            if (level == 0) 
            {
                BongoGameManager.instance.gameObject.SetActive(true);
                bongoUI.SetActive(true);
            }
        }
    }
}
