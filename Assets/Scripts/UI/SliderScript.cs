using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] int startValue;
    AudioSource[] _audio;

    void Start()
    {
        _audio = GameObject.FindObjectOfType<GameManager>().GetMusic();

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
