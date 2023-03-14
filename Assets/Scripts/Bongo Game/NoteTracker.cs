using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NoteTracker : MonoBehaviour
{
    List<GameObject> leftArrows;
    List<GameObject> rightArrows;
    List<GameObject> upArrows;
    List<GameObject> downArrows;

    // Head arrow indicies
    int leftIndex = 0;
    int rightIndex = 0;
    int upIndex = 0;
    int downIndex = 0;

    [SerializeField] GameObject arrowHolder;

    void Start() 
    {
        // Instanitate lists
        leftArrows = new List<GameObject>();
        rightArrows = new List<GameObject>();
        upArrows = new List<GameObject>();
        downArrows = new List<GameObject>();
    }

    public void Setup()
    {
        // Fill up lists
        leftArrows = GameObject.FindGameObjectsWithTag("Left").ToList();
        rightArrows = GameObject.FindGameObjectsWithTag("Right").ToList();
        upArrows = GameObject.FindGameObjectsWithTag("Up").ToList();
        downArrows = GameObject.FindGameObjectsWithTag("Down").ToList();

        // Sort lists smallest x to biggest x
        leftArrows.Sort(SortByPosition);
        rightArrows.Sort(SortByPosition);
        upArrows.Sort(SortByPosition);
        downArrows.Sort(SortByPosition);
    }

    // Method for sort command
    static int SortByPosition(GameObject val1, GameObject val2)
    {
        return val1.transform.position.x.CompareTo(val2.transform.position.x);
    }

    private void RemoveUnwantedArrows(List<GameObject> list)
    {
        foreach (GameObject arrow in list)
        {
            if (arrow.transform.parent != arrowHolder)
            {
                list.Remove(arrow);
            }
        }
    }

    public void NextIndex(KeyCode key)
    {
        StartCoroutine(NextIndexWait(key));
    }

    IEnumerator NextIndexWait(KeyCode key)
    {
        yield return new WaitForSeconds(.3f);
        if (key == KeyCode.UpArrow)
        {
            upIndex++;
        }
        else if (key == KeyCode.DownArrow)
        {
            downIndex++;
        }
        else if (key == KeyCode.RightArrow)
        {
            rightIndex++;
        }
        else if (key == KeyCode.LeftArrow)
        {
            leftIndex++;
        }
    }

    public bool IsHeadArrow(KeyCode key, GameObject arrow)
    {
        if (key == KeyCode.UpArrow)
        {
            if (upArrows.Count == 0) return false;
            return GameObject.ReferenceEquals(upArrows[upIndex], arrow);
        }
        else if (key == KeyCode.DownArrow)
        {
            if (downArrows.Count == 0) return false;
            return GameObject.ReferenceEquals(downArrows[downIndex], arrow);
        }
        else if (key == KeyCode.RightArrow)
        {
            if (rightArrows.Count == 0) return false;
            return GameObject.ReferenceEquals(rightArrows[rightIndex], arrow);
        }
        else if (key == KeyCode.LeftArrow)
        {
            if (leftArrows.Count == 0) return false;
            return GameObject.ReferenceEquals(leftArrows[leftIndex], arrow);
        }

        return false;
    }

    public void SetLeftIndex(int val)
    {
        leftIndex = val;
    }

    public void SetRightIndex(int val) 
    {
        rightIndex = val;
    }

      public void SetUpIndex(int val)
    {
        upIndex = val;
    }

    public void SetDownIndex(int val) 
    {
        downIndex = val;
    }
}
