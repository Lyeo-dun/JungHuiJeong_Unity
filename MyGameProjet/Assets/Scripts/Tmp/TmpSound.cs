using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TmpSound : MonoBehaviour
{
    private AudioSource BGMAudio;
    [SerializeField] private Toggle CheckBox;
    [SerializeField] private Slider SoundVolume;

    private float CurrentVolume;

    private void Awake()
    {
        BGMAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        CurrentVolume = 0.5f;
        BGMAudio.volume = CurrentVolume;
        SoundVolume.value = CurrentVolume;
    }
    public void Update()
    {
        if(CurrentVolume != SoundVolume.value)
        {
            CurrentVolume = SoundVolume.value;
            BGMAudio.volume = CurrentVolume;
        }
    }
    public void OnEventSound()
    {
        if (CheckBox.isOn)
            BGMAudio.Stop();

        if (!CheckBox.isOn)
        {
            BGMAudio.volume = CurrentVolume;
            BGMAudio.Play();
        }
    }
}
