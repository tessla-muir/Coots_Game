using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    float tempo;
    float beatTempo;
    [SerializeField] GameObject arrowHolder;
    private bool canMove;  
    private Vector3 ogPosition;


    public void Setup(int tempo)
    {
        this.tempo = tempo;
        beatTempo = 60f / tempo;

        // Get arrow holder and starting position
        ogPosition = arrowHolder.transform.position;
    }

    void Update()
    {
        if (canMove)
        {
            arrowHolder.transform.position = new Vector3(-1 * (((float) AudioSettings.dspTime - BongoGameManager.instance.GetDpsTime()) - BongoGameManager.instance.GetPauseTime()), arrowHolder.transform.position.y, 0f);
        }
    }

    public void SetCanMove(bool val)
    {
        canMove = val;
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public float GetTempo()
    {
        return tempo;
    }

    public void ResetArrows()
    {
        arrowHolder.transform.position = ogPosition;

        foreach (Transform arrow in arrowHolder.transform)
        {
            arrow.gameObject.SetActive(true);
            arrow.GetComponent<Note>().SetCanBePressed(false);
        }
    }
}
