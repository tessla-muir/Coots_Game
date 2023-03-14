using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] KeyCode keyToPress;
    [SerializeField] string buttonName;
    GameObject button;
    bool canBePressed = false;
    float buttonX;
    NoteTracker noteTracker;
    
    private void Start() 
    {
        button = GameObject.Find(buttonName);
        buttonX = button.transform.position.x;
        noteTracker = GameObject.FindObjectOfType<NoteTracker>();
    }

    private void Update() 
    {
        if (!noteTracker.IsHeadArrow(keyToPress, gameObject)) { return; }

        if (Input.GetKeyDown(keyToPress) && canBePressed && !BongoGameManager.instance.GetPaused() && noteTracker.IsHeadArrow(keyToPress, gameObject))
        {
            // Make note inactive
            gameObject.SetActive(false);

            // Move index
            noteTracker.NextIndex(keyToPress);

            // Determine quality of note
            if (buttonX - transform.position.x > 0.33)
            {
                // Miss -- too far left
                BongoGameManager.instance.NoteMissed(button);
            }
            else if (buttonX - transform.position.x < -0.33)
            {
                // Offbeat -- too far right; early
                BongoGameManager.instance.NoteOffbeat(button);
            }
            else if (Mathf.Abs(buttonX - transform.position.x) > 0.055)
            {
                BongoGameManager.instance.NoteNormalHit(button);
            }
            else if (Mathf.Abs(buttonX - transform.position.x) > 0.03)
            {
                BongoGameManager.instance.NoteGreatHit(button);
            }
            else
            {
                BongoGameManager.instance.NotePerfectHit(button);
            }
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
            BongoGameManager.instance.NoteMissed(button);
            gameObject.SetActive(false);
            noteTracker.NextIndex(keyToPress);
        }
    }

    public void SetCanBePressed(bool val)
    {
        canBePressed = val;
    }
}
