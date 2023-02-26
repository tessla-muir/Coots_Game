using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] GameObject holder;
    [SerializeField] int startValue;
    [SerializeField] Slider slider;
    AudioSource[] _audio;

    void Start()
    {
        _audio = holder.transform.GetComponents<AudioSource>();

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
