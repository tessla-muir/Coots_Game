using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] AudioSource[] _audio;
    [SerializeField] int startValue;


    void Start()
    {
        slider.maxValue = 100;
        slider.minValue = 0;
        slider.value = startValue;

        slider.onValueChanged.AddListener((val) => {
            foreach (var audio in _audio)
            {
                audio.volume = val/100;
            }
        });
    }
}
