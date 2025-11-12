using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _cursorOverClip;
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] private AudioClip _backClip;
    [SerializeField] private AudioClip _playClip;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void CursorOverSound()
    {
        _audioSource.PlayOneShot(_cursorOverClip);
    }

    public void ClickSound()
    {
        _audioSource.PlayOneShot(_clickClip);
    }

    public void BackSound()
    {
        _audioSource.PlayOneShot(_backClip);
    }

    public void ClickPlaySound()
    {
        _audioSource.PlayOneShot(_playClip);
    }
}
