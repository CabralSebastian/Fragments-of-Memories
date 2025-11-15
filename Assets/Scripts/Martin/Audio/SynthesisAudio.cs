using UnityEngine;

public class SynthesisAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _activatingClip;
    [SerializeField] private AudioClip _deactivatingClip;

    private Synthesizer _synthesis;
    private AudioSource _audioSource;

    private SynthesizerState _previousState;

    private void Awake()
    {
        _synthesis = GetComponentInChildren<Synthesizer>();
        _audioSource = GetComponent<AudioSource>();
        _previousState = _synthesis.State;
    }

    private void Update()
    {
        if (_synthesis.State == SynthesizerState.ACTIVATING && _previousState == SynthesizerState.OFF)
        {
            _audioSource.PlayOneShot(_activatingClip);
        }


        else if (_synthesis.State == SynthesizerState.DEACTIVATING && _previousState == SynthesizerState.ACTIVE)
        {
            _audioSource.PlayOneShot(_deactivatingClip);
        }

        _previousState = _synthesis.State;
    }
}
