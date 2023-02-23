using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsUI : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;

    void Start()
    {
        scrollbar.value = 1;
    }

    // // Update is called once per frame
    // void Update()
    // {
    //    if (canScroll) StartCoroutine(Wait());
    // }

    // IEnumerator Wait()
    // {
    //     yield return new WaitForSeconds(2f);
    //     if (scrollbar.value - .01f > 0)
    //     {
    //         scrollbar.value = scrollbar.value - .001f;
    //     }
    //     else
    //     {
    //         canScroll = false;
    //     }
    // }
}
