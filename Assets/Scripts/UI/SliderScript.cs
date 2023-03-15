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
    float resumeTime;
    bool hasAdjustedVolume;

    void Start()
    {
        _audio = holder.transform.GetComponents<AudioSource>();

        slider.onValueChanged.AddListener((val) =>
        {
            foreach (var audio in _audio)
            {
                audio.volume = val / 100;
            }
        });

        slider.maxValue = 100;
        slider.minValue = 0;
        slider.value = startValue;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isMusic)
        {
            if (!hasAdjustedVolume)
            {
                resumeTime = _audio[GameManager.instance.GetCurrentLevel()].time;
                hasAdjustedVolume = true;
            }
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
            _audio[GameManager.instance.GetCurrentLevel()].Pause();
            _audio[GameManager.instance.GetCurrentLevel()].time = resumeTime;
        }
        else
        {
            _audio[0].Stop();
        }
    }

    public void SetHasAdjustedVolume(bool val)
    {
        hasAdjustedVolume = val;
    }
}
