using UnityEngine;

public class GeyserAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    
    private AudioSource _audioSource;

    private GeyserEruption _geyser;


    private void Awake()
    {
        _geyser = GetComponent<GeyserEruption>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_geyser.IsAstralized)
        {
            _audioSource.enabled = true;
        }
        else
        {
            _audioSource.enabled = false;
        }
    }
}
