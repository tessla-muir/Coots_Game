using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] KeyCode keyToPress;
    bool canBePressed = false;
    float buttonX;
    
    
    private void Start() 
    {
        buttonX = GameObject.FindWithTag("Box").transform.position.x;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(keyToPress) && canBePressed)
        {
            // Make note inactive
            gameObject.SetActive(false);

            // Debug.Log(Mathf.Abs(buttonX - transform.position.x));
            // Determine quality of note
            if (Mathf.Abs(buttonX - transform.position.x) > 0.055)
            {
                Debug.Log("Normal");
                BongoGameManager.instance.NoteNormalHit();
            }
            else if (Mathf.Abs(buttonX - transform.position.x) > 0.03)
            {
                Debug.Log("Great");
                BongoGameManager.instance.NoteGreatHit();
            }
            else
            {
                Debug.Log("Perfect");
                BongoGameManager.instance.NotePerfectHit();
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
            BongoGameManager.instance.NoteMissed();
        }
    }
}
