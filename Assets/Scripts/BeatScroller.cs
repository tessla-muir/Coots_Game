using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] float beatTempo;
    private bool canMove;  

    void Start()
    {
        beatTempo = 60f / beatTempo;
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
}
