using UnityEngine;

public class GrabAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private GrabSkill _grab;

    [SerializeField] private float _volume;

    private AudioSource _audioSource;

    private bool _previousState;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _previousState = _grab.IsGrabbing;
    }


    private void Update()
    {
        if(_grab.IsGrabbing && !_previousState)
        {
            _audioSource.volume = _volume;
            _audioSource.PlayOneShot(_audioClip);
        }

        _previousState = _grab.IsGrabbing;
    }
}
