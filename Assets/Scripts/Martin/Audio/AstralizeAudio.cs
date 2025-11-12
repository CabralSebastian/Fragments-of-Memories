using UnityEngine;

public class AstralizeAudio : MonoBehaviour
{
    [SerializeField] private AstralizeSkill _astralize;

    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private float _volume;

    private AudioSource _audioSource;

    private bool _previousState;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _previousState = _astralize.IsAstralized;
    }

    private void Update()
    {
        if ((_astralize.IsAstralized && !_previousState) || (!_astralize.IsAstralized && _previousState))
        {
            _audioSource.volume = _volume;
            _audioSource.PlayOneShot(_audioClip);
        }

        _previousState = _astralize.IsAstralized;
    }

}
