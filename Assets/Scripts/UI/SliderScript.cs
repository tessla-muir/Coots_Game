using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] GameObject holder;
    [SerializeField] int startValue;
    [SerializeField] Slider slider;
    [SerializeField] bool isMusic;
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isMusic)
        {
            _audio[GameManager.instance.GetCurrentLevel()].Play();
        }
        else
        {
            _audio[0].Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isMusic)
        {
            _audio[GameManager.instance.GetCurrentLevel()].Stop();
        }
        else
        {
            _audio[0].Stop();
        }
    }
}
