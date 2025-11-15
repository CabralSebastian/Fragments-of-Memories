using UnityEngine;

public class BearAttackAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;

    [SerializeField] private float _volume;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void AttackSound()
    {
        _audioSource.volume = _volume;
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);
    }
}
