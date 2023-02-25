using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] KeyCode keyToPress;
    bool canBePressed = false;
    float buttonX;
    GameObject noteButton;
    
    private void Start() 
    {
        buttonX = GameObject.FindWithTag("Box").transform.position.x;

        foreach(Transform button in GameObject.Find("NoteButtons").transform)
        {
            if (button.position.y == transform.position.y) 
            {   
                noteButton = button.gameObject;
            }
        }
    }

    private void Update() 
    {
        if (Input.GetKeyDown(keyToPress) && canBePressed && !BongoGameManager.instance.GetPaused())
        {
            // Make note inactive
            gameObject.SetActive(false);

            // Determine quality of note
            if (Mathf.Abs(buttonX - transform.position.x) > 0.055)
            {
                BongoGameManager.instance.NoteNormalHit(noteButton);
            }
            else if (Mathf.Abs(buttonX - transform.position.x) > 0.03)
            {
                BongoGameManager.instance.NoteGreatHit(noteButton);
            }
            else
            {
                BongoGameManager.instance.NotePerfectHit(noteButton);
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
            BongoGameManager.instance.NoteMissed(noteButton);
            gameObject.SetActive(false);
        }
    }

    public void SetCanBePressed(bool val)
    {
        canBePressed = val;
    }
}
