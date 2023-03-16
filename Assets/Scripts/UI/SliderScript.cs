using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler
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

    public void SetHasAdjustedVolume(bool val)
    {
        hasAdjustedVolume = val;
    }

    public void OnBeginDrag(PointerEventData eventData)
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
            _audio[3].Play();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isMusic)
        {
            _audio[GameManager.instance.GetCurrentLevel()].Stop();
            _audio[GameManager.instance.GetCurrentLevel()].time = resumeTime;
        }
        else
        {
            _audio[3].Stop();
        }
    }
}
