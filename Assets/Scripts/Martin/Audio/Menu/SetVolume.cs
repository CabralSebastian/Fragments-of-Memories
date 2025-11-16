using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private Slider mainSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider voiceSlider;

    [SerializeField] private AudioMixer mainMixer;
    

    private void Update()
    {
        mainMixer.SetFloat("Master", mainSlider.value);
        mainMixer.SetFloat("MusicVolume", musicSlider.value);
        mainMixer.SetFloat("SFXVolume", sfxSlider.value);
        mainMixer.SetFloat("VoiceVolume", voiceSlider.value);

    }
}
