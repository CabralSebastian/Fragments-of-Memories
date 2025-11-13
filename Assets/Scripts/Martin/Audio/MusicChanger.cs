using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicChanger : MonoBehaviour
{
    private AudioSource _exploringSource;
    private AudioSource _combatSource;

    void Start()
    {
        _exploringSource = GetComponent<AudioSource>();
        _exploringSource.Play();
        _combatSource.Play();
    }
}
