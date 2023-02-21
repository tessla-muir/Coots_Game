using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int level = 0;

    BongoGameManager bgm;

    GameObject bongoUI;
    GameObject bongoGameManager;



    // Start is called before the first frame update
    void Start()
    {
        //bgm = GameObject.FindObjectOfType<BongoGameManager>();

        bongoGameManager = GameObject.Find("Bongo Game Manager");
        bongoUI = GameObject.Find("Bongo UI");
        bongoUI.SetActive(false);
        bongoGameManager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Grabs UI needed
        if (Input.GetKeyDown("space")) 
        {
            if (level == 0) 
            {
                bongoGameManager.SetActive(true);
                bongoUI.SetActive(true);
            }
        }
    }
}
