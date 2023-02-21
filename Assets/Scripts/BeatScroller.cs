using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [SerializeField] float beatTempo;
    private bool hasStarted;  
    private float dspSongTime;  

    void Start()
    {
        beatTempo = 60f / beatTempo;  
    }

    void Update()
    {
        if (hasStarted)
        {
            dspSongTime = GameManager.instance.dspSongTime;
            transform.position = new Vector3(-1 * ((float) AudioSettings.dspTime - dspSongTime), transform.position.y, 0f);
        }
    }

    public void SetHasStarted(bool val)
    {
        hasStarted = val;
    }
}
