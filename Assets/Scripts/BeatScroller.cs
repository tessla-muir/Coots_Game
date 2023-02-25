using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] float beatTempo;
    private bool canMove;  
    private Vector3 ogPosition;

    void Start()
    {
        beatTempo = 60f / beatTempo;
        ogPosition = transform.position;
    }

    void Update()
    {
        if (canMove)
        {
            transform.position = new Vector3(-1 * (((float) AudioSettings.dspTime - BongoGameManager.instance.GetDpsTime()) - BongoGameManager.instance.GetPauseTime()), transform.position.y, 0f);
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

    public void ResetArrows()
    {
        transform.position = ogPosition;

        foreach (Transform arrow in transform)
        {
            arrow.gameObject.SetActive(true);
            arrow.GetComponent<Note>().SetCanBePressed(false);
        }
    }
}
