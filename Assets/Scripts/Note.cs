using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public bool canBePressed = false;
    [SerializeField] KeyCode keyToPress;

    private void Update() 
    {
        if (Input.GetKeyDown(keyToPress) && canBePressed)
        {
            gameObject.SetActive(false);
            BongoGameManager.instance.NoteHit();
        }

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Box")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Box" && gameObject.activeSelf)
        {
            canBePressed = false;
            BongoGameManager.instance.NoteMissed();
        }
    }
}
