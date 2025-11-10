using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]AudioSource _audioSource;
    public void PlaySound()
    {
        _audioSource.Play();
    }
}
