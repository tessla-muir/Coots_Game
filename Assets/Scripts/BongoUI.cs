using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BongoUI : MonoBehaviour
{
    Image coots;
    GameObject catJam1;
    GameObject catJam2;
    [SerializeField] Sprite cootsNormal;
    [SerializeField] Sprite cootsMiss;
    [SerializeField] Sprite cootsRight;
    [SerializeField] Sprite cootsLeft;

    void Start()
    {
        coots = GameObject.Find("Coots").GetComponent<Image>();

        // Find & Set Cat Jams to Inactive
        catJam1 = GameObject.Find("CatJam1");
        catJam2 = GameObject.Find("CatJam2");
        catJam1.SetActive(false);
        catJam2.SetActive(false);
    }

    
    void Update()
    {
        coots = GameObject.Find("Coots").GetComponent<Image>();

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            coots.sprite = cootsLeft;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            coots.sprite = cootsRight;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            StartCoroutine(BongoWait());
        }
    }

    public void CootsMiss()
    {
        coots.sprite = cootsMiss;
        StartCoroutine(MissWait());
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
        if (coots.sprite != cootsNormal) coots.sprite = cootsNormal;
    }

    IEnumerator MissWait()
    {
        yield return new WaitForSeconds(.3f);
        if (coots.sprite != cootsNormal) coots.sprite = cootsNormal;
    }
}
