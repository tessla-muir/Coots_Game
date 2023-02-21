using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BongoUI : MonoBehaviour
{
    Image coots;
    [SerializeField] Sprite cootsNormal;
    [SerializeField] Sprite cootsMiss;
    [SerializeField] Sprite cootsRight;
    [SerializeField] Sprite cootsLeft;

    void Start()
    {
        coots = GameObject.Find("Coots").GetComponent<Image>();
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
    }

    IEnumerator BongoWait()
    {
        yield return new WaitForSeconds(.1f);
        coots.sprite = cootsNormal;
    }
}
