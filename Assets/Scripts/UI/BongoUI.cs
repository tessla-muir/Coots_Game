using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BongoUI : MonoBehaviour
{
    // Sprite
    Image coots;
    Image enemy;
    public Sprite enemyNormal;
    public Sprite enemyOnHit;
    [SerializeField] Sprite cootsNormal;
    [SerializeField] Sprite cootsMiss;
    [SerializeField] Sprite cootsRight;
    [SerializeField] Sprite cootsLeft;
    [SerializeField] Sprite cootsBoth;

    // Cat Jam
    GameObject catJam1;
    GameObject catJam2;

    // Text
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI multiText;

    void Start()
    {
        coots = GameObject.Find("Coots").GetComponent<Image>();
        enemy = GameObject.Find("Enemy").GetComponent<Image>();

        // Find & Set Cat Jams to Inactive
        catJam1 = GameObject.Find("CatJam1");
        catJam2 = GameObject.Find("CatJam2");
        catJam1.SetActive(false);
        catJam2.SetActive(false);
    }

    void Update()
    {
        bool left = false;
        bool right = false;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow)) left = true;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow)) right = true;

        if (!BongoGameManager.instance.GetPaused()) UpdateCootsSprite(left, right);
    }

    private void UpdateCootsSprite(bool left, bool right)
    {
        if (left && right)
        {
            coots.sprite = cootsBoth;
            StartCoroutine(BongoWait());
        }
        else if (left)
        {
            coots.sprite = cootsLeft;
            StartCoroutine(BongoWait());
        }
        else if (right)
        {
            coots.sprite = cootsRight;
            StartCoroutine(BongoWait());
        }
    }

    public void CootsMiss()
    {
        coots.sprite = cootsMiss;
        StartCoroutine(MissWait());
    }

    public void EnemyHit()
    {
        enemy.sprite = enemyOnHit;
        StartCoroutine(HitWait());
    }

    public void SetCatJam(bool val, bool val2)
    {
        // Set false first to sync animations
        catJam1.SetActive(false);
        catJam2.SetActive(false);

        // Set values
        catJam1.SetActive(val);
        catJam2.SetActive(val2);
    }

    IEnumerator BongoWait()
    {
        yield return new WaitForSeconds(.1f);
        coots.sprite = cootsNormal;
    }

    IEnumerator MissWait()
    {
        yield return new WaitForSeconds(.5f);
        coots.sprite = cootsNormal;
    }

    IEnumerator HitWait()
    {
        yield return new WaitForSeconds(.4f);
        enemy.sprite = enemyNormal;
    }

    public void UpdateScore(int val)
    {
        scoreText.text = "Score: " + val;
    }

    public void UpdateMulti(int val)
    {
        multiText.text = "Multiplier: x" + val;
    }
}
