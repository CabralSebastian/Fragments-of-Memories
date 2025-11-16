using UnityEngine;

public class HitAudio : MonoBehaviour
{
    [SerializeField] private float _minimumTime = 0.5f;
    [SerializeField] private float _minimumPitch = 0.5f;
    [SerializeField] private float _maximumPitch = 1f;
    [SerializeField] private float _volume = 0.5f;
    [SerializeField] private AudioClip _audioClip;
    private Health _character;
    private AudioSource _audioSource;
    private float _timer;
    private float _currentHealth;
    private float _previousHealth;

    private void Start()
    {
        _character = GetComponent<Health>();
        _audioSource = GetComponent<AudioSource>();
        _timer = _minimumTime;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        _currentHealth = _character.Value;
        if (_timer >= _minimumTime && _currentHealth < _previousHealth)
        {
            _audioSource.volume = _volume;
            _audioSource.pitch = Random.Range(_minimumPitch, _maximumPitch);
            _audioSource.PlayOneShot(_audioClip);

            _timer = 0f;
        }
        _previousHealth = _character.Value;
    }
}
