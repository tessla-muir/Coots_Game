using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Image image;
    BeatScroller bs;
    [SerializeField] KeyCode keyToPress;
    [SerializeField] Color defaultColor;
    [SerializeField] Color pressedColor;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        bs = GameObject.FindObjectOfType<BeatScroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bs.GetCanMove())
        {
            if (Input.GetKeyDown(keyToPress))
            {
                image.color = pressedColor;
            }

            if (Input.GetKeyUp(keyToPress))
            {
                image.color = defaultColor;
            }
        }
    }
}
